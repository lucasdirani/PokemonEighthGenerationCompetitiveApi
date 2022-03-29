using EighthGenerationCompetitive.Business.Entities.TypeAggregate;
using MongoDB.Driver.Linq;

namespace EighthGenerationCompetitive.Data.Filters.TypeAggregate.TypeRelations
{
    internal interface ITypeRelationsFilter
    {
        IMongoQueryable<Type> ApplyFilters();
    }
}