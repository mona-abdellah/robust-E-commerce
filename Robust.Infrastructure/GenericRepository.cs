using Microsoft.EntityFrameworkCore;
using Robust.App.Contracts;
using Robust.Context;
using Robust.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Robust.Infrastructure
{
    public class GenericRepository<T,TId> : IGenericRepo<T, TId> where T:BaseEntity<TId>
    {
        private readonly RobustContext robustContext;
        private readonly DbSet<T> dbSet;
        public GenericRepository(RobustContext _robustContext)
        {
            robustContext = _robustContext;
            dbSet = robustContext.Set<T>();
        }
        public async Task<T> CreateAsync(T entity)
        {
            return (await dbSet.AddAsync(entity)).Entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            return  robustContext.Remove(entity).Entity;
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            return  dbSet;
        }

        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predict)
        {
            return  dbSet.Where(predict);
        }

        public async Task<T> GetOneAsync(TId id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await robustContext.SaveChangesAsync();
        }

        public Task<T> UpdateAsync(T entity)
        {
            return Task.FromResult(dbSet.Update(entity).Entity);
        }
    }
}
