namespace EighthGenerationCompetitive.Api.V1.ViewModels.Types
{
    /// <summary>
    /// Represents a Pokémon's ability on the endpoints of the Types route
    /// </summary>
    public class PokemonTypeCreatureAbilityViewModel
    {
        public PokemonTypeCreatureAbilityViewModel() {}

        /// <summary>
        /// The ability name
        /// </summary>
        public string AbilityName { get; set; }

        /// <summary>
        /// The unique identification of the ability
        /// </summary>
        public string AbilityId { get; set; }

        /// <summary>
        /// The description of the effects of ability
        /// </summary>
        public string AbilityDetails { get; set; }
    }
}