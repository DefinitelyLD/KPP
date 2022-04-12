using Microsoft.Extensions.Options;
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
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        public virtual T Create(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> CreateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual bool DeleteById(string id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> DeleteByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> FindByFilter(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<T>> FindByFilterAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public virtual T GetById(string id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public virtual T Replace(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> ReplaceAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
