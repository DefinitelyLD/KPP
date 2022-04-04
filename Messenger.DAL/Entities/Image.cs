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
    public class Image : BaseEntity
    {
        [BsonRequired]
        public string ImageUrl { get; set; }
    }
}
