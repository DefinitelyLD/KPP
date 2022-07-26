using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messenger.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Messenger.DAL.Repositories
{
    public abstract class BaseRepository<T, TId> : IRepository<T, TId> where T : class where TId : IComparable<TId>
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual T Create(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public async virtual Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual bool DeleteById(TId id)
        {
            T entity = _dbSet.Find(id);
            if (entity == null) 
                return false;
            _dbSet.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        public async virtual Task<bool> DeleteByIdAsync(TId id)
        {
            T entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return false;
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual T GetById(TId id)
        {
            var result = _dbSet.Find(id);
            if (result == null)
                throw new KeyNotFoundException();
            return result;
        }

        public async virtual Task<T> GetByIdAsync(TId id)
        {
            var result = await _dbSet.FindAsync(id);
            if (result == null)
                throw new KeyNotFoundException();
            return result;
        }

        public virtual T Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public async virtual Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
