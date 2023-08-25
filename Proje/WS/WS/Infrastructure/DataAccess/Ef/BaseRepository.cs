using Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.DataAccess.EF
{
    public class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity>
      where TEntity : class, IEntity
      where TContext : DbContext, new()
    {
        public async Task DeleteAsync(TEntity entity)
        {
            using var ctx = new TContext();

            ctx.Set<TEntity>().Update(entity);
            await ctx.SaveChangesAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params string[] includeList)
        {
            using var ctx = new TContext();

            IQueryable<TEntity> dbSet = ctx.Set<TEntity>();

            if (includeList.Length > 0)
            {
                foreach (var item in includeList)
                {
                    dbSet = dbSet.Include(item);
                }
            }

            return await dbSet.SingleOrDefaultAsync(predicate);
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null,
          params string[] includeList)
        {
            using (var ctx = new TContext())
            {
                IQueryable<TEntity> dbSet = ctx.Set<TEntity>();

                if (includeList.Length > 0)
                {
                    foreach (var item in includeList)
                    {
                        dbSet = dbSet.Include(item);
                    }
                }

                if (predicate == null)
                    return await dbSet.ToListAsync();

                return await dbSet.Where(predicate).ToListAsync();

            }
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            using var ctx = new TContext();

            var entityEntry = await ctx.Set<TEntity>().AddAsync(entity);

            await ctx.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            using var ctx = new TContext();

            ctx.Set<TEntity>().Update(entity);
            await ctx.SaveChangesAsync();
        }
    }
}
