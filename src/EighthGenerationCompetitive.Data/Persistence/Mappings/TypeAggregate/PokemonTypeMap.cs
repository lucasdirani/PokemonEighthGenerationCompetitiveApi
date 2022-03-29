using EighthGenerationCompetitive.Business.Entities.TypeAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.TypeAggregate
{
    internal static class PokemonTypeMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<PokemonType>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(p => p.TypeId).SetIsRequired(true).SetElementName("typeId");
                map.MapMember(p => p.TypeName).SetIsRequired(true).SetElementName("typeName");
            });
        }
    }
}