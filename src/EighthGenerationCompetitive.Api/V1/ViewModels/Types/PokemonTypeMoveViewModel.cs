namespace EighthGenerationCompetitive.Api.V1.ViewModels.Types
{
    /// <summary>
    /// Represents a move on the endpoints of the Types route
    /// </summary>
    public class PokemonTypeMoveViewModel
    {
        public PokemonTypeMoveViewModel() {}

        /// <summary>
        /// The move name
        /// </summary>
        public string MoveName { get; set; }

        /// <summary>
        /// The unique identification for the move
        /// </summary>
        public string MoveId { get; set; }

        /// <summary>
        /// Indicates whether the move is physical, special, or non-damaging
        /// </summary>
        public string MoveCategory { get; set; }

        /// <summary>
        /// The damage done by the move
        /// </summary>
        public int? MovePower { get; set; }

        /// <summary>
        /// The chance that the move has to hit
        /// </summary>
        public int? MoveAccuracy { get; set; }

        /// <summary>
        /// The amount of times the move can be used
        /// </summary>
        public int MovePP { get; set; }

        /// <summary>
        /// The description about the effects of the move
        /// </summary>
        public string MoveDetails { get; set; }
    }
}