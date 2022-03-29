namespace EighthGenerationCompetitive.Api.V1.ViewModels.Types
{
    /// <summary>
    /// Represents the base stats of a pokemon
    /// </summary>
    public class PokemonTypeCreatureBaseStatViewModel
    {
        public PokemonTypeCreatureBaseStatViewModel() {}

        /// <summary>
        /// The description of the base stat
        /// </summary>
        public PokemonTypeCreatureStatViewModel BaseStat { get; set; }

        /// <summary>
        /// The base stat value
        /// </summary>
        public int BaseStatNumber { get; set; }
    }
}