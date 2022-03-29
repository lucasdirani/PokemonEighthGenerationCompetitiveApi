using EighthGenerationCompetitive.Business.Entities.TypeAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.TypeAggregate
{
    internal static class TypeMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Type>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(p => p.TypeId).SetIsRequired(true).SetElementName("typeId");
                map.MapMember(p => p.TypeMoves).SetIsRequired(true).SetElementName("typeMoves");
                map.MapMember(p => p.TypeName).SetIsRequired(true).SetElementName("typeName");
                map.MapMember(p => p.TypePokemon).SetIsRequired(true).SetElementName("typePokemon");
                map.MapMember(p => p.TypePokemonForms).SetIsRequired(false).SetElementName("typePokemonForms");
                map.MapMember(p => p.TypeRelations).SetIsRequired(true).SetElementName("typeRelations");
            });
        }
    }
}