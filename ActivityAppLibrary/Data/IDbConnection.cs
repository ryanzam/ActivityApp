using MongoDB.Driver;

namespace ActivityAppLibrary.Data
{
    public interface IDbConnection
    {
        string Activities { get; }
        IMongoCollection<ActivityModel> ActivityCollection { get; }
        string Db { get; }
        MongoClient MongoClient { get; }
        IMongoCollection<ProposalModel> ProposalCollection { get; }
        string Proposals { get; }
        string Status { get; }
        IMongoCollection<StatusModel> StatusCollection { get; }
        IMongoCollection<UserModel> UserCollection { get; }
        string Users { get; }
    }
}