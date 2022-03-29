using EighthGenerationCompetitive.Business.Entities.TypeAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.TypeAggregate
{
    internal static class PokemonMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Pokemon>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(p => p.PokemonAbilities).SetIsRequired(true).SetElementName("pokemonAbilities");
                map.MapMember(p => p.PokemonBaseStats).SetIsRequired(true).SetElementName("pokemonBaseStats");
                map.MapMember(p => p.PokemonId).SetIsRequired(true).SetElementName("pokemonId");
                map.MapMember(p => p.PokemonName).SetIsRequired(true).SetElementName("pokemonName");
                map.MapMember(p => p.PokemonTiers).SetIsRequired(true).SetElementName("pokemonTiers");
                map.MapMember(p => p.PokemonTypes).SetIsRequired(true).SetElementName("pokemonTypes");
            });
        }
    }
}