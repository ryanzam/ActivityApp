using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ActivityAppLibrary.Data
{
    public class DbConnection : IDbConnection
    {
        private readonly IConfiguration _configuraton;
        private readonly IMongoDatabase _mdb;
        private string _connId = "MongoDB";
        public string Db { get; private set; }
        public string Proposals { get; private set; } = "proposals";
        public string Activities { get; private set; } = "activities";
        public string Users { get; private set; } = "users";
        public string Status { get; private set; } = "status";
        public MongoClient MongoClient { get; private set; }
        public IMongoCollection<ProposalModel> ProposalCollection { get; private set; }
        public IMongoCollection<ActivityModel> ActivityCollection { get; private set; }
        public IMongoCollection<UserModel> UserCollection { get; private set; }
        public IMongoCollection<StatusModel> StatusCollection { get; private set; }

        public DbConnection(IConfiguration configuraton)
        {
            _configuraton = configuraton;
            MongoClient = new MongoClient(_configuraton.GetConnectionString(_connId));
            Db = _configuraton["DB"];
            _mdb = MongoClient.GetDatabase(Db);

            ProposalCollection = _mdb.GetCollection<ProposalModel>(Proposals);
            ActivityCollection = _mdb.GetCollection<ActivityModel>(Activities);
            UserCollection = _mdb.GetCollection<UserModel>(Users);
            StatusCollection = _mdb.GetCollection<StatusModel>(Status);
        }
    }
}
