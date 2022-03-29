using EighthGenerationCompetitive.Business.Entities.NatureAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.NatureAggregate
{
    internal static class PokemonStatMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<PokemonStat>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(s => s.StatName).SetIsRequired(true).SetElementName("statName");
            });
        }
    }
}