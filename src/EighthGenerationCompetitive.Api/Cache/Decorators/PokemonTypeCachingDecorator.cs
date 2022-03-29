using EighthGenerationCompetitive.Api.Cache.Keys;
using EighthGenerationCompetitive.Business.Monads;
using EighthGenerationCompetitive.Business.Parameters.TypeAggregate;
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
    public class PokemonTypeCachingDecorator<T> : BaseCaching<Business.Entities.TypeAggregate.Type>, IPokemonTypeRepository
        where T : IPokemonTypeRepository
    {
        private readonly T _pokemonTypeRepository;

        private bool _disposed = false;

        public PokemonTypeCachingDecorator(T pokemonTypeRepository, IDistributedCache cache)
            : base(cache)
        {
            _pokemonTypeRepository = pokemonTypeRepository;
        }

        public IQueryable<Business.Entities.TypeAggregate.Type> AsQueryable()
        {
            return _pokemonTypeRepository.AsQueryable();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            await _pokemonTypeRepository.DeleteByIdAsync(id);
        }

        public async Task DeleteManyAsync(Expression<Func<Business.Entities.TypeAggregate.Type, bool>> filter)
        {
            await _pokemonTypeRepository.DeleteManyAsync(filter);
        }

        public async Task DeleteOneAsync(Expression<Func<Business.Entities.TypeAggregate.Type, bool>> filter)
        {
            await _pokemonTypeRepository.DeleteOneAsync(filter);
        }

        public IEnumerable<Business.Entities.TypeAggregate.Type> FilterBy(Expression<Func<Business.Entities.TypeAggregate.Type, bool>> filter)
        {
            return _pokemonTypeRepository.FilterBy(filter);
        }

        public IEnumerable<TProjection> FilterBy<TProjection>(
            Expression<Func<Business.Entities.TypeAggregate.Type, bool>> filter, 
            Expression<Func<Business.Entities.TypeAggregate.Type, TProjection>> projection)
        {
            return _pokemonTypeRepository.FilterBy(filter, projection);
        }

        public async Task<IEnumerable<Business.Entities.TypeAggregate.Type>> FilterByAsync(Expression<Func<Business.Entities.TypeAggregate.Type, bool>> filter)
        {
            return await _pokemonTypeRepository.FilterByAsync(filter);
        }

        public async Task<IEnumerable<TProjection>> FilterByAsync<TProjection>(
            Expression<Func<Business.Entities.TypeAggregate.Type, bool>> filter, 
            Expression<Func<Business.Entities.TypeAggregate.Type, TProjection>> projection)
        {
            return await _pokemonTypeRepository.FilterByAsync(filter, projection);
        }

        public async Task<IEnumerable<Business.Entities.TypeAggregate.Type>> FindAllAsync()
        {
            return await _pokemonTypeRepository.FindAllAsync();
        }

        public async Task<Business.Entities.TypeAggregate.Type> FindByIdAsync(Guid id)
        {
            return await _pokemonTypeRepository.FindByIdAsync(id);
        }

        public async Task<Business.Entities.TypeAggregate.Type> FindOneAsync(Expression<Func<Business.Entities.TypeAggregate.Type, bool>> filter)
        {
            return await _pokemonTypeRepository.FindOneAsync(filter);
        }

        public async Task<Business.Entities.TypeAggregate.Type> FindOneAsync(
            Expression<Func<Business.Entities.TypeAggregate.Type, object>> selectedField, 
            string regexExpression)
        {
            return await _pokemonTypeRepository.FindOneAsync(selectedField, regexExpression);
        }

        public async Task<TProjection> FindOneAsync<TProjection>(
            Expression<Func<Business.Entities.TypeAggregate.Type, object>> selectedField, 
            string regexExpression, 
            Expression<Func<Business.Entities.TypeAggregate.Type, TProjection>> projection)
        {
            return await _pokemonTypeRepository.FindOneAsync(selectedField, regexExpression, projection);
        }

        public async Task<Business.Entities.TypeAggregate.Type> FindPokemonTypeByNameAsync(string typeName)
        {
            string pokemonTypeCacheId = $"{PokemonTypeCacheKey.PokemonTypeByName}: {typeName}";

            Maybe<string> pokemonTypeFromCache = await RetrieveResourceFromCacheAsync(pokemonTypeCacheId);

            if (pokemonTypeFromCache.HasValue)
            {
                return JsonSerializer.Deserialize<Business.Entities.TypeAggregate.Type>(pokemonTypeFromCache.Value);
            }

            Business.Entities.TypeAggregate.Type pokemonTypeFromDatabase = await _pokemonTypeRepository.FindPokemonTypeByNameAsync(typeName);

            if (pokemonTypeFromDatabase is not null)
            {
                await SetResourceOnCacheAsync(pokemonTypeCacheId, pokemonTypeFromDatabase, TimeSpan.FromDays(10));
            }

            return pokemonTypeFromDatabase;
        }

        public async Task<Business.Entities.TypeAggregate.Type> FindPokemonTypeByNameOnlyWithOurMonstersAsync(string typeName)
        {
            return await _pokemonTypeRepository.FindPokemonTypeByNameOnlyWithOurMonstersAsync(typeName);
        }

        public async Task<Business.Entities.TypeAggregate.Type> FindPokemonTypeByNameOnlyWithOurMonstersFormsAsync(string typeName)
        {
            return await _pokemonTypeRepository.FindPokemonTypeByNameOnlyWithOurMonstersFormsAsync(typeName);
        }

        public async Task<Business.Entities.TypeAggregate.Type> FindPokemonTypeByNameOnlyWithOurMovesAsync(string typeName)
        {
            return await _pokemonTypeRepository.FindPokemonTypeByNameOnlyWithOurMovesAsync(typeName);
        }

        public async Task<PagedList<TProjection>> FindPokemonTypesAsync<TProjection>(
            FindPokemonTypesParameters<TProjection> queryParameters)
        {
            return await _pokemonTypeRepository.FindPokemonTypesAsync(queryParameters);
        }

        public async Task InsertManyAsync(ICollection<Business.Entities.TypeAggregate.Type> entities)
        {
            await _pokemonTypeRepository.InsertManyAsync(entities);
        }

        public async Task InsertOneAsync(Business.Entities.TypeAggregate.Type entity)
        {
            await _pokemonTypeRepository.InsertOneAsync(entity);
        }

        public async Task ReplaceOneAsync(Business.Entities.TypeAggregate.Type entity)
        {
            await _pokemonTypeRepository.ReplaceOneAsync(entity);
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
                _pokemonTypeRepository.Dispose();
                _disposed = true;
            }
        }
    }
}