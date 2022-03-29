using System.Collections.Generic;

namespace EighthGenerationCompetitive.Business.Entities.TypeAggregate
{
    public class Pokemon
    {
        public string PokemonName { get; set; }

        public string PokemonId { get; set; }

        public IList<PokemonType> PokemonTypes { get; set; }

        public IList<PokemonBaseStat> PokemonBaseStats { get; set; }

        public IList<PokemonAbility> PokemonAbilities { get; set; }

        public IList<PokemonTier> PokemonTiers { get; set; }
    }
}