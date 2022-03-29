using System.Collections.Generic;

namespace EighthGenerationCompetitive.Api.V1.ViewModels.Natures
{
    /// <summary>
    /// Represents a Pokemon that has a particular nature in their strategies
    /// </summary>
    public class NaturePokemonViewModel
    {
        public NaturePokemonViewModel()
        {
            PokemonTypes = new List<NaturePokemonTypeViewModel>();
            PokemonBaseStats = new List<NaturePokemonBaseStatViewModel>();
            PokemonAbilities = new List<NaturePokemonAbilityViewModel>();
            PokemonTiers = new List<NaturePokemonTierViewModel>();
        }

        /// <summary>
        /// The pokemon name
        /// </summary>
        public string PokemonName { get; set; }

        /// <summary>
        /// The pokemon unique identification
        /// </summary>
        public string PokemonId { get; set; }

        /// <summary>
        /// The types that the pokemon has
        /// </summary>
        public IList<NaturePokemonTypeViewModel> PokemonTypes { get; set; }

        /// <summary>
        /// The base stats that the pokemon has
        /// </summary>
        public IList<NaturePokemonBaseStatViewModel> PokemonBaseStats { get; set; }

        /// <summary>
        /// The abilities that the pokemon has
        /// </summary>
        public IList<NaturePokemonAbilityViewModel> PokemonAbilities { get; set; }

        /// <summary>
        /// The tiers that the pokemon is part of
        /// </summary>
        public IList<NaturePokemonTierViewModel> PokemonTiers { get; set; }
    }
}