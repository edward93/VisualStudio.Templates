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
    public class EntityRepository<T> : IEntityRepository<T> where T : Entity 
    {
        protected readonly IEntityDbContext DbContext;

        public EntityRepository(IEntityDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<T> GetByIdAsync(long entityId)
        {
            return await DbContext.Set<T>().FirstOrDefaultAsync(c => c.Id == entityId);
        }

        public async Task<T> GetEntityAsync(Expression<Func<T, bool>> filter)
        {
            return await DbContext.Set<T>().Where(filter).FirstOrDefaultAsync();
        }

        public async Task<dynamic> GetEntityAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> project, params Expression<Func<T, object>>[] includes)
        {
            return await DbContext.Set<T>().IncludeMultiple(includes).Where(filter).Select(project).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllEntitiesAsync()
        {
            return await DbContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllEntitiesAsync(Expression<Func<T, bool>> filter)
        {
            return await DbContext.Set<T>().Where(filter).ToListAsync();
        }

        public async Task<IEnumerable<dynamic>> GetAllEntitiesAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> project, params Expression<Func<T, object>>[] includes)
        {
            return await DbContext.Set<T>().IncludeMultiple(includes).Where(filter).Select(project).ToListAsync();
        }

        public async Task<T> RemoveEntityAsync(long entityId)
        {
            var entityToRemove = await DbContext.Set<T>().FirstOrDefaultAsync(c => c.Id == entityId);
            DbContext.Set<T>().Remove(entityToRemove);
            await DbContext.SaveChangesAsync();
            return entityToRemove;
        }

        public async Task<IEnumerable<T>> RemoveEntitiesAsync(Expression<Func<T, bool>> filter)
        {
            var entitiesToRemove = await DbContext.Set<T>().Where(filter).ToListAsync();
            DbContext.Set<T>().RemoveRange(entitiesToRemove);
            await DbContext.SaveChangesAsync();
            return entitiesToRemove;
        }

        public async Task<T> CreateAsync(T entity)
        {
            entity.CreatedDt = DateTime.Now;
            entity.UpdatedDt = DateTime.Now;
            await DbContext.Set<T>().AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> CreateRangeAsync(IEnumerable<T> entities)
        {
            var createRange = entities as T[] ?? entities.ToArray();
            await DbContext.Set<T>().AddRangeAsync(createRange);
            await DbContext.SaveChangesAsync();
            return createRange;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            entity.UpdatedDt = DateTime.Now;
            DbContext.Set<T>().Update(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities)
        {
            var updateRange = entities as T[] ?? entities.ToArray();
            DbContext.Set<T>().UpdateRange(updateRange);
            await DbContext.SaveChangesAsync();
            return updateRange;
        }

        public async Task<IEnumerable<T>> GetAllEntitiesAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>>[] includes)
        {
            return await DbContext.Set<T>().IncludeMultiple(includes).Where(filter).ToListAsync();
        }

        public async Task<T> GetEntityAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>>[] includes)
        {
            return await DbContext.Set<T>().IncludeMultiple(includes).Where(filter).FirstOrDefaultAsync();
        }
    }
}