using System.Collections.Generic;

namespace EighthGenerationCompetitive.Business.Entities.NatureAggregate
{
    public class Nature : Entity
    {
        public string NatureName { get; set; }
        public string NatureId { get; set; }
        public NatureStat NatureDecreasedStat { get; set; }
        public NatureStat NatureIncreasedStat { get; set; }
        public decimal NatureDecreasedStatIn { get; set; }
        public decimal NatureIncreasedStatIn { get; set; }
        public IList<Pokemon> NaturePokemon { get; set; }
        public IList<PokemonForm> NaturePokemonForms { get; set; }
    }
}