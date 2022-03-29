using EighthGenerationCompetitive.Data.Persistence.Mappings.StrategyAggregate;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.Registration
{
    public static class StrategyAggregateMapRegister
    {
        public static void Register()
        {
            PokemonAbilityMap.Configure();

            PokemonStatMap.Configure();

            PokemonBaseStatMap.Configure();

            PokemonEffortValueMap.Configure();

            PokemonIndividualValueMap.Configure();

            PokemonItemMap.Configure();

            PokemonMoveMap.Configure();

            PokemonNatureMap.Configure();

            PokemonTierMap.Configure();

            PokemonTypeMap.Configure();

            StrategyCheckerMap.Configure();

            StrategyCounterMap.Configure();

            StrategyPartnerMap.Configure();

            StrategyMoveMap.Configure();

            PokemonFormMap.Configure();

            PokemonMap.Configure();

            StrategyMap.Configure();
        }
    }
}