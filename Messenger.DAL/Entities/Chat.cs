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
        public User Owner { get; set; }

        public IEnumerable<User> Admins { get; set; }
    }
}
