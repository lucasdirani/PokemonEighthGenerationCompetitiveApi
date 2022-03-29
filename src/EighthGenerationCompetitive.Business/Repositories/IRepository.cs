using EighthGenerationCompetitive.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Business.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        IQueryable<TEntity> AsQueryable();

        IEnumerable<TEntity> FilterBy(Expression<Func<TEntity, bool>> filter);

        IEnumerable<TProjection> FilterBy<TProjection>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TProjection>> projection);

        Task<IEnumerable<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> filter);

        Task<IEnumerable<TProjection>> FilterByAsync<TProjection>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TProjection>> projection);

        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filter);

        Task<TEntity> FindOneAsync(Expression<Func<TEntity, object>> selectedField, string regexExpression);

        Task<TProjection> FindOneAsync<TProjection>(Expression<Func<TEntity, object>> selectedField, string regexExpression, Expression<Func<TEntity, TProjection>> projection);

        Task<TEntity> FindByIdAsync(Guid id);

        Task<IEnumerable<TEntity>> FindAllAsync();

        Task InsertOneAsync(TEntity entity);

        Task InsertManyAsync(ICollection<TEntity> entities);

        Task ReplaceOneAsync(TEntity entity);

        Task DeleteOneAsync(Expression<Func<TEntity, bool>> filter);

        Task DeleteByIdAsync(Guid id);

        Task DeleteManyAsync(Expression<Func<TEntity, bool>> filter);
    }
}