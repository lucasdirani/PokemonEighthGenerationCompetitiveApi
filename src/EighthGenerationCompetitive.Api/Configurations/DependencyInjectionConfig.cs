using EighthGenerationCompetitive.Api.Authentication;
using EighthGenerationCompetitive.Api.Cache.Decorators;
using EighthGenerationCompetitive.Api.Identity.Repository;
using EighthGenerationCompetitive.Api.Identity.Repository.Interfaces;
using EighthGenerationCompetitive.Api.Services;
using EighthGenerationCompetitive.Api.Services.Interfaces;
using EighthGenerationCompetitive.Business.Interfaces;
using EighthGenerationCompetitive.Business.Notifications;
using EighthGenerationCompetitive.Business.Repositories;
using EighthGenerationCompetitive.Business.Services;
using EighthGenerationCompetitive.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EighthGenerationCompetitive.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveGeneralDependencies(this IServiceCollection services)
        {
            services.AddScoped<INotifier, Notifier>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUser, AspNetUser>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }

        public static IServiceCollection ResolveRepositories(this IServiceCollection services)
        {
            services.AddScoped<PokemonTypeRepository>();

            services.AddScoped<IPokemonTypeRepository, PokemonTypeCachingDecorator<PokemonTypeRepository>>();

            services.AddScoped<NatureRepository>();

            services.AddScoped<INatureRepository, NatureCachingDecorator<NatureRepository>>();

            services.AddScoped<ApplicationUserRepository>();

            services.AddScoped<IApplicationUserRepository, ApplicationUserCachingDecorator<ApplicationUserRepository>>();

            return services;
        }

        public static IServiceCollection ResolveApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IPokemonTypeService, PokemonTypeService>();

            services.AddScoped<INatureService, NatureService>();

            services.AddScoped<IApplicationUserService, ApplicationUserService>();

            return services;
        }
    }
}