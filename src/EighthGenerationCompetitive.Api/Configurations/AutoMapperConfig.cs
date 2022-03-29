using AutoMapper;
using EighthGenerationCompetitive.Api.Profiles.V1;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace EighthGenerationCompetitive.Api.Configurations
{
    public static class AutoMapperConfig
    {
        public static IServiceCollection AddAutoMapperConfigurationForV1(this IServiceCollection services)
        {
            services.AddAutoMapper(config => 
            {
                config.AddProfiles(new List<Profile> 
                { 
                    new NatureProfile(),
                    new PokemonTypeProfile(),
                    new UserProfile(),
                });
            });

            return services;
        }
    }
}