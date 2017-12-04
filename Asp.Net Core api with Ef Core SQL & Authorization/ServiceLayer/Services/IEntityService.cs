using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using $ext_safeprojectname$.DAL.GenericEntity;

namespace $safeprojectname$.Services
{
    public interface IEntityService<T> where T : Entity
    {
        Task<T> GetByIdAsync(long entityId);
        Task<T> GetEntityAsync(Expression<Func<T, bool>> filter);
        Task<T> GetEntityAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllEntitiesAsync();
        Task<IEnumerable<T>> GetAllEntitiesAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllEntitiesAsync(Expression<Func<T, bool>> filter);
        Task<T> RemoveEntityAsync(long entityId);
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> CreateRangeAsync(IEnumerable<T> entities);
        Task<T> UpdateAsync(T entity);
        Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities);
    }
}