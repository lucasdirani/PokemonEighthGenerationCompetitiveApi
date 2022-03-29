using EighthGenerationCompetitive.Business.Entities.NatureAggregate;

namespace EighthGenerationCompetitive.Business.Projections.NatureAggregate
{
    public class NatureNameAndStatsProjection
    {
        public string NatureName { get; set; }
        public NatureStat NatureDecreasedStat { get; set; }
        public NatureStat NatureIncreasedStat { get; set; }
        public decimal NatureDecreasedStatIn { get; set; }
        public decimal NatureIncreasedStatIn { get; set; }
    }
}