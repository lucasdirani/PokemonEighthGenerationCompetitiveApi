namespace EighthGenerationCompetitive.Api.V1.ViewModels.Types
{
    /// <summary>
    /// Represents a simplification of a pokemon type
    /// </summary>
    public class PokemonTypeCreatureTypeViewModel
    {
        public PokemonTypeCreatureTypeViewModel() {}

        /// <summary>
        /// The name of the pokemon type
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// The pokemon type unique identification
        /// </summary>
        public string TypeId { get; set; }
    }
}