namespace EighthGenerationCompetitive.Data.Filters.NatureAggregate.Stats
{
    internal interface IIncreasedStatFilter
    {
        INatureStatsFilters ApplyIncreasedStat(string increasedStatName);
    }
}