
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UserCabinet.Data.IRepositories
{
    public interface IGenericRepository<TSource> where TSource : class
    {
        Task<TSource> AddAsync(TSource entity);
        Task DeleteAsync(Expression<Func<TSource, bool>> expression);
        Task<TSource> GetAsync(Expression<Func<TSource, bool>> expression = null, string include = null);
        IQueryable<TSource> GetAll(Expression<Func<TSource, bool>> expression = null, string include = null, bool isTracking = true);
        Task<TSource> UpdateAsync(TSource entity);
    }
}
