using EighthGenerationCompetitive.Data.Persistence.Mappings.TypeAggregate;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.Registration
{
    internal static class TypeAggregateMapRegister
    {
        public static void Register()
        {
            PokemonMoveMap.Configure();

            PokemonTypeMap.Configure();

            PokemonTierMap.Configure();

            PokemonStatMap.Configure();

            PokemonBaseStatMap.Configure();

            PokemonAbilityMap.Configure();

            TypeRelationsMap.Configure();

            PokemonMap.Configure();

            PokemonFormMap.Configure();

            TypeMap.Configure();
        }
    }
}