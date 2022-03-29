using EighthGenerationCompetitive.Api.Configurations;
using EighthGenerationCompetitive.Api.Conventions;
using EighthGenerationCompetitive.Api.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

[assembly: ApiConventionType(typeof(EighthGenerationCompetitiveApiConventions))]
namespace EighthGenerationCompetitive.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiResponseCompressionConfig();

            services.AddIdentityConfiguration(Configuration);

            services.AddAuthenticationConfiguration(Configuration);

            services.AddApiCachingConfig(Configuration);

            services.AddHateoasConfig();

            services.AddMongoDbConfig();

            services.AddAutoMapperConfigurationForV1();

            services.AddWebApiConfig();

            services.AddHealthCheckConfiguration(Environment, Configuration);

            services.ResolveGeneralDependencies();

            services.ResolveRepositories();

            services.ResolveApplicationServices();

            services.AddLoggingConfiguration();

            services.AddSwaggerConfig();
        }

        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env, 
            IApiVersionDescriptionProvider provider)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor 
                | ForwardedHeaders.XForwardedProto,
            });

            app.UseHealthCheckConfiguration();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseLoggingConfiguration(Configuration);

            app.UseMvcConfiguration();

            app.UseSwaggerConfig(provider);
        }
    }
}