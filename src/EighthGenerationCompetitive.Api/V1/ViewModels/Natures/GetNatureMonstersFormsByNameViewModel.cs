using System.Collections.Generic;

namespace EighthGenerationCompetitive.Api.V1.ViewModels.Natures
{
    /// <summary>
    /// Represents the Nature returned from the endpoint GetNatureMonstersFormsByName
    /// </summary>
    public class GetNatureMonstersFormsByNameViewModel
    {
        /// <summary>
        /// The nature name
        /// </summary>
        public string NatureName { get; set; }

        /// <summary>
        /// Pokémon forms that have strategies with the searched nature
        /// </summary>
        public IList<NaturePokemonFormViewModel> NaturePokemonForms { get; set; }
    }
}