using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EighthGenerationCompetitive.Api.Configurations
{
    public static class ApiCachingConfig
    {
        public static IServiceCollection AddApiCachingConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                options.InstanceName = "EighthGenerationSmogonCompetitivePokemonApi";
            });

            return services;
        }
    }
}