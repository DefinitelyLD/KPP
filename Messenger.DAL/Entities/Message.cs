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
    [BsonCollection("Messeges")]
    public class Message : BaseEntity
    {
        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ChatId { get; set; }

        [BsonRequired]
        public User User { get; set; }

        public string Text { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ImageId { get; set; } // we will use MongoDB.GridFS for uploading images.
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
