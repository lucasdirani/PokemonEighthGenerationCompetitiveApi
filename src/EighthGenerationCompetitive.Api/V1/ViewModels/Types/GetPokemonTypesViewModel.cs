using RiskFirst.Hateoas.Models;

namespace EighthGenerationCompetitive.Api.V1.ViewModels.Types
{
    /// <summary>
    /// Represents the Pokemon types returned from the endpoint GetPokemonTypes
    /// </summary>
    public class GetPokemonTypesViewModel : LinkContainer
    {
        public GetPokemonTypesViewModel() {}

        /// <summary>
        /// The pokemon type name
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Damage relationships with other pokemon types
        /// </summary>
        public PokemonTypeRelationsViewModel TypeRelations { get; set; }
    }
}