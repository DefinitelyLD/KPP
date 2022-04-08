using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;


namespace Messenger.DAL.Entities
{
    [BsonCollection("Users")]
    public class User : BaseEntity
    {
        [BsonRequired]
        public string Name { get; set; }

        [BsonRequired]
        public string Password { get; set; }

        [BsonRequired]
        public string Email { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public IEnumerable<string> ContactsIds { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public IEnumerable<string> BlockedUsersIds { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public IEnumerable<string> ChatsIds { get; set; }
    }
}
