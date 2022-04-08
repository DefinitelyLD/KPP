using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.DAL.Entities
{
    [BsonCollection("Chats")]
    public class Chat : BaseEntity
    {
        [BsonRequired]
        public string Topic { get; set; }

        public string? Password { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        public string OwnerId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public IEnumerable<string> AdminsIds { get; set; }
    }
}
