using EighthGenerationCompetitive.Business.Entities.StrategyAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.StrategyAggregate
{
    internal static class PokemonEffortValueMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<PokemonEffortValue>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(p => p.EffortValueNumber).SetIsRequired(true).SetElementName("effortValueNumber");
                map.MapMember(p => p.EffortValueStat).SetIsRequired(true).SetElementName("effortValueStat");
            });
        }
    }
}