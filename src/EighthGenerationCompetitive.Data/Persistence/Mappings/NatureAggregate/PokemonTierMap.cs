using EighthGenerationCompetitive.Business.Entities.NatureAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.NatureAggregate
{
    internal static class PokemonTierMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<PokemonTier>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(t => t.TierId).SetIsRequired(true).SetElementName("tierId");
                map.MapMember(t => t.TierName).SetIsRequired(true).SetElementName("tierName");
            });
        }
    }
}