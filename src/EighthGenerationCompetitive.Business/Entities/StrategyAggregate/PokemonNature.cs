namespace EighthGenerationCompetitive.Business.Entities.StrategyAggregate
{
    public class PokemonNature
    {
        public string NatureName { get; set; }
        public string NatureId { get; set; }
        public PokemonStat NatureDecreasedStat { get; set; }
        public PokemonStat NatureIncreasedStat { get; set; }
    }
}