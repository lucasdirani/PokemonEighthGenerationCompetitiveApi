using System.Collections.Generic;

namespace EighthGenerationCompetitive.Api.V1.ViewModels.Natures
{
    /// <summary>
    /// Represents the Nature returned from the endpoint GetNatureMonstersByName
    /// </summary>
    public class GetNatureMonstersByNameViewModel
    {
        /// <summary>
        /// The nature name
        /// </summary>
        public string NatureName { get; set; }

        /// <summary>
        /// Pokémon that have strategies with the searched nature
        /// </summary>
        public IList<NaturePokemonViewModel> NaturePokemon { get; set; }
    }
}