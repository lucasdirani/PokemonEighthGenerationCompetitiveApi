using EighthGenerationCompetitive.Api.Extensions;
using EighthGenerationCompetitive.Api.Identity.Interfaces;
using EighthGenerationCompetitive.Api.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Api.Identity.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public override int SaveChanges()
        {
            AddTimestamps();

            return base.SaveChanges();
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().GetAddedAndModifiedEntities();

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow;

                ITraceableEntity traceableEntity = entity.Entity as ITraceableEntity;

                if (entity.State == EntityState.Added)
                {
                    traceableEntity.CreatedAt = now;
                }

                traceableEntity.UpdatedAt = now;
            }
        }
    }
}