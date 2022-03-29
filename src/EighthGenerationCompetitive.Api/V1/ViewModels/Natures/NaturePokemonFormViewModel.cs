using System.Collections.Generic;

namespace EighthGenerationCompetitive.Api.V1.ViewModels.Natures
{
    /// <summary>
    /// Represents a Pokemon form that has a particular nature in their strategies
    /// </summary>
    public class NaturePokemonFormViewModel
    {
        public NaturePokemonFormViewModel()
        {
            PokemonFormTypes = new List<NaturePokemonTypeViewModel>();
            PokemonFormBaseStats = new List<NaturePokemonBaseStatViewModel>();
            PokemonFormAbilities = new List<NaturePokemonAbilityViewModel>();
            PokemonFormTiers = new List<NaturePokemonTierViewModel>();
        }

        /// <summary>
        /// The pokemon form name
        /// </summary>
        public string PokemonFormName { get; set; }

        /// <summary>
        /// The pokemon form unique identification
        /// </summary>
        public string PokemonFormId { get; set; }

        /// <summary>
        /// The types that the pokemon form has
        /// </summary>
        public IList<NaturePokemonTypeViewModel> PokemonFormTypes { get; set; }

        /// <summary>
        /// The base stats that the pokemon form has
        /// </summary>
        public IList<NaturePokemonBaseStatViewModel> PokemonFormBaseStats { get; set; }

        /// <summary>
        /// The abilities that the pokemon form has
        /// </summary>
        public IList<NaturePokemonAbilityViewModel> PokemonFormAbilities { get; set; }

        /// <summary>
        /// The tiers that the pokemon form is part of
        /// </summary>
        public IList<NaturePokemonTierViewModel> PokemonFormTiers { get; set; }
    }
}