using EighthGenerationCompetitive.Business.Entities.NatureAggregate;
using MongoDB.Driver.Linq;

namespace EighthGenerationCompetitive.Data.Filters.NatureAggregate.Stats
{
    internal interface INatureStatsFilters
    {
        IMongoQueryable<Nature> ApplyFilters();
    }
}