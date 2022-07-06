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
        public SimpleUserModel Proposer { get; set; }
        public HashSet<string> UserThumbsUps { get; set; } = new();
        public StatusModel ProposalStatus { get; set; }
        public string Notes { get; set; }
        public bool IsApproved { get; set; } = false;
        public bool IsArchived { get; set; } = false;
        public bool IsDismissed { get; set; } = false;
        public DateTime Created { get; set; } = DateTime.Now;

        public ProposalModel()
        {

        }

        public ProposalModel(ProposalModel proposal)
        {
            Id = proposal.Id;
            Title = proposal.Title;
        }
    }
}
