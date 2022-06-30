namespace ActivityAppLibrary.Models
{
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ObjectID { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }

        public List<ProposalModel> Proposals { get; set; } = new();
        public List<ProposalModel> ProposalsVoted { get; set; } = new();
    }
}
