using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace EighthGenerationCompetitive.Api.Configurations
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;
        readonly IConfiguration configuration;

        public ConfigureSwaggerOptions(
            IApiVersionDescriptionProvider provider, 
            IConfiguration configuration)
        {
            this.provider = provider;
            this.configuration = configuration;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description, configuration));
            }
        }

        static OpenApiInfo CreateInfoForApiVersion(
            ApiVersionDescription description, 
            IConfiguration configuration)
        {
            string mitLicense = configuration.GetSection("GeneralSettings").GetSection("MITLicense").Value; 

            var info = new OpenApiInfo()
            {
                Title = "EighthGenerationCompetitive API",
                Version = description.ApiVersion.ToString(),
                Description = "This API returns data about Pokémon teams and competitive strategies for the eighth generation UU and OU tiers.",
                Contact = new OpenApiContact() { Name = "Lucas Dirani", Email = "lucas.dirani@gmail.com" },
                License = new OpenApiLicense() { Name = "MIT", Url = new Uri(mitLicense) }
            };

            if (description.IsDeprecated)
            {
                info.Description += " This version is deprecated.";
            }

            return info;
        }
    }
}