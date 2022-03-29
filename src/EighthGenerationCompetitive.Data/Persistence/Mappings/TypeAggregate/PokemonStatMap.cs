using EighthGenerationCompetitive.Business.Entities.TypeAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.TypeAggregate
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