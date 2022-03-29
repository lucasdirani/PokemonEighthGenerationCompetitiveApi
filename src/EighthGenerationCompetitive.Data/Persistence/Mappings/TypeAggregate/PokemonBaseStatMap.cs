using EighthGenerationCompetitive.Business.Entities.TypeAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.TypeAggregate
{
    internal static class PokemonBaseStatMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<PokemonBaseStat>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(p => p.BaseStat).SetIsRequired(true).SetElementName("baseStat");
                map.MapMember(p => p.BaseStatNumber).SetIsRequired(true).SetElementName("baseStatNumber");
            });
        }
    }
}