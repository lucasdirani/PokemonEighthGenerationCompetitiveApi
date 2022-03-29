using EighthGenerationCompetitive.Business.Entities.TypeAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.TypeAggregate
{
    internal static class TypeRelationsMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<TypeRelations>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(p => p.TypeRelationsDoubleDamageFrom).SetIsRequired(false).SetElementName("typeRelationsDoubleDamageFrom");
                map.MapMember(p => p.TypeRelationsDoubleDamageTo).SetIsRequired(false).SetElementName("typeRelationsDoubleDamageTo");
                map.MapMember(p => p.TypeRelationsHalfDamageFrom).SetIsRequired(false).SetElementName("typeRelationsHalfDamageFrom");
                map.MapMember(p => p.TypeRelationsHalfDamageTo).SetIsRequired(false).SetElementName("typeRelationsHalfDamageTo");
                map.MapMember(p => p.TypeRelationsNoDamageFrom).SetIsRequired(false).SetElementName("typeRelationsNoDamageFrom");
                map.MapMember(p => p.TypeRelationsNoDamageTo).SetIsRequired(false).SetElementName("typeRelationsNoDamageTo");
            });
        }
    }
}