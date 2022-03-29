using System.Collections.Generic;

namespace EighthGenerationCompetitive.Business.Entities.StrategyAggregate
{
    public class StrategyMove
    {
        public IList<PokemonMove> StrategyFirstMoves { get; set; }
        public IList<PokemonMove> StrategySecondMoves { get; set; }
        public IList<PokemonMove> StrategyThirdMoves { get; set; }
        public IList<PokemonMove> StrategyFourthMoves { get; set; }
    }
}