using EighthGenerationCompetitive.Business.Entities.NatureAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.NatureAggregate
{
    internal static class PokemonBaseStatMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<PokemonBaseStat>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(b => b.BaseStat).SetIsRequired(true).SetElementName("baseStat");
                map.MapMember(b => b.BaseStatNumber).SetIsRequired(true).SetElementName("baseStatNumber");
            });
        }
    }
}