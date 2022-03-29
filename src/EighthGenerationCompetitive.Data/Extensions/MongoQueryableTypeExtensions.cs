using EighthGenerationCompetitive.Business.Entities.TypeAggregate;
using EighthGenerationCompetitive.Business.Parameters.TypeAggregate;
using EighthGenerationCompetitive.Data.Filters.TypeAggregate.TypeRelations;
using MongoDB.Driver.Linq;

namespace EighthGenerationCompetitive.Data.Extensions
{
    internal static class MongoQueryableTypeExtensions
    {
        internal static IMongoQueryable<Type> ApplyTypeRelationsConditions(
            this IMongoQueryable<Type> pokemonTypes,
            TypeRelationsParameters typeRelationsParameters) =>
            TypeRelationsFilters
                .ApplyFiltersFrom(pokemonTypes)
                .ApplyHalfDamageTo(typeRelationsParameters.HalfDamageTo)
                .ApplyNoDamageTo(typeRelationsParameters.NoDamageTo)
                .ApplyDoubleDamageTo(typeRelationsParameters.DoubleDamageTo)
                .ApplyNoDamageFrom(typeRelationsParameters.NoDamageFrom)
                .ApplyHalfDamageFrom(typeRelationsParameters.HalfDamageFrom)
                .ApplyDoubleDamageFrom(typeRelationsParameters.DoubleDamageFrom)
                .ApplyFilters();
    }
}