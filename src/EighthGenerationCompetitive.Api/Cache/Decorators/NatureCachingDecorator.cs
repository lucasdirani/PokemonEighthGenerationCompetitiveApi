using EighthGenerationCompetitive.Api.Cache.Keys;
using EighthGenerationCompetitive.Business.Entities.NatureAggregate;
using EighthGenerationCompetitive.Business.Monads;
using EighthGenerationCompetitive.Business.Parameters.NatureAggregate;
using EighthGenerationCompetitive.Business.Repositories;
using EighthGenerationCompetitive.Business.Utils;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Api.Cache.Decorators
{
    public class NatureCachingDecorator<T> : BaseCaching<Nature>, INatureRepository
        where T : INatureRepository
    {
        private readonly T _natureRepository;

        private bool _disposed = false;

        public NatureCachingDecorator(T natureRepository, IDistributedCache cache)
            : base(cache)
        {
            _natureRepository = natureRepository;
        }

        public IQueryable<Nature> AsQueryable()
        {
            return _natureRepository.AsQueryable();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            await _natureRepository.DeleteByIdAsync(id);
        }

        public async Task DeleteManyAsync(Expression<Func<Nature, bool>> filter)
        {
            await _natureRepository.DeleteManyAsync(filter);
        }

        public async Task DeleteOneAsync(Expression<Func<Nature, bool>> filter)
        {
            await _natureRepository.DeleteOneAsync(filter);
        }

        public IEnumerable<Nature> FilterBy(Expression<Func<Nature, bool>> filter)
        {
            return _natureRepository.FilterBy(filter);
        }

        public IEnumerable<TProjection> FilterBy<TProjection>(
            Expression<Func<Nature, bool>> filter, 
            Expression<Func<Nature, TProjection>> projection)
        {
            return _natureRepository.FilterBy(filter, projection);
        }

        public async Task<IEnumerable<Nature>> FilterByAsync(Expression<Func<Nature, bool>> filter)
        {
            return await _natureRepository.FilterByAsync(filter);
        }

        public async Task<IEnumerable<TProjection>> FilterByAsync<TProjection>(
            Expression<Func<Nature, bool>> filter, 
            Expression<Func<Nature, TProjection>> projection)
        {
            return await _natureRepository.FilterByAsync(filter, projection);
        }

        public async Task<IEnumerable<Nature>> FindAllAsync()
        {
            return await _natureRepository.FindAllAsync();
        }

        public async Task<Nature> FindByIdAsync(Guid id)
        {
            return await _natureRepository.FindByIdAsync(id);
        }

        public async Task<Nature> FindNatureByNameAsync(string natureName)
        {
            string natureCacheId = $"{NatureCacheKey.NatureByName}: {natureName}";

            Maybe<string> natureFromCache = await RetrieveResourceFromCacheAsync(natureCacheId);

            if (natureFromCache.HasValue)
            {
                return JsonSerializer.Deserialize<Nature>(natureFromCache.Value);
            }

            Nature natureFromDatabase = await _natureRepository.FindNatureByNameAsync(natureName);

            if (natureFromDatabase is not null)
            {
                await SetResourceOnCacheAsync(natureCacheId, natureFromDatabase, TimeSpan.FromDays(10));
            }

            return natureFromDatabase;
        }

        public async Task<Nature> FindNatureByNameOnlyWithOurMonstersAsync(string natureName)
        {
            return await _natureRepository.FindNatureByNameOnlyWithOurMonstersAsync(natureName);
        }

        public async Task<Nature> FindNatureByNameOnlyWithOurMonstersFormsAsync(string natureName)
        {
            return await _natureRepository.FindNatureByNameOnlyWithOurMonstersFormsAsync(natureName);
        }

        public async Task<PagedList<TProjection>> FindNaturesAsync<TProjection>(
            FindNaturesParameters<TProjection> queryParameters)
        {
            return await _natureRepository.FindNaturesAsync(queryParameters);
        }

        public async Task<Nature> FindOneAsync(Expression<Func<Nature, bool>> filter)
        {
            return await _natureRepository.FindOneAsync(filter);
        }

        public async Task<Nature> FindOneAsync(Expression<Func<Nature, object>> selectedField, string regexExpression)
        {
            return await _natureRepository.FindOneAsync(selectedField, regexExpression);
        }

        public async Task<TProjection> FindOneAsync<TProjection>(
            Expression<Func<Nature, object>> selectedField, 
            string regexExpression, 
            Expression<Func<Nature, TProjection>> projection)
        {
            return await _natureRepository.FindOneAsync(selectedField, regexExpression, projection);
        }

        public async Task InsertManyAsync(ICollection<Nature> entities)
        {
            await _natureRepository.InsertManyAsync(entities);
        }

        public async Task InsertOneAsync(Nature entity)
        {
            await _natureRepository.InsertOneAsync(entity);
        }

        public async Task ReplaceOneAsync(Nature entity)
        {
            await _natureRepository.ReplaceOneAsync(entity);
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
                _natureRepository.Dispose();
                _disposed = true;
            }
        }
    }
}