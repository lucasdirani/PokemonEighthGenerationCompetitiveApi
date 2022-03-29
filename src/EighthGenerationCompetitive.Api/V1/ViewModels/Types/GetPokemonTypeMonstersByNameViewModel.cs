using System.Collections.Generic;

namespace EighthGenerationCompetitive.Api.V1.ViewModels.Types
{
    /// <summary>
    /// Represents the Pokemon returned from the endpoint GetPokemonTypesMonstersByName
    /// </summary>
    public class GetPokemonTypeMonstersByNameViewModel
    {
        /// <summary>
        /// The pokemon type name
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Pokémon that have the searched type
        /// </summary>
        public IList<PokemonTypeCreatureViewModel> TypePokemon { get; set; }
    }
}