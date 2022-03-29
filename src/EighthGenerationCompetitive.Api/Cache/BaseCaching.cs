using EighthGenerationCompetitive.Api.Extensions;
using EighthGenerationCompetitive.Business.Monads;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Api.Cache
{
    public abstract class BaseCaching<TResource> where TResource : class
    {
        private readonly IDistributedCache _cache;

        protected BaseCaching(IDistributedCache cache)
        {
            _cache = cache;
        }

        protected async Task<Maybe<string>> RetrieveResourceFromCacheAsync(string cacheKey) =>
            await _cache.GetStringAsync(cacheKey);

        protected async Task SetResourceOnCacheAsync(
            string cacheKey,
            TResource resource,
            TimeSpan cacheExpiration)
        {
            var cacheOptions = new DistributedCacheEntryOptions();

            cacheOptions.SetAbsoluteExpiration(cacheExpiration);

            string jsonResource = resource.ToJson();

            await _cache.SetStringAsync(cacheKey, jsonResource, cacheOptions);
        }
    }
}