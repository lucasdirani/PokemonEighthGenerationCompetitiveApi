using RiskFirst.Hateoas.Models;
using System.Collections.Generic;

namespace EighthGenerationCompetitive.Api.V1.ViewModels.Types
{
    /// <summary>
    /// Represents the Pokemon type returned from the endpoint GetPokemonTypeByName
    /// </summary>
    public class GetPokemonTypeViewModel : LinkContainer
    {
        public GetPokemonTypeViewModel() 
        {
            TypePokemon = new List<PokemonTypeCreatureViewModel>();
            TypePokemonForms = new List<PokemonTypeCreatureFormViewModel>();
            TypeMoves = new List<PokemonTypeMoveViewModel>();
        }

        /// <summary>
        /// The pokemon type name
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Pokémon that have the searched type
        /// </summary>
        public List<PokemonTypeCreatureViewModel> TypePokemon { get; set; }

        /// <summary>
        /// Pokémon forms that have the searched type
        /// </summary>
        public List<PokemonTypeCreatureFormViewModel> TypePokemonForms { get; set; }

        /// <summary>
        /// Moves that have the searched type
        /// </summary>
        public List<PokemonTypeMoveViewModel> TypeMoves { get; set; }

        /// <summary>
        /// Damage relationships with other pokemon types
        /// </summary>
        public PokemonTypeRelationsViewModel TypeRelations { get; set; }
    }
}