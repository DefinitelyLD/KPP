using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.DAL.Repositories.Interfaces
{
    public interface IRepository<T, TId> : IDisposable where T : class where TId : IComparable<TId>
    {
        IEnumerable<T> GetAll();
        T GetById(TId id);
        Task<T> GetByIdAsync(TId id);
        T Create(T entity);
        Task<T> CreateAsync(T entity);
        T Update(T entity);
        Task<T> UpdateAsync(T entity);
        bool DeleteById(TId id);
        Task<bool> DeleteByIdAsync(TId id);
    }
}