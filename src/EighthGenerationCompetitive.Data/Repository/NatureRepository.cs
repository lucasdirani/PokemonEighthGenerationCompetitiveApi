using EighthGenerationCompetitive.Business.Entities.NatureAggregate;
using EighthGenerationCompetitive.Business.Parameters.NatureAggregate;
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
    public class NatureRepository : BaseRepository<Nature>, INatureRepository
    {
        public NatureRepository(IMongoContext context) : base(context) { }

        public async Task<PagedList<TProjection>> FindNaturesAsync<TProjection>(
            FindNaturesParameters<TProjection> queryParameters) =>
            await _collection
                    .AsQueryable()
                    .ApplyStatsConditions(queryParameters.IncreasedStatName, queryParameters.DecreasedStatName)
                    .Select(queryParameters.Projection)
                    .OrderBy(queryParameters.OrderBy)
                    .ToPagedListAsync(queryParameters.SkipDocuments, queryParameters.LimitDocuments);

        public async Task<Nature> FindNatureByNameAsync(string natureName) =>
            await FindOneAsync(nature => nature.NatureName, RegexExpression.ApplyIgnoreCaseExpressionTo(natureName));

        public async Task<Nature> FindNatureByNameOnlyWithOurMonstersAsync(string natureName)
        {
            Expression<Func<Nature, object>> filterBy = nature => nature.NatureName;

            string regexFilter = RegexExpression.ApplyIgnoreCaseExpressionTo(natureName);

            var natureWithOurMonsters = await FindOneAsync(filterBy, regexFilter, projection: nature => new { nature.NatureName, nature.NaturePokemon });

            return new Nature() { NatureName = natureWithOurMonsters.NatureName, NaturePokemon = natureWithOurMonsters.NaturePokemon };
        }

        public async Task<Nature> FindNatureByNameOnlyWithOurMonstersFormsAsync(string natureName)
        {
            Expression<Func<Nature, object>> filterBy = nature => nature.NatureName;

            string regexFilter = RegexExpression.ApplyIgnoreCaseExpressionTo(natureName);

            var natureWithOurMonstersForms = await FindOneAsync(filterBy, regexFilter, projection: nature => new { nature.NatureName, nature.NaturePokemonForms });

            return new Nature() { NatureName = natureWithOurMonstersForms.NatureName, NaturePokemonForms = natureWithOurMonstersForms.NaturePokemonForms };
        }
    }
}