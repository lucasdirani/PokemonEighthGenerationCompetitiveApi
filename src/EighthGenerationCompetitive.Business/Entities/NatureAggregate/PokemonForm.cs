using System.Collections.Generic;

namespace EighthGenerationCompetitive.Business.Entities.NatureAggregate
{
    public class PokemonForm
    {
        public string PokemonFormName { get; set; }
        public string PokemonFormId { get; set; }
        public IList<PokemonType> PokemonFormTypes { get; set; }
        public IList<PokemonBaseStat> PokemonFormBaseStats { get; set; }
        public IList<PokemonAbility> PokemonFormAbilities { get; set; }
        public IList<PokemonTier> PokemonFormTiers { get; set; }
    }
}