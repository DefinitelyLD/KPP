using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.DAL.Context
{
    public class DbContext : IDisposable
    {
        private IMongoDatabase Database { get; set; }

        public DbContext(IMessengerDatabaseSettings settings)
        {

            Database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(Type documentType)
        {
            return Database.GetCollection<T>(GetCollectionName(documentType));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType
                .GetCustomAttributes(typeof(BsonCollectionAttribute), true)
                .FirstOrDefault())?
                .CollectionName;
        }
    }
}
