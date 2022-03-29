using System.Collections.Generic;

namespace EighthGenerationCompetitive.Api.V1.ViewModels.Types
{
    /// <summary>
    /// Represents the Pokemon forms returned from the endpoint GetPokemonTypesMonstersFormsByName
    /// </summary>
    public class GetPokemonTypeMonstersFormsByNameViewModel
    {
        /// <summary>
        /// The pokemon type name
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Pokémon forms that have the searched type
        /// </summary>
        public IList<PokemonTypeCreatureFormViewModel> TypePokemonForms { get; set; }
    }
}