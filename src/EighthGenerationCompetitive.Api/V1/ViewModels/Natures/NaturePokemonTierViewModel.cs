namespace EighthGenerationCompetitive.Api.V1.ViewModels.Natures
{
    /// <summary>
    /// Represents the simplification of a tier that a Pokémon can be part of
    /// </summary>
    public class NaturePokemonTierViewModel
    {
        /// <summary>
        /// The tier name
        /// </summary>
        public string TierName { get; set; }

        /// <summary>
        /// The tier unique identification
        /// </summary>
        public string TierId { get; set; }
    }
}