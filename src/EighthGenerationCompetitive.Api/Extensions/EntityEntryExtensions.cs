using EighthGenerationCompetitive.Api.Identity.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;

namespace EighthGenerationCompetitive.Api.Extensions
{
    public static class EntityEntryExtensions
    {
        public static IList<EntityEntry> GetAddedAndModifiedEntities(this IEnumerable<EntityEntry> entries)
        {
            return entries.Where(e => e.Entity is ITraceableEntity && (e.State == EntityState.Added || e.State == EntityState.Modified)).ToList();
        }
    }
}