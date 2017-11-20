using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using $ext_safeprojectname$.DAL.Context;
using $ext_safeprojectname$.DAL.GenericEntity;
using $ext_safeprojectname$.DAL.Helpers;


namespace $safeprojectname$.Repositories
{
    public class EntityRepository : IEntityRepository
    {
        protected readonly IEntityDbContext DbContext;

        public EntityRepository(IEntityDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<T> GetByIdAsync<T>(long entityId) where T : Entity
        {
            return await DbContext.Set<T>().FirstOrDefaultAsync(c => c.Id == entityId);
        }

        public async Task<T> GetEntityAsync<T>(Expression<Func<T, bool>> filter) where T : Entity
        {
            return await DbContext.Set<T>().Where(filter).FirstOrDefaultAsync();
        }

        public async Task<T> GetEntityAsync<T>(Expression<Func<T, bool>> filter, params Expression<Func<T, Object>>[] includes) where T : Entity
        {
            return await DbContext.Set<T>().IncludeMultiple(includes).Where(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllEntitiesAsync<T>() where T : Entity
        {
            return await DbContext.Set<T>().ToListAsync();
        }
		
		public async Task<IEnumerable<T>> GetAllEntitiesAsync<T>(Expression<Func<T, bool>> filter) where T : Entity
        {
            return await DbContext.Set<T>().Where(filter).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllEntitiesAsync<T>(Expression<Func<T, bool>> filter, params Expression<Func<T, Object>>[] includes) where T : Entity
        {
            return await DbContext.Set<T>().IncludeMultiple(includes).Where(filter).ToListAsync();
        }

        public async Task<T> RemoveEntityAsync<T>(long entityId) where T : Entity
        {
            var entityToRemove = await DbContext.Set<T>().FirstOrDefaultAsync(c => c.Id == entityId);
            DbContext.Set<T>().Remove(entityToRemove);
            await DbContext.SaveChangesAsync();
            return entityToRemove;
        }

        public async Task<T> CreateAsync<T>(T entity) where T : Entity
        {
            entity.CreatedDt = DateTime.Now;
            entity.UpdatedDt = DateTime.Now;
            await DbContext.Set<T>().AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> CreateRangeAsync<T>(IEnumerable<T> entities) where T : Entity
        {
            var createRange = entities as T[] ?? entities.ToArray();
            await DbContext.Set<T>().AddRangeAsync(createRange);
            await DbContext.SaveChangesAsync();
            return createRange;
        }

        public async Task<T> UpdateAsync<T>(T entity) where T : Entity
        {
            entity.UpdatedDt = DateTime.Now;
            DbContext.Set<T>().Update(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> UpdateRangeAsync<T>(IEnumerable<T> entities) where T : Entity
        {
            var updateRange = entities as T[] ?? entities.ToArray();
            DbContext.Set<T>().UpdateRange(updateRange);
            await DbContext.SaveChangesAsync();
            return updateRange;
        }
    }
}