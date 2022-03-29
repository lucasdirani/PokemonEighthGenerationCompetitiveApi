namespace EighthGenerationCompetitive.Api.V1.ViewModels.Types
{
    /// <summary>
    /// Represents a tier associated with a Pokémon on the endpoints of the Types route
    /// </summary>
    public class PokemonTypeCreatureTierViewModel
    {
        public PokemonTypeCreatureTierViewModel() {}

        /// <summary>
        /// The tier name
        /// </summary>
        public string TierName { get; set; }

        /// <summary>
        /// The unique identification for the tier
        /// </summary>
        public string TierId { get; set; }
    }
}