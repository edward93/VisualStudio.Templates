using System;
using $ext_safeprojectname$.DAL.GenericEntity;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace $safeprojectname$.Repositories
{
    public interface IEntityRepository
    {
        Task<T> GetByIdAsync<T>(int entityId) where T : Entity;
        Task<T> GetEntity<T>(Expression<Func<T,bool>> filter) where T : Entity;
        Task<IEnumerable<T>> GetAllEntitiesAsync<T>() where T : Entity;
        Task<IEnumerable<T>> GetAllEntitiesAsync<T>(Expression<Func<T, bool>> filter) where T : Entity;
        Task<T> RemoveEntityAsync<T>(int entityId) where T : Entity;
        Task<T> CreateAsync<T>(T entity) where T : Entity;
        Task<IEnumerable<T>> CreateRangeAsync<T>(IEnumerable<T> entities) where T : Entity;
        Task<T> UpdateAsync<T>(T entity) where T : Entity;
        Task<IEnumerable<T>> UpdateRangeAsync<T>(IEnumerable<T> entities) where T : Entity;
    }
}