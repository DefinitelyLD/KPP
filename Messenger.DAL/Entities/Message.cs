using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Messenger.DAL.Entities
{
    internal class Message : BaseEntity
    {
        [BsonRequired]
        public Chat Chat { get; set; }

        [BsonRequired]
        public User User { get; set; }

        public string? Text { get; set; }

        public IEnumerable<Image> Images { get; set; }
    }
}
