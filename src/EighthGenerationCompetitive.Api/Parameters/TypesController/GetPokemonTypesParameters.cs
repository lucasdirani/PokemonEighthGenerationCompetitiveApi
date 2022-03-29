using EighthGenerationCompetitive.Business.Projections.TypeAggregate;
using EighthGenerationCompetitive.Business.Parameters.TypeAggregate;
using System.ComponentModel.DataAnnotations;

namespace EighthGenerationCompetitive.Api.Parameters.TypesController
{
    /// <summary>
    /// Filters the search by Pokémon types
    /// </summary>
    public class GetPokemonTypesParameters : PaginationParameters, ISortParameters, IFilterPokemonTypesParameters
    {
        /// <summary>
        /// Defines in which order the Pokémon types are returned. The "-typeName" filter returns types in descending order by their name, and the default filter "typeName" in ascending order.
        /// </summary>
        [RegularExpression("^[-]?(typeName)$", ErrorMessage = "The sort by clause must contain the typeName property in descending or ascending order.")]
        public string SortByClause { get; set; } = "typeName";

        /// <summary>
        /// Filters by Pokémon types that don't affect the specified types. The searched names must be separated by a comma, for example: "Fire, Water, Grass".
        /// </summary>
        [MaxLength(130)]
        [RegularExpression("^(\\w+)(,\\s*\\w+)*$", ErrorMessage = "The NoDamageTo property must have the name of the types separated by a comma.")]
        public string NoDamageTo { get; set; }

        /// <summary>
        /// Filters by Pokemon types that deal half damage to specified types. The searched names must be separated by a comma, for example: "Fire, Water, Grass".
        /// </summary>
        [MaxLength(130)]
        [RegularExpression("^(\\w+)(,\\s*\\w+)*$", ErrorMessage = "The HalfDamageTo property must have the name of the types separated by a comma.")]
        public string HalfDamageTo { get; set; }

        /// <summary>
        /// Filters by Pokémon types that deal double damage to specified types. The searched names must be separated by a comma, for example: "Fire, Water, Grass".
        /// </summary>
        [MaxLength(130)]
        [RegularExpression("^(\\w+)(,\\s*\\w+)*$", ErrorMessage = "The DoubleDamageTo property must have the name of the types separated by a comma.")]
        public string DoubleDamageTo { get; set; }

        /// <summary>
        /// Filters by Pokémon types that don't take damage from the specified types. The searched names must be separated by a comma, for example: "Fire, Water, Grass".
        /// </summary>
        [MaxLength(130)]
        [RegularExpression("^(\\w+)(,\\s*\\w+)*$", ErrorMessage = "The NoDamageFrom property must have the name of the types separated by a comma.")]
        public string NoDamageFrom { get; set; }

        /// <summary>
        /// Filters by Pokémon types that take half damage from the specified types. The searched names must be separated by a comma, for example: "Fire, Water, Grass".
        /// </summary>
        [MaxLength(130)]
        [RegularExpression("^(\\w+)(,\\s*\\w+)*$", ErrorMessage = "The HalfDamageFrom property must have the name of the types separated by a comma.")]
        public string HalfDamageFrom { get; set; }

        /// <summary>
        /// Filters by Pokémon types that suffer double the damage of the specified types. The searched names must be separated by a comma, for example: "Fire, Water, Grass".
        /// </summary>
        [MaxLength(130)]
        [RegularExpression("^(\\w+)(,\\s*\\w+)*$", ErrorMessage = "The DoubleDamageFrom property must have the name of the types separated by a comma.")]
        public string DoubleDamageFrom { get; set; }

        public FindPokemonTypesParameters<TypeNameAndRelationsProjection> MakeFindPokemonTypeParameters() =>
            new FindPokemonTypesParameters<TypeNameAndRelationsProjection>()
            {
                LimitDocuments = PageSize,
                SkipDocuments = PageNumber,
                OrderBy = SortByClause,
                Projection = pokemonType => new TypeNameAndRelationsProjection() 
                { 
                    TypeName = pokemonType.TypeName,
                    TypeRelations = pokemonType.TypeRelations
                },
                TypeRelationsConditions = new TypeRelationsParameters(
                    NoDamageTo,
                    HalfDamageTo,
                    DoubleDamageTo,
                    NoDamageFrom,
                    HalfDamageFrom,
                    DoubleDamageFrom
                )
            };
    }
}