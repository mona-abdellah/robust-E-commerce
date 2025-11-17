using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Robust.App.Contracts
{
    public interface IGenericRepo<T,TId>
    {
        public Task<T> CreateAsync(T entity);
        public Task<T> UpdateAsync(T entity);
        public Task<T> DeleteAsync(T entity);
        public Task<IQueryable<T>> GetAllAsync();
        public Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predict);
        public Task<T> GetOneAsync(TId id);
        public Task<int> SaveChangesAsync();
    }
}
