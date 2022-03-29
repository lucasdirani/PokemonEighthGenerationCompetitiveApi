using EighthGenerationCompetitive.Data.Context;
using EighthGenerationCompetitive.Data.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace EighthGenerationCompetitive.Api.Configurations
{
    public static class MongoDbConfig
    {
        public static IServiceCollection AddMongoDbConfig(this IServiceCollection services)
        {
            MongoDbConfiguration.Configure();

            services.AddScoped<IMongoContext, MongoContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}