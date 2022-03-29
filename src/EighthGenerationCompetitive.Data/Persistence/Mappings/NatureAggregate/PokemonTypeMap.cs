using EighthGenerationCompetitive.Business.Entities.NatureAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.NatureAggregate
{
    internal static class PokemonTypeMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<PokemonType>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(t => t.TypeId).SetIsRequired(true).SetElementName("typeId");
                map.MapMember(t => t.TypeName).SetIsRequired(true).SetElementName("typeName");
            });
        }
    }
}