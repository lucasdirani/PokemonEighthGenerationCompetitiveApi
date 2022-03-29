using EighthGenerationCompetitive.Business.Entities.TypeAggregate;
using EighthGenerationCompetitive.Business.Parameters.TypeAggregate;
using EighthGenerationCompetitive.Business.Repositories;
using EighthGenerationCompetitive.Business.Utils;
using EighthGenerationCompetitive.Data.Context;
using EighthGenerationCompetitive.Data.Extensions;
using EighthGenerationCompetitive.Data.Utils;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Data.Repository
{
    public class PokemonTypeRepository : BaseRepository<Business.Entities.TypeAggregate.Type>, IPokemonTypeRepository
    {
        public PokemonTypeRepository(IMongoContext context) : base(context)
        {
        }

        public async Task<PagedList<TProjection>> FindPokemonTypesAsync<TProjection>(
            FindPokemonTypesParameters<TProjection> queryParameters) =>
            await _collection
                    .AsQueryable()
                    .ApplyTypeRelationsConditions(queryParameters.TypeRelationsConditions)
                    .Select(queryParameters.Projection)
                    .OrderBy(queryParameters.OrderBy)
                    .ToPagedListAsync(queryParameters.SkipDocuments, queryParameters.LimitDocuments);

        public async Task<Business.Entities.TypeAggregate.Type> FindPokemonTypeByNameAsync(string typeName) =>
            await FindOneAsync(pokemonType => pokemonType.TypeName, RegexExpression.ApplyIgnoreCaseExpressionTo(typeName));

        public async Task<Business.Entities.TypeAggregate.Type> FindPokemonTypeByNameOnlyWithOurMovesAsync(string typeName)
        {
            Expression<Func<Business.Entities.TypeAggregate.Type, object>> filterBy = pokemonType => pokemonType.TypeName;
            
            string regexFilter = RegexExpression.ApplyIgnoreCaseExpressionTo(typeName);

            var pokemonTypeWithOurMoves = await FindOneAsync(filterBy, regexFilter, projection: type => new { type.TypeName, type.TypeMoves });

            return new Business.Entities.TypeAggregate.Type() { TypeName = pokemonTypeWithOurMoves.TypeName, TypeMoves = pokemonTypeWithOurMoves.TypeMoves };
        }

        public async Task<Business.Entities.TypeAggregate.Type> FindPokemonTypeByNameOnlyWithOurMonstersAsync(string typeName)
        {
            Expression<Func<Business.Entities.TypeAggregate.Type, object>> filterBy = pokemonType => pokemonType.TypeName;

            string regexFilter = RegexExpression.ApplyIgnoreCaseExpressionTo(typeName);

            var pokemonTypeWithOurMonsters = await FindOneAsync(filterBy, regexFilter, projection: type => new { type.TypeName, type.TypePokemon });

            return new Business.Entities.TypeAggregate.Type() { TypeName = pokemonTypeWithOurMonsters.TypeName, TypePokemon = pokemonTypeWithOurMonsters.TypePokemon };
        }

        public async Task<Business.Entities.TypeAggregate.Type> FindPokemonTypeByNameOnlyWithOurMonstersFormsAsync(string typeName)
        {
            Expression<Func<Business.Entities.TypeAggregate.Type, object>> filterBy = pokemonType => pokemonType.TypeName;

            string regexFilter = RegexExpression.ApplyIgnoreCaseExpressionTo(typeName);

            var pokemonTypeWithOurMonstersForms = await FindOneAsync(filterBy, regexFilter, projection: type => new { type.TypeName, type.TypePokemonForms });

            return new Business.Entities.TypeAggregate.Type() { TypeName = pokemonTypeWithOurMonstersForms.TypeName, TypePokemonForms = pokemonTypeWithOurMonstersForms.TypePokemonForms };
        }
    }
} 