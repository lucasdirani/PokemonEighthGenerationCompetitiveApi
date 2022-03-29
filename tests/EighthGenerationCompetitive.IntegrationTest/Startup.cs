using EighthGenerationCompetitive.IntegrationTest.Dependencies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace EighthGenerationCompetitive.IntegrationTest
{
    public class Startup
    {
        protected IConfiguration Configuration { get; }

        public Startup()
        {
            Configuration = BuildConfiguration();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<TestedUser>(options => Configuration.GetSection("TestedUser").Bind(options));
        }

        private static IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }
    }
}