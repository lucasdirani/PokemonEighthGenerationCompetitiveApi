using System.Collections.Generic;

namespace EighthGenerationCompetitive.Api.V1.ViewModels.Types
{
    /// <summary>
    /// Represents the Pokémon form returned from the Types route endpoints
    /// </summary>
    public class PokemonTypeCreatureFormViewModel
    {
        public PokemonTypeCreatureFormViewModel() 
        {
            PokemonFormTypes = new List<PokemonTypeCreatureTypeViewModel>();
            PokemonFormBaseStats = new List<PokemonTypeCreatureBaseStatViewModel>();
            PokemonFormAbilities = new List<PokemonTypeCreatureAbilityViewModel>();
            PokemonFormTiers = new List<PokemonTypeCreatureTierViewModel>();
        }

        /// <summary>
        /// The pokemon form name
        /// </summary>
        public string PokemonFormName { get; set; }

        /// <summary>
        /// The types that the pokemon form have
        /// </summary>
        public List<PokemonTypeCreatureTypeViewModel> PokemonFormTypes { get; set; }

        /// <summary>
        /// The base stats that the pokemon form have
        /// </summary>
        public List<PokemonTypeCreatureBaseStatViewModel> PokemonFormBaseStats { get; set; }

        /// <summary>
        /// The abilities that the pokemon form have
        /// </summary>
        public List<PokemonTypeCreatureAbilityViewModel> PokemonFormAbilities { get; set; }

        /// <summary>
        /// The tiers in which the pokemon form has strategies
        /// </summary>
        public List<PokemonTypeCreatureTierViewModel> PokemonFormTiers { get; set; }
    }
}