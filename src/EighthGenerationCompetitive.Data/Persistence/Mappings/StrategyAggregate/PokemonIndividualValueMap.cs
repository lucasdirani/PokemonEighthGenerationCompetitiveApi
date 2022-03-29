using EighthGenerationCompetitive.Business.Entities.StrategyAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.StrategyAggregate
{
    internal static class PokemonIndividualValueMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<PokemonIndividualValue>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(p => p.IndividualValueNumber).SetIsRequired(true).SetElementName("individualValueNumber");
                map.MapMember(p => p.IndividualValueStat).SetIsRequired(true).SetElementName("individualValueStat");
            });
        }
    }
}