using System;

namespace EighthGenerationCompetitive.Api.Cache
{
    public static class CacheExpiration
    {
        public static TimeSpan FromDays(int days) => TimeSpan.FromDays(days);
    }
}