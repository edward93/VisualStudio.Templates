using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using $ext_safeprojectname$.DAL.GenericEntity;
using $safeprojectname$.Repositories;

namespace $safeprojectname$.Services
{
    public class EntityService : IEntityService
    {
        private readonly IEntityRepository _entityRepository;

        public EntityService(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public async Task<T> GetByIdAsync<T>(int entityId) where T : Entity
        {
            return await _entityRepository.GetByIdAsync<T>(entityId);
        }

        public async Task<T> GetEntity<T>(Expression<Func<T, bool>> filter) where T : Entity
        {
            return await _entityRepository.GetEntity(filter);
        }

        public async Task<IEnumerable<T>> GetAllEntitiesAsync<T>() where T : Entity
        {
            return await _entityRepository.GetAllEntitiesAsync<T>();
        }

        public async Task<T> RemoveEntityAsync<T>(int entityId) where T : Entity
        {
            return await _entityRepository.RemoveEntityAsync<T>(entityId);
        }

        public async Task<T> CreateAsync<T>(T entity) where T : Entity
        {
            return await _entityRepository.CreateAsync(entity);
        }

        public async Task<IEnumerable<T>> CreateRangeAsync<T>(IEnumerable<T> entities) where T : Entity
        {
            return await _entityRepository.CreateRangeAsync(entities);
        }

        public async Task<T> UpdateAsync<T>(T entity) where T : Entity
        {
            return await _entityRepository.UpdateAsync(entity);
        }

        public async Task<IEnumerable<T>> UpdateRangeAsync<T>(IEnumerable<T> entities) where T : Entity
        {
            return await _entityRepository.UpdateRangeAsync(entities);
        }
    }
}