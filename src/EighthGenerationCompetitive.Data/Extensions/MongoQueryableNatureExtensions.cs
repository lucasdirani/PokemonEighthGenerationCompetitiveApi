using EighthGenerationCompetitive.Business.Entities.NatureAggregate;
using EighthGenerationCompetitive.Data.Filters.NatureAggregate.Stats;
using MongoDB.Driver.Linq;

namespace EighthGenerationCompetitive.Data.Extensions
{
    internal static class MongoQueryableNatureExtensions
    {
        internal static IMongoQueryable<Nature> ApplyStatsConditions(
            this IMongoQueryable<Nature> natures,
            string increasedStatName,
            string decreasedStatName) =>
            NatureStatsFilters
                .ApplyFiltersFrom(natures)
                .ApplyDecreasedStat(decreasedStatName)
                .ApplyIncreasedStat(increasedStatName)
                .ApplyFilters();
    }
}