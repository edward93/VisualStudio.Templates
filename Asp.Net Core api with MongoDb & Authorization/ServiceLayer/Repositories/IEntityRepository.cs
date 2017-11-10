using $ext_safeprojectname$.DAL.GenericEntity;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace $safeprojectname$.Repositories
{
    public interface IEntityRepository
    {
        /// <summary>
        /// A generic GetOne method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetOne<TEntity>(string id) where TEntity : Entity;

        /// <summary>
        /// A generic GetOne method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<TEntity> GetOne<TEntity>(FilterDefinition<TEntity> filter) where TEntity : Entity;

        /// <summary>
        /// A generic get many method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetMany<TEntity>(IEnumerable<string> ids) where TEntity : Entity;

        /// <summary>
        /// A generic get many method with filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetMany<TEntity>(FilterDefinition<TEntity> filter) where TEntity : Entity;

        /// <summary>
        /// A generic get many method with filter projection and sorting
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetMany<TEntity>(FilterDefinition<TEntity> filter, SortDefinition<TEntity> sort, int limit) where TEntity : Entity;

        /// <summary>
        /// GetMany with filter and projection
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns>A cursor for the query</returns>
        IFindFluent<TEntity, TEntity> FindCursor<TEntity>(FilterDefinition<TEntity> filter) where TEntity : Entity;

        /// <summary>
        /// A generic get all method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAll<TEntity>() where TEntity : Entity;

        /// <summary>
        /// A generic Exists method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> Exists<TEntity>(string id) where TEntity : Entity;

        /// <summary>
        /// A generic count method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<long> Count<TEntity>(string id) where TEntity : Entity;

        /// <summary>
        /// A generic count method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<long> Count<TEntity>(FilterDefinition<TEntity> filter) where TEntity : Entity;

        /// <summary>
        /// A generic Add One method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<TEntity> AddOne<TEntity>(TEntity item) where TEntity : Entity;

        /// <summary>
        /// A generic Add Many method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> AddMany<TEntity>(IEnumerable<TEntity> items) where TEntity : Entity;

        /// <summary>
        /// A generic delete one method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> DeleteOne<TEntity>(string id) where TEntity : Entity;

        /// <summary>
        /// A generic delete many method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> DeleteMany<TEntity>(IEnumerable<string> ids) where TEntity : Entity;

        /// <summary>
        /// A generic for delete many using filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<bool> DeleteMany<TEntity>(FilterDefinition<TEntity> filter) where TEntity : Entity;

        #region Update
        /// <summary>
        /// UpdateOne by id
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        Task<bool> UpdateOne<TEntity>(string id, UpdateDefinition<TEntity> update) where TEntity : Entity;

        /// <summary>
        /// UpdateOne with filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        Task<bool> UpdateOne<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update) where TEntity : Entity;

        /// <summary>
        /// UpdateMany with Ids
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="ids"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        Task<bool> UpdateMany<TEntity>(IEnumerable<string> ids, UpdateDefinition<TEntity> update) where TEntity : Entity;

        /// <summary>
        /// UpdateMany with filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        Task<bool> UpdateMany<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update) where TEntity : Entity;
        #endregion Update

        #region Find And Update

        /// <summary>
        /// GetAndUpdateOne with filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<TEntity> GetAndUpdateOne<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, FindOneAndUpdateOptions<TEntity, TEntity> options) where TEntity : Entity;

        #endregion

        Task<bool> UpdateByIdAsync<TEntity>(string id, TEntity replacement, UpdateOptions options = null) where TEntity : Entity;
    }
}