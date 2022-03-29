using System.Collections.Generic;

namespace EighthGenerationCompetitive.Api.V1.ViewModels.Types
{
    /// <summary>
    /// Damage relationships with other pokemon types
    /// </summary>
    public class PokemonTypeRelationsViewModel
    {
        public PokemonTypeRelationsViewModel() 
        {
            TypeRelationsNoDamageTo = new List<PokemonTypeCreatureTypeViewModel>();
            TypeRelationsHalfDamageTo = new List<PokemonTypeCreatureTypeViewModel>();
            TypeRelationsDoubleDamageTo = new List<PokemonTypeCreatureTypeViewModel>();
            TypeRelationsNoDamageFrom = new List<PokemonTypeCreatureTypeViewModel>(); 
            TypeRelationsHalfDamageFrom = new List<PokemonTypeCreatureTypeViewModel>();
            TypeRelationsDoubleDamageFrom = new List<PokemonTypeCreatureTypeViewModel>();
        }

        /// <summary>
        /// The pokemon types that are not affected by the searched type
        /// </summary>
        public List<PokemonTypeCreatureTypeViewModel> TypeRelationsNoDamageTo { get; set; }

        /// <summary>
        /// The pokemon types that take half damage by the searched type
        /// </summary>
        public List<PokemonTypeCreatureTypeViewModel> TypeRelationsHalfDamageTo { get; set; }

        /// <summary>
        /// The pokemon types that take double damage by the researched type
        /// </summary>
        public List<PokemonTypeCreatureTypeViewModel> TypeRelationsDoubleDamageTo { get; set; }

        /// <summary>
        /// The pokemon types that don't affect the searched type
        /// </summary>
        public List<PokemonTypeCreatureTypeViewModel> TypeRelationsNoDamageFrom { get; set; }

        /// <summary>
        /// The pokemon types that deal half damage on the searched type
        /// </summary>
        public List<PokemonTypeCreatureTypeViewModel> TypeRelationsHalfDamageFrom { get; set; }

        /// <summary>
        /// The pokemon types that deal double the damage on the searched type
        /// </summary>
        public List<PokemonTypeCreatureTypeViewModel> TypeRelationsDoubleDamageFrom { get; set; }
    }
}