namespace ActivityAppLibrary.Data
{
    public interface IProposalData
    {
        Task CreateProposal(ProposalModel proposal);
        Task<List<ProposalModel>> GetApprovedProposals();
        Task<List<ProposalModel>> GetNotApprovedProposals();
        Task<ProposalModel> GetProposal(string id);
        Task<List<ProposalModel>> GetProposals();
        Task<List<ProposalModel>> GetUsersProposals(string uid);
        Task ThumbsUpProposal(string propId, string usrId);
        Task UpdateProposal(ProposalModel proposal);
    }
}