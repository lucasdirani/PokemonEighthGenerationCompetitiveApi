using EighthGenerationCompetitive.Business.Utils;
using EighthGenerationCompetitive.Data.Sorting;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Data.Extensions
{
    internal static class MongoQueryableExtensions
    {
        public static async Task<PagedList<TEntity>> ToPagedListAsync<TEntity>(
            this IMongoQueryable<TEntity> source,
            int pageNumber,
            int pageSize) 
        {
            var count = await source.CountAsync();

            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedList<TEntity>(items, count, pageNumber, pageSize);
        }

        public static IMongoQueryable<TEntity> OrderBy<TEntity>(this IMongoQueryable<TEntity> mongoQueryable, string sorts)
        {
            var sort = new SortCollection<TEntity>(sorts);

            return sort.Apply(mongoQueryable);
        }

        public static IMongoQueryable<TEntity> OrderBy<TEntity>(this IMongoQueryable<TEntity> mongoQueryable, params string[] sorts)
        {
            var sort = new SortCollection<TEntity>(sorts);

            return sort.Apply(mongoQueryable);
        }
    }
}