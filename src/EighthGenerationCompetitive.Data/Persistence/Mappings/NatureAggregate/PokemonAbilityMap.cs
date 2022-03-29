using EighthGenerationCompetitive.Business.Entities.NatureAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.NatureAggregate
{
    internal static class PokemonAbilityMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<PokemonAbility>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(a => a.AbilityDetails).SetIsRequired(true).SetElementName("abilityDetails");
                map.MapMember(a => a.AbilityId).SetIsRequired(true).SetElementName("abilityId");
                map.MapMember(a => a.AbilityName).SetIsRequired(true).SetElementName("abilityName");
            });
        }
    }
}