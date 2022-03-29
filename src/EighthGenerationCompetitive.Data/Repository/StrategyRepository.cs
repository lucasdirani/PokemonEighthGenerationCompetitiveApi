using EighthGenerationCompetitive.Business.Entities.StrategyAggregate;
using EighthGenerationCompetitive.Business.Repositories;
using EighthGenerationCompetitive.Data.Context;

namespace EighthGenerationCompetitive.Data.Repository
{
    public class StrategyRepository : BaseRepository<Strategy>, IStrategyRepository
    {
        public StrategyRepository(IMongoContext context) : base(context) {}
    }
}