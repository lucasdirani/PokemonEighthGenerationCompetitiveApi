using EighthGenerationCompetitive.Business.Entities.NatureAggregate;
using EighthGenerationCompetitive.Data.Utils;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EighthGenerationCompetitive.Data.Filters.NatureAggregate.Stats
{
    internal class NatureStatsFilters : INatureStatsFilters, IIncreasedStatFilter, IDecreasedStatFilter
    {
        private IMongoQueryable<Nature> _natures;

        private NatureStatsFilters(IMongoQueryable<Nature> natures)
        {
            _natures = natures;
        }

        public static IDecreasedStatFilter ApplyFiltersFrom(IMongoQueryable<Nature> natures)
        {
            return new NatureStatsFilters(natures);
        }

        public IIncreasedStatFilter ApplyDecreasedStat(string decreasedStatName)
        {
            if (!string.IsNullOrEmpty(decreasedStatName))
            {
                string regexExpression = RegexExpression.ApplyIgnoreCaseExpressionTo(decreasedStatName);

                var decreasedStatNameFilter = Builders<Nature>.Filter.Regex(n => n.NatureDecreasedStat.StatName, new BsonRegularExpression(regexExpression));

                _natures = _natures.Where(p => decreasedStatNameFilter.Inject());
            }

            return this;
        }

        public IMongoQueryable<Nature> ApplyFilters()
        {
            return _natures;
        }

        public INatureStatsFilters ApplyIncreasedStat(string increasedStatName)
        {
            if (!string.IsNullOrEmpty(increasedStatName))
            {
                string regexExpression = RegexExpression.ApplyIgnoreCaseExpressionTo(increasedStatName);

                var increasedStatNameFilter = Builders<Nature>.Filter.Regex(n => n.NatureIncreasedStat.StatName, new BsonRegularExpression(regexExpression));

                _natures = _natures.Where(p => increasedStatNameFilter.Inject());
            }

            return this;
        }
    }
}