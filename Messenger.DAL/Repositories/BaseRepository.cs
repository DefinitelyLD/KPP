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
using Microsoft.EntityFrameworkCore;

namespace Messenger.DAL.Repositories
{
    public abstract class BaseRepository<T, TId> : IRepository<T, TId> where T : BaseEntity<TId> where TId : IComparable<TId>
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

        public virtual IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public virtual T GetById(TId id)
        {
            return _dbSet.AsNoTracking().Where(e => e.Id.CompareTo(id) == 0).SingleOrDefault();
        }

        public async virtual Task<T> GetByIdAsync(TId id)
        {
            return await _dbSet.AsNoTracking().Where(e => e.Id.CompareTo(id) == 0).SingleOrDefaultAsync();
        }

        public virtual T Update(T entity)
        {
            var oldEntity = _dbSet.Find(entity.Id);
            _context.Entry(oldEntity).CurrentValues.SetValues(entity);
            _context.SaveChanges();
            return entity;
        }

        public async virtual Task<T> UpdateAsync(T entity)
        {
            var oldEntity = await _dbSet.FindAsync(entity.Id);
            _context.Entry(oldEntity).CurrentValues.SetValues(entity);
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
