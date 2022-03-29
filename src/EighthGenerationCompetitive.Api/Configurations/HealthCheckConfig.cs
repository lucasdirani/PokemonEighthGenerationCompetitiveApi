using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EighthGenerationCompetitive.Api.Configurations
{
    public static class HealthCheckConfig
    {
        public static IServiceCollection AddHealthCheckConfiguration(
            this IServiceCollection services,
            IWebHostEnvironment environment,
            IConfiguration configuration)
        {
            string mongoConnectionString = configuration.GetSection("MongoSettings").GetSection("Connection").Value;
            string redisConnectionString = configuration.GetConnectionString("Redis");
            string sqlConnectionString = configuration.GetConnectionString("SqlServer");

            services
                .AddHealthChecks()
                .AddMongoDb(mongoConnectionString)
                .AddSqlServer(sqlConnectionString)
                .AddRedis(redisConnectionString)
                .CheckOnlyWhen(Registrations.Redis, environment.IsProduction());

            services.AddHealthChecksUI().AddInMemoryStorage();

            return services;
        }

        public static IApplicationBuilder UseHealthCheckConfiguration(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/healthcheck", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI(options =>
            {
                options.UIPath = "/healthchecks-ui";
                options.ApiPath = "/health-ui-api";
            });

            return app;
        }
    }
}