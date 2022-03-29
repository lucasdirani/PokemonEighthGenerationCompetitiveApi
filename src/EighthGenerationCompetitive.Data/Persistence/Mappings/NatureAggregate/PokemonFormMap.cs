using EighthGenerationCompetitive.Business.Entities.NatureAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.NatureAggregate
{
    internal static class PokemonFormMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<PokemonForm>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(p => p.PokemonFormAbilities).SetIsRequired(false).SetElementName("pokemonFormAbilities");
                map.MapMember(p => p.PokemonFormBaseStats).SetIsRequired(true).SetElementName("pokemonFormBaseStats");
                map.MapMember(p => p.PokemonFormId).SetIsRequired(true).SetElementName("pokemonFormId");
                map.MapMember(p => p.PokemonFormName).SetIsRequired(true).SetElementName("pokemonFormName");
                map.MapMember(p => p.PokemonFormTiers).SetIsRequired(true).SetElementName("pokemonFormTiers");
                map.MapMember(p => p.PokemonFormTypes).SetIsRequired(true).SetElementName("pokemonFormTypes");
            });
        }
    }
}