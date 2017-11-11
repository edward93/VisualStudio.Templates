using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using $ext_safeprojectname$.DAL.GenericEntity;

namespace $safeprojectname$.Services
{
    public interface IEntityService
    {
        Task<T> GetByIdAsync<T>(int entityId) where T : Entity;
        Task<T> GetEntity<T>(Expression<Func<T, bool>> filter) where T : Entity;
        Task<IEnumerable<T>> GetAllEntitiesAsync<T>() where T : Entity;
        Task<T> RemoveEntityAsync<T>(int entityId) where T : Entity;
        Task<T> CreateAsync<T>(T entity) where T : Entity;
        Task<IEnumerable<T>> CreateRangeAsync<T>(IEnumerable<T> entities) where T : Entity;
        Task<T> UpdateAsync<T>(T entity) where T : Entity;
        Task<IEnumerable<T>> UpdateRangeAsync<T>(IEnumerable<T> entities) where T : Entity;
    }
}