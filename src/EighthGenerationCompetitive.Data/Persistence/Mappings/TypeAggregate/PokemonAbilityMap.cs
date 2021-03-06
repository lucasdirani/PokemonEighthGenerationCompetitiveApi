using EighthGenerationCompetitive.Business.Entities.TypeAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.TypeAggregate
{
    internal static class PokemonAbilityMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<PokemonAbility>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(p => p.AbilityDetails).SetIsRequired(true).SetElementName("abilityDetails");
                map.MapMember(p => p.AbilityId).SetIsRequired(true).SetElementName("abilityId");
                map.MapMember(p => p.AbilityName).SetIsRequired(true).SetElementName("abilityName");
            });
        }
    }
}