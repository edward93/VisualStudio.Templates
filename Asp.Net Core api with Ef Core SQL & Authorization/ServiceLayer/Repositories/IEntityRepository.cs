using System;
using $ext_safeprojectname$.DAL.GenericEntity;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace $safeprojectname$.Repositories
{
    public interface IEntityRepository<T> where T : Entity
    {
        Task<T> GetByIdAsync(long entityId);
        Task<T> GetEntityAsync(Expression<Func<T,bool>> filter);
        Task<T> GetEntityAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllEntitiesAsync(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> GetAllEntitiesAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllEntitiesAsync();
        Task<T> RemoveEntityAsync(long entityId);
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> CreateRangeAsync(IEnumerable<T> entities);
        Task<T> UpdateAsync(T entity);
        Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities);
    }
}