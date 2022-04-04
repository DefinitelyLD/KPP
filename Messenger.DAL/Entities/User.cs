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
    internal class User : BaseEntity
    {
        [BsonRequired]
        public string Name { get; set; }

        [BsonRequired]
        public string Password { get; set; }

        [BsonRequired]
        public string Email { get; set; }

        public IEnumerable<ObjectId> Contacts { get; set; }

        public IEnumerable<ObjectId> BlockedUsers { get; set; }

        public IEnumerable<Chat> Chats { get; set; }
    }
}
