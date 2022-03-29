using System;
using System.Collections.Generic;

namespace EighthGenerationCompetitive.Business.Entities.StrategyAggregate
{
    public class Strategy : Entity
    {
        public string StrategyName { get; set; }
        public string StrategyId { get; set; }
        public string StrategyDetails { get; set; }
        public string StrategyCreator { get; set; }
        public Pokemon StrategyPokemon { get; set; }
        public PokemonForm StrategyPokemonForm { get; set; }
        public DateTime StrategyCreationDate { get; set; }
        public PokemonTier StrategyTier { get; set; }
        public IList<PokemonItem> StrategyItems { get; set; }
        public IList<PokemonAbility> StrategyAbilities { get; set; }
        public IList<PokemonNature> StrategyNatures { get; set; }
        public IList<PokemonEffortValue> StrategyEffortValues { get; set; }
        public IList<PokemonIndividualValue> StrategyIndividualValues { get; set; }
        public IList<StrategyPartner> StrategyPartners { get; set; }
        public IList<StrategyCounter> StrategyCounters { get; set; }
        public IList<StrategyChecker> StrategyCheckers { get; set; }
        public StrategyMove StrategyMove { get; set; }
    }
}