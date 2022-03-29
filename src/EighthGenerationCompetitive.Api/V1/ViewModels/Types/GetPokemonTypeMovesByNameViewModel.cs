using System.Collections.Generic;

namespace EighthGenerationCompetitive.Api.V1.ViewModels.Types
{
    /// <summary>
    /// Represents the moves returned from the endpoint GetPokemonTypesMovesByName
    /// </summary>
    public class GetPokemonTypeMovesByNameViewModel
    {
        /// <summary>
        /// The pokemon type name
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Moves that have the searched type
        /// </summary>
        public IList<PokemonTypeMoveViewModel> TypeMoves { get; set; }
    }
}