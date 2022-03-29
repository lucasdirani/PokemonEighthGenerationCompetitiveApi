using System.Collections.Generic;

namespace EighthGenerationCompetitive.Api.V1.ViewModels.Types
{
    /// <summary>
    /// Represents the Pokémon returned from the Types route endpoints
    /// </summary>
    public class PokemonTypeCreatureViewModel
    {
        public PokemonTypeCreatureViewModel() 
        {
            PokemonTypes = new List<PokemonTypeCreatureTypeViewModel>();
            PokemonBaseStats = new List<PokemonTypeCreatureBaseStatViewModel>();
            PokemonAbilities = new List<PokemonTypeCreatureAbilityViewModel>();
            PokemonTiers = new List<PokemonTypeCreatureTierViewModel>();
        }

        /// <summary>
        /// The pokemon name
        /// </summary>
        public string PokemonName { get; set; }

        /// <summary>
        /// The types that the pokemon have
        /// </summary>
        public List<PokemonTypeCreatureTypeViewModel> PokemonTypes { get; set; }

        /// <summary>
        /// The base stats that the pokemon have
        /// </summary>
        public List<PokemonTypeCreatureBaseStatViewModel> PokemonBaseStats { get; set; }

        /// <summary>
        /// The abilities that the pokemon have
        /// </summary>
        public List<PokemonTypeCreatureAbilityViewModel> PokemonAbilities { get; set; }

        /// <summary>
        /// The tiers in which the pokemon has strategies
        /// </summary>
        public List<PokemonTypeCreatureTierViewModel> PokemonTiers { get; set; }
    }
}