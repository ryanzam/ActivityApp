using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityAppLibrary.Models
{
    public class SimpleUserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string NickName { get; set; }

        public SimpleUserModel()
        {

        }

        public SimpleUserModel(UserModel u)
        {
            Id = u.Id;
            NickName = u.NickName;
        }
    }
}
