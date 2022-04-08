using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Messenger.DAL.Entities;
using Messenger.DAL.Repositories.Interfaces;
using Messenger.DAL.Context;

namespace Messenger.DAL.Repositories
{
    public abstract class BaseMongoRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly IMongoCollection<T> _collection;

        public BaseMongoRepository(DbContext context)
        {
            _collection = context.GetCollection<T>(typeof(T));
        }

        public virtual T Create(T entity)
        {
            _collection.InsertOne(entity);
            return entity;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public virtual bool DeleteById(string id)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
            var result = _collection.DeleteOne(filter);
            return result.DeletedCount > 0;
        }

        public async virtual Task<bool> DeleteByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
            var result = await _collection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }

        public virtual IEnumerable<T> FindByFilter(Expression<Func<T, bool>> expression)
        {
            return _collection.Find(expression).ToList();
        }

        public virtual async Task<IEnumerable<T>> FindByFilterAsync(Expression<Func<T, bool>> expression)
        {
            return await _collection.Find(expression).ToListAsync();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _collection.Find(_ => true).ToList();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public virtual T GetById(string id)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
            return _collection.Find(filter).FirstOrDefault();
        }

        public virtual async Task<T> GetByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq(entity => entity.Id, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public virtual T Replace(T entity)
        {
            var filter = Builders<T>.Filter.Eq(filter => filter.Id, entity.Id);
            var result = _collection.ReplaceOne(filter, entity);
            if (!(result.ModifiedCount > 0))
            {
                throw new Exception("Replace error");
            }
            return entity;
        }

        public async virtual Task<T> ReplaceAsync(T entity)
        {
            var filter = Builders<T>.Filter.Eq(filter => filter.Id, entity.Id);
            var result = await _collection.ReplaceOneAsync(filter, entity);
            if (!(result.ModifiedCount > 0))
            {
                throw new Exception("Replace error");
            }
            return entity;
        }
    }
}
