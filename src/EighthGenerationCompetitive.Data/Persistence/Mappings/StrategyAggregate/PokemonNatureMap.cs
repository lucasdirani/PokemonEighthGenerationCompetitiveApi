using EighthGenerationCompetitive.Business.Entities.StrategyAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.StrategyAggregate
{
    internal static class PokemonNatureMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<PokemonNature>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(p => p.NatureDecreasedStat).SetIsRequired(true).SetElementName("natureDecreasedStat");
                map.MapMember(p => p.NatureId).SetIsRequired(true).SetElementName("natureId");
                map.MapMember(p => p.NatureIncreasedStat).SetIsRequired(true).SetElementName("natureIncreasedStat");
                map.MapMember(p => p.NatureName).SetIsRequired(true).SetElementName("natureName");
            });
        }
    }
}