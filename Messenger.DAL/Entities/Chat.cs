using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.DAL.Entities
{
    internal class Chat : BaseEntity
    {
        [BsonRequired]
        public string Topic { get; set; }

        public string? Password { get; set; }

        public ObjectId Owner { get; set; }
    }
}
