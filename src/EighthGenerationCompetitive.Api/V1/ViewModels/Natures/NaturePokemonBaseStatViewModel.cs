namespace EighthGenerationCompetitive.Api.V1.ViewModels.Natures
{
    /// <summary>
    /// Represents the base stat of a pokemon
    /// </summary>
    public class NaturePokemonBaseStatViewModel
    {
        /// <summary>
        /// The base stat identification
        /// </summary>
        public NaturePokemonStatViewModel BaseStat { get; set; }

        /// <summary>
        /// The base stat value
        /// </summary>
        public int BaseStatNumber { get; set; }
    }
}