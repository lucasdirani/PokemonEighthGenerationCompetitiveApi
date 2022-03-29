using EighthGenerationCompetitive.Data.Persistence.Mappings.NatureAggregate;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.Registration
{
    internal static class NatureAggregateMapRegister
    {
        public static void Register()
        {
            DecreasedNatureMap.Configure();

            IncreasedNatureMap.Configure();

            PokemonAbilityMap.Configure();

            PokemonStatMap.Configure();

            PokemonTierMap.Configure();

            PokemonTypeMap.Configure();

            NatureStatMap.Configure();

            PokemonBaseStatMap.Configure();

            PokemonFormMap.Configure();

            PokemonMap.Configure();

            NatureMap.Configure();
        }
    }
}