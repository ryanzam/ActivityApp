using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityAppLibrary.Data
{
    public class ProposalData : IProposalData
    {
        private readonly IMongoCollection<ProposalModel> proposals;
        private readonly IDbConnection _dbConnection;
        private readonly IMemoryCache _memoryCache;
        private readonly IUserData _userData;

        private const string CacheName = "proposals";

        public ProposalData(IDbConnection dbConnection, IMemoryCache memoryCache, IUserData userData)
        {
            proposals = dbConnection.ProposalCollection;
            _dbConnection = dbConnection;
            _memoryCache = memoryCache;
            _userData = userData;
        }

        public async Task<List<ProposalModel>> GetProposals()
        {
            var cachedData = _memoryCache.Get<List<ProposalModel>>(CacheName);
            if (cachedData is null)
            {
                var res = await proposals.FindAsync(p => p.IsArchived == false);
                cachedData = res.ToList();
                _memoryCache.Set(CacheName, cachedData, TimeSpan.FromMinutes(1));
            }
            return cachedData;
        }

        public async Task<List<ProposalModel>> GetApprovedProposals()
        {
            var data = await GetProposals();
            return data.Where(p => p.IsApproved).ToList();
        }

        public async Task<ProposalModel> GetProposal(string id)
        {
            var data = await proposals.FindAsync(p => p.Id == id);
            return data.FirstOrDefault();
        }

        public async Task<List<ProposalModel>> GetNotApprovedProposals()
        {
            var data = await GetProposals();
            return data.Where(p => p.IsDismissed == false && p.IsApproved == false).ToList();
        }

        public async void UpdateProposal(ProposalModel proposal)
        {
            await proposals.ReplaceOneAsync(p => p.Id == proposal.Id, proposal);
            _memoryCache.Remove(CacheName);
        }

        public async Task ThumbsUpProposal(string propId, string usrId)
        {
            var client = _dbConnection.MongoClient;

            using var session = await client.StartSessionAsync();
            session.StartTransaction();

            try
            {
                var db = client.GetDatabase(_dbConnection.Db);
                var propsInTransac = db.GetCollection<ProposalModel>(_dbConnection.Proposals);
                var prop = (await propsInTransac.FindAsync(p => p.Id == propId)).First();
                bool isThumbsUp = prop.UserThumbsUps.Add(usrId);
                if (!isThumbsUp)
                {
                    prop.UserThumbsUps.Remove(usrId);
                }
                await propsInTransac.ReplaceOneAsync(p => p.Id == propId, prop);

                var usrsInTransac = db.GetCollection<UserModel>(_dbConnection.Users);
                var usr = await _userData.GetUser(prop.Proposer.Id);

                if (isThumbsUp) { usr.ProposalsThumbsUpped.Add(new ProposalModel(prop)); }
                else
                {
                    var removeProp = usr.ProposalsThumbsUpped.Where(p => p.Id == propId).First();
                    usr.ProposalsThumbsUpped.Remove(removeProp);
                }
                await usrsInTransac.ReplaceOneAsync(u => u.Id == usrId, usr);
                await session.CommitTransactionAsync();
                _memoryCache.Remove(CacheName);
            }
            catch (Exception)
            {
                await session.AbortTransactionAsync();
                throw;
            }
        }

        public async Task CreateProposal(ProposalModel proposal)
        {
            var client = _dbConnection.MongoClient;
            using var session = await client.StartSessionAsync();
            session.StartTransaction();

            try
            {
                var db = client.GetDatabase(_dbConnection.Db);
                var propsInTransac = db.GetCollection<ProposalModel>(_dbConnection.Proposals);
                await propsInTransac.InsertOneAsync(proposal);

                var usrsInTransac = db.GetCollection<UserModel>(_dbConnection.Users);
                var usr = await _userData.GetUser(proposal.Proposer.Id);
                await usrsInTransac.ReplaceOneAsync(u => u.Id == usr.Id, usr);
                await session.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await session.AbortTransactionAsync();
                throw;
            }
        }
    }
}
