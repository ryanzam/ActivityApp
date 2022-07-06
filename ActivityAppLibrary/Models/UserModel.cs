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

		public List<SimpleProposalModel> Proposals { get; set; } = new();
		public List<SimpleProposalModel> ProposalsThumbsUpped { get; set; } = new();
	}
}
