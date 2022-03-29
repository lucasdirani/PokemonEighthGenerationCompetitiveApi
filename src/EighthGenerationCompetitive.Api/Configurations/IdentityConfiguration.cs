using EighthGenerationCompetitive.Api.Identity.Context;
using EighthGenerationCompetitive.Api.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EighthGenerationCompetitive.Api.Configurations
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection AddIdentityConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("SqlServer");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services
                .AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}