using EighthGenerationCompetitive.Api.BaseControllers;
using EighthGenerationCompetitive.Api.Extensions;
using EighthGenerationCompetitive.Api.Handlers;
using Microsoft.Extensions.DependencyInjection;
using RiskFirst.Hateoas;

namespace EighthGenerationCompetitive.Api.Configurations
{
    public static class HateoasConfig
    {
        public static IServiceCollection AddHateoasConfig(this IServiceCollection services)
        {
            services.AddLinks(config =>
            {
                config.UseRelativeHrefs();
                config.AddPolicyForGetPokemonTypes();
                config.AddPolicyForGetPokemonTypeByName();
                config.AddPolicyForGetNatures();
                config.AddPolicyForGetNatureByName();
                config.AddPolicyForUserLogin();
                config.AddPolicyForGetUser();
                config.AddPolicyForGetUserContacts();
                config.AddPolicyForGetUserContact();
            });

            services.AddTransient<ILinksHandler, VersionLinkRequirement<MainController>>();

            return services;
        }
    }
}