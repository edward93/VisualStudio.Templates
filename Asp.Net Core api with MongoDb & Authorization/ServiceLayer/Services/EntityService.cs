using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
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

        public async Task<TEntity> GetOne<TEntity>(string id) where TEntity : Entity
        {
            return await _entityRepository.GetOne<TEntity>(id);
        }

        public async Task<TEntity> GetOne<TEntity>(FilterDefinition<TEntity> filter) where TEntity : Entity
        {
            return await _entityRepository.GetOne(filter);
        }

        public async Task<IEnumerable<TEntity>> GetMany<TEntity>(IEnumerable<string> ids) where TEntity : Entity
        {
            return await _entityRepository.GetMany<TEntity>(ids);
        }

        public async Task<IEnumerable<TEntity>> GetMany<TEntity>(FilterDefinition<TEntity> filter) where TEntity : Entity
        {
            return await _entityRepository.GetMany(filter);
        }

        public async Task<IEnumerable<TEntity>> GetMany<TEntity>(FilterDefinition<TEntity> filter, SortDefinition<TEntity> sort, int limit) where TEntity : Entity
        {
            return await _entityRepository.GetMany(filter, sort, limit);
        }

        public IFindFluent<TEntity, TEntity> FindCursor<TEntity>(FilterDefinition<TEntity> filter) where TEntity : Entity
        {
            return _entityRepository.FindCursor(filter);
        }

        public async Task<IEnumerable<TEntity>> GetAll<TEntity>() where TEntity : Entity
        {
            return await _entityRepository.GetAll<TEntity>();
        }

        public async Task<bool> Exists<TEntity>(string id) where TEntity : Entity
        {
            return await _entityRepository.Exists<TEntity>(id);
        }

        public async Task<long> Count<TEntity>(string id) where TEntity : Entity
        {
            return await _entityRepository.Count<TEntity>(id);
        }

        public async Task<long> Count<TEntity>(FilterDefinition<TEntity> filter) where TEntity : Entity
        {
            return await _entityRepository.Count(filter);
        }

        public async Task<TEntity> AddOne<TEntity>(TEntity item) where TEntity : Entity
        {
            return await _entityRepository.AddOne(item);
        }

        public async Task<IEnumerable<TEntity>> AddMany<TEntity>(IEnumerable<TEntity> items) where TEntity : Entity
        {
            return await _entityRepository.AddMany(items);
        }

        public async Task<string> DeleteOne<TEntity>(string id) where TEntity : Entity
        {
            return await _entityRepository.DeleteOne<TEntity>(id);
        }

        public async Task<IEnumerable<string>> DeleteMany<TEntity>(IEnumerable<string> ids) where TEntity : Entity
        {
            return await _entityRepository.DeleteMany<TEntity>(ids);
        }

        public async Task<bool> DeleteMany<TEntity>(FilterDefinition<TEntity> filter) where TEntity : Entity
        {
            return await _entityRepository.DeleteMany(filter);
        }

        public async Task<bool> UpdateOne<TEntity>(string id, UpdateDefinition<TEntity> update) where TEntity : Entity
        {
            return await _entityRepository.UpdateOne(id, update);
        }

        public async Task<bool> UpdateOne<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update) where TEntity : Entity
        {
            return await _entityRepository.UpdateOne(filter, update);
        }

        public async Task<bool> UpdateMany<TEntity>(IEnumerable<string> ids, UpdateDefinition<TEntity> update) where TEntity : Entity
        {
            return await _entityRepository.UpdateMany(ids, update);
        }

        public async Task<bool> UpdateMany<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update) where TEntity : Entity
        {
            return await _entityRepository.UpdateMany(filter, update);
        }

        public async Task<TEntity> GetAndUpdateOne<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, FindOneAndUpdateOptions<TEntity, TEntity> options) where TEntity : Entity
        {
            return await _entityRepository.GetAndUpdateOne(filter, update, options);
        }

        public async Task<bool> UpdateByIdAsync<TEntity>(string id, TEntity replacement, UpdateOptions options = null) where TEntity : Entity
        {
            return await _entityRepository.UpdateByIdAsync(id, replacement, options);
        }
    }
}