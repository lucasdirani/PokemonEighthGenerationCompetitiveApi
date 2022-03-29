using EighthGenerationCompetitive.Business.Entities.TypeAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.TypeAggregate
{
    internal static class PokemonTierMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<PokemonTier>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(p => p.TierId).SetIsRequired(true).SetElementName("tierId");
                map.MapMember(p => p.TierName).SetIsRequired(true).SetElementName("tierName");
            });
        }
    }
}