using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.DAL.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        T GetById(string id);
        Task<T> GetByIdAsync(string id);
        IEnumerable<T> FindByFilter(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> FindByFilterAsync(Expression<Func<T, bool>> expression);
        T Create(T entity);
        Task<T> CreateAsync(T entity);
        T Replace(T entity);
        Task<T> ReplaceAsync(T entity);
        bool DeleteById(string id);
        Task<bool> DeleteByIdAsync(string id);
    }
}