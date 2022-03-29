using EighthGenerationCompetitive.Business.Entities.StrategyAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.StrategyAggregate
{
    internal static class PokemonStatMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<PokemonStat>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(p => p.StatName).SetIsRequired(true).SetElementName("statName");
            });
        }
    }
}