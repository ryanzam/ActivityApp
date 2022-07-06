using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityAppLibrary.Models
{
    public class SimpleProposalModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }

        public SimpleProposalModel()
        {

        }

        public SimpleProposalModel(ProposalModel p)
        {
            p.Title = p.Title;
            p.Id = p.Id;
        }
    }
}
