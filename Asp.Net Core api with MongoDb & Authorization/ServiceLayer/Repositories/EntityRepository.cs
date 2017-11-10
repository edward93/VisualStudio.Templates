using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using $ext_safeprojectname$.DAL.Context;
using $ext_safeprojectname$.DAL.GenericEntity;
using $ext_safeprojectname$.Infrastructure.Constants;

namespace $safeprojectname$.Repositories
{
    public class EntityRepository : IEntityRepository
    {
        protected readonly MongoDbContext MongoDbContext;

        /// <summary>
        /// The private GetCollection method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        protected IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return MongoDbContext.GetCollection<TEntity>();
        }

        public EntityRepository(MongoDbContext mongoDbContext = null)
        {
            MongoDbContext = mongoDbContext ?? new MongoDbContext();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntity> GetOne<TEntity>(string id) where TEntity : Entity
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id));
            return await GetOne(filter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<TEntity> GetOne<TEntity>(FilterDefinition<TEntity> filter) where TEntity : Entity
        {
            var collection = GetCollection<TEntity>();
            var entity = await collection.Find(filter).FirstOrDefaultAsync();
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetMany<TEntity>(IEnumerable<string> ids) where TEntity : Entity
        {
            var objectIds = ids.Select(id => new ObjectId(id)).ToList();

            var filter = Builders<TEntity>.Filter.In(EntityConstants.Id, objectIds);
            return await GetMany(filter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetMany<TEntity>(FilterDefinition<TEntity> filter) where TEntity : Entity
        {
            var collection = GetCollection<TEntity>();
            var findQuery = collection.Find(filter);
            var log = findQuery.ToString();
            var entities = await findQuery.ToListAsync();
            return entities;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetMany<TEntity>(FilterDefinition<TEntity> filter, SortDefinition<TEntity> sort, int limit) where TEntity : Entity
        {
            var collection = GetCollection<TEntity>();

            var entities =
                await collection.Find(filter ?? Builders<TEntity>.Filter.Empty)
                    .Sort(sort)
                    .Limit(limit)
                    .ToListAsync();
            return entities;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IFindFluent<TEntity, TEntity> FindCursor<TEntity>(FilterDefinition<TEntity> filter) where TEntity : Entity
        {
            var collection = GetCollection<TEntity>();
            var cursor = collection.Find(filter);
            return cursor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetAll<TEntity>() where TEntity : Entity
        {
            var collection = GetCollection<TEntity>();
            var entities = await collection.Find(new BsonDocument()).ToListAsync();
            return entities;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Exists<TEntity>(string id) where TEntity : Entity
        {
            var collection = GetCollection<TEntity>();
            var filter = Builders<TEntity>.Filter.Eq(EntityConstants.Id, ObjectId.Parse(id));
            var cursor = collection.Find(filter);
            var count = await cursor.CountAsync();
            return (count > 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<long> Count<TEntity>(string id) where TEntity : Entity
        {
            var filter = new FilterDefinitionBuilder<TEntity>().Eq("Id", id);
            return await Count(filter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<long> Count<TEntity>(FilterDefinition<TEntity> filter) where TEntity : Entity
        {
            var collection = GetCollection<TEntity>();
            var cursor = collection.Find(filter);
            var count = await cursor.CountAsync();
            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<TEntity> AddOne<TEntity>(TEntity item) where TEntity : Entity
        {
            var collection = GetCollection<TEntity>();
            item.CreatedDt = DateTime.UtcNow;
            item.UpdatedDt = DateTime.UtcNow;
            await collection.InsertOneAsync(item);
            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> AddMany<TEntity>(IEnumerable<TEntity> items) where TEntity : Entity
        {
            var collection = GetCollection<TEntity>();
            var addMany = items as TEntity[] ?? items.ToArray();

            foreach (var item in addMany)
            {
                item.CreatedDt = DateTime.UtcNow;
                item.UpdatedDt = DateTime.UtcNow;
            }
            await collection.InsertManyAsync(addMany);

            return addMany;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> DeleteOne<TEntity>(string id) where TEntity : Entity
        {
            var filter = new FilterDefinitionBuilder<TEntity>().Eq(EntityConstants.Id, ObjectId.Parse(id));
            return await DeleteOne<TEntity>(filter) ? id : null;
        }

        private async Task<bool> DeleteOne<TEntity>(FilterDefinition<TEntity> filter) where TEntity : Entity
        {
            var collection = GetCollection<TEntity>();
            var deleteRes = await collection.DeleteOneAsync(filter);
            return deleteRes.IsAcknowledged;
        }

        public async Task<IEnumerable<string>> DeleteMany<TEntity>(IEnumerable<string> ids) where TEntity : Entity
        {
            var filter = new FilterDefinitionBuilder<TEntity>().In("_id", ids);
            return await DeleteMany<TEntity>(filter) ? ids : null;
        }

        public async Task<bool> DeleteMany<TEntity>(FilterDefinition<TEntity> filter) where TEntity : Entity
        {
            var collection = GetCollection<TEntity>();
            var deleteRes = await collection.DeleteManyAsync(filter);
            return deleteRes.IsAcknowledged;
        }

        public async Task<bool> UpdateOne<TEntity>(string id, UpdateDefinition<TEntity> update) where TEntity : Entity
        {
            var filter = new FilterDefinitionBuilder<TEntity>().Eq("Id", id);
            return await UpdateOne<TEntity>(filter, update);
        }

        public async Task<bool> UpdateOne<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update) where TEntity : Entity
        {
            var collection = GetCollection<TEntity>();
            var updateRes = await collection.UpdateOneAsync(filter, update);
            return updateRes.IsAcknowledged;
        }

        public async Task<bool> UpdateMany<TEntity>(IEnumerable<string> ids, UpdateDefinition<TEntity> update) where TEntity : Entity
        {
            var filter = new FilterDefinitionBuilder<TEntity>().In("Id", ids);
            return await UpdateOne<TEntity>(filter, update);
        }

        public async Task<bool> UpdateMany<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update) where TEntity : Entity
        {
            var collection = GetCollection<TEntity>();
            var updateRes = await collection.UpdateManyAsync(filter, update);
            return updateRes.IsAcknowledged;
        }

        public async Task<TEntity> GetAndUpdateOne<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, FindOneAndUpdateOptions<TEntity, TEntity> options) where TEntity : Entity
        {
            var collection = GetCollection<TEntity>();
            return await collection.FindOneAndUpdateAsync(filter, update, options);
        }

        public async Task<bool> UpdateByIdAsync<TEntity>(string id, TEntity replacement, UpdateOptions options = null) where TEntity : Entity
        {
            var collection = GetCollection<TEntity>();

            var result = await collection.ReplaceOneAsync<TEntity>(c => c.Id == id, replacement, options);
            return result.IsAcknowledged;

        }
    }
}