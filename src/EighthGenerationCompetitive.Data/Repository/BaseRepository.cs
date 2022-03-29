using EighthGenerationCompetitive.Business.Entities;
using EighthGenerationCompetitive.Business.Repositories;
using EighthGenerationCompetitive.Data.Context;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Data.Repository
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly IMongoContext _mongoContext;
        protected readonly IMongoCollection<TEntity> _collection;

        private bool _disposed = false;

        protected BaseRepository(IMongoContext context)
        {
            _mongoContext = context;
            _collection = _mongoContext.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual IQueryable<TEntity> AsQueryable() => _collection.AsQueryable();

        public virtual Task DeleteByIdAsync(Guid id)
        {
            return Task.Run(() =>
            {
                _mongoContext.AddCommand(async () => await _collection.FindOneAndDeleteAsync(Builders<TEntity>.Filter.Eq("_id", id)));
            });
        }

        public virtual Task DeleteManyAsync(Expression<Func<TEntity, bool>> filter)
        {
            return Task.Run(() =>
            {
                _mongoContext.AddCommand(async () => await _collection.DeleteManyAsync(filter));
            });
        }

        public virtual Task DeleteOneAsync(Expression<Func<TEntity, bool>> filter)
        {
            return Task.Run(() =>
            {
                _mongoContext.AddCommand(async () => await _collection.DeleteOneAsync(filter));
            });
        }

        public virtual async Task<IEnumerable<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> filter)
        {
            var documents = await _collection.FindAsync(filter);
           
            return documents.ToEnumerable();
        }

        public virtual async Task<IEnumerable<TProjection>> FilterByAsync<TProjection>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TProjection>> projection)
        {
            var documents = await _collection.FindAsync(filter, new FindOptions<TEntity, TProjection>() { Projection = Builders<TEntity>.Projection.Expression(projection) });

            return documents.ToEnumerable();
        }

        public virtual IEnumerable<TProjection> FilterBy<TProjection>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TProjection>> projection)
        {
            return _collection.Find(filter).Project(projection).ToEnumerable();
        }

        public virtual IEnumerable<TEntity> FilterBy(Expression<Func<TEntity, bool>> filter)
        {
            return _collection.Find(filter).ToEnumerable();
        }

        public virtual async Task<TEntity> FindByIdAsync(Guid id)
        {
            var document = await _collection.FindAsync(Builders<TEntity>.Filter.Eq("_id", id));

            return await document.FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filter)
        {
            var document = await _collection.FindAsync(filter);

            return await document.FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> FindOneAsync(Expression<Func<TEntity, object>> selectedField, string regexExpression)
        {
            var document = await _collection.FindAsync(Builders<TEntity>.Filter.Regex(selectedField, new BsonRegularExpression(regexExpression)));

            return await document.FirstOrDefaultAsync();
        }

        public virtual async Task<TProjection> FindOneAsync<TProjection>(Expression<Func<TEntity, object>> selectedField, string regexExpression, Expression<Func<TEntity, TProjection>> projection)
        {
            var regexFilter = Builders<TEntity>.Filter.Regex(selectedField, new BsonRegularExpression(regexExpression));

            var fieldsToSelect = new FindOptions<TEntity,TProjection>() { Projection = Builders<TEntity>.Projection.Expression(projection) };

            var document = await _collection.FindAsync(regexFilter, fieldsToSelect);

            return await document.FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            var allDocuments = await _collection.FindAsync(Builders<TEntity>.Filter.Empty);

            return allDocuments.ToList();
        }

        public virtual Task InsertManyAsync(ICollection<TEntity> entities)
        {
            return Task.Run(() => _mongoContext.AddCommand(async () => await _collection.InsertManyAsync(entities)));
        }

        public virtual Task InsertOneAsync(TEntity entity)
        {
            return Task.Run(() => _mongoContext.AddCommand(async () => await _collection.InsertOneAsync(entity)));
        }

        public virtual Task ReplaceOneAsync(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, entity.Id);

            return Task.Run(() => _mongoContext.AddCommand(async () => await _collection.FindOneAndReplaceAsync(filter, entity)));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _mongoContext.Dispose();
                _disposed = true;
            }
        }
    }
}