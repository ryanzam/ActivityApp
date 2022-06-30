namespace ActivityAppLibrary.Models
{
    public class ProposalModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ActivityModel Activity { get; set; }
        public UserModel Proposer { get; set; }
        public HashSet<string> UpVotes { get; set; } = new();
        public StatusModel ProposalStatus { get; set; }
        public string Notes { get; set; }
        public bool Approved { get; set; } = false;
        public bool Archived { get; set; } = false;
        public bool Dismissed { get; set; } = false;
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
