
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UserCabinet.Data.DbContexts;
using UserCabinet.Data.IRepositories;

namespace UserCabinet.Data.Repositories
{
#pragma warning disable
    public class GenericRepository<TSource> : IGenericRepository<TSource> where TSource : class
    {
        private readonly UserCabinetDbContext _dbContext;
        private readonly DbSet<TSource> _dbSet;

        public GenericRepository(UserCabinetDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TSource>();
        }

        public async Task<TSource> AddAsync(TSource entity)
        {
            var entry = await _dbSet.AddAsync(entity);

            return entry.Entity;
        }

        public async Task DeleteAsync(Expression<Func<TSource, bool>> expression)
        {
            var entity = await GetAsync(expression);

            _dbSet.Remove(entity);
        }

        public IQueryable<TSource> GetAll(Expression<Func<TSource, bool>> expression = null, string include = null, bool isTracking = true)
        {
            IQueryable<TSource> query = expression is null ? _dbSet : _dbSet.Where(expression);

            if (!string.IsNullOrEmpty(include))
                query = query.Include(include);

            if (!isTracking)
                query = query.AsNoTracking();

            return query;
        }

        public async Task<TSource> GetAsync(Expression<Func<TSource, bool>> expression = null, string include = null)
            => await GetAll(expression, include).FirstOrDefaultAsync();

        public async Task<TSource> UpdateAsync(TSource entity)
           => _dbSet.Update(entity).Entity;
    }
}
