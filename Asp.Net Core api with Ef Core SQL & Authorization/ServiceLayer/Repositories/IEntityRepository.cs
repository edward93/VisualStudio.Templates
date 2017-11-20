using System;
using $ext_safeprojectname$.DAL.GenericEntity;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace $safeprojectname$.Repositories
{
    public interface IEntityRepository
    {
        Task<T> GetByIdAsync<T>(long entityId) where T : Entity;
        Task<T> GetEntityAsync<T>(Expression<Func<T,bool>> filter) where T : Entity;
        Task<T> GetEntityAsync<T>(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes) where T : Entity;
        Task<IEnumerable<T>> GetAllEntitiesAsync<T>(Expression<Func<T, bool>> filter) where T : Entity;
        Task<IEnumerable<T>> GetAllEntitiesAsync<T>(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes) where T : Entity;
        Task<IEnumerable<T>> GetAllEntitiesAsync<T>() where T : Entity;
        Task<T> RemoveEntityAsync<T>(long entityId) where T : Entity;
        Task<T> CreateAsync<T>(T entity) where T : Entity;
        Task<IEnumerable<T>> CreateRangeAsync<T>(IEnumerable<T> entities) where T : Entity;
        Task<T> UpdateAsync<T>(T entity) where T : Entity;
        Task<IEnumerable<T>> UpdateRangeAsync<T>(IEnumerable<T> entities) where T : Entity;
    }
}