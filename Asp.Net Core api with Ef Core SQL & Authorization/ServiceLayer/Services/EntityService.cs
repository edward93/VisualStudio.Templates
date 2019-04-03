using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using $ext_safeprojectname$.DAL.GenericEntity;
using $safeprojectname$.Repositories;

namespace $safeprojectname$.Services
{
    public class EntityService<T> : IEntityService<T> where T : Entity
    {
        private readonly IEntityRepository<T> _entityRepository;

        public EntityService(IEntityRepository<T> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public async Task<T> GetByIdAsync(long entityId)
        {
            return await _entityRepository.GetByIdAsync(entityId);
        }

        public async Task<T> GetEntityAsync(Expression<Func<T, bool>> filter)
        {
            return await _entityRepository.GetEntityAsync(filter);
        }

        public async Task<T> GetEntityAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            return await _entityRepository.GetEntityAsync(filter, includes);
        }

        public async Task<dynamic> GetEntityAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> project, params Expression<Func<T, object>>[] includes)
        {
            return await _entityRepository.GetEntityAsync(filter, project, includes);
        }

        public async Task<IEnumerable<T>> GetAllEntitiesAsync()
        {
            return await _entityRepository.GetAllEntitiesAsync();
        }

        public async Task<IEnumerable<T>> GetAllEntitiesAsync(Expression<Func<T, bool>> filter)
        {
            return await _entityRepository.GetAllEntitiesAsync(filter);
        }

        public async Task<IEnumerable<T>> GetAllEntitiesAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            return await _entityRepository.GetAllEntitiesAsync(filter, includes);
        }

        public async Task<IEnumerable<dynamic>> GetAllEntitiesAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> project, params Expression<Func<T, object>>[] includes)
        {
            return await _entityRepository.GetAllEntitiesAsync(filter, project, includes);
        }

        public async Task<T> RemoveEntityAsync(long entityId)
        {
            return await _entityRepository.RemoveEntityAsync(entityId);
        }

        public async Task<IEnumerable<T>> RemoveEntitiesAsync(Expression<Func<T, bool>> filter)
        {
            return await _entityRepository.RemoveEntitiesAsync(filter);
        }

        public async Task<T> CreateAsync(T entity)
        {
            return await _entityRepository.CreateAsync(entity);
        }

        public async Task<IEnumerable<T>> CreateRangeAsync(IEnumerable<T> entities)
        {
            return await _entityRepository.CreateRangeAsync(entities);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            return await _entityRepository.UpdateAsync(entity);
        }

        public async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities)
        {
            return await _entityRepository.UpdateRangeAsync(entities);
        }
    }
}