using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using $ext_safeprojectname$.DAL.GenericEntity;

namespace $safeprojectname$.Services
{
    public interface IEntityService<T> where T : Entity
    {
        // Get single entity
        Task<T> GetByIdAsync(long entityId);
        Task<T> GetEntityAsync(Expression<Func<T, bool>> filter);
        Task<T> GetEntityAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        Task<dynamic> GetEntityAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> project, params Expression<Func<T, object>>[] includes);

        // Get multiple entities
        Task<IEnumerable<T>> GetAllEntitiesAsync();
        Task<IEnumerable<T>> GetAllEntitiesAsync(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> GetAllEntitiesAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<dynamic>> GetAllEntitiesAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> project, params Expression<Func<T, object>>[] includes);

        // Remove entities
        Task<T> RemoveEntityAsync(long entityId);
        Task<IEnumerable<T>> RemoveEntitiesAsync(Expression<Func<T, bool>> filter);

        // Create entities
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> CreateRangeAsync(IEnumerable<T> entities);

        // Update entities
        Task<T> UpdateAsync(T entity);
        Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities);
    }
}