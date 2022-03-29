namespace EighthGenerationCompetitive.Data.Filters.NatureAggregate.Stats
{
    internal interface IDecreasedStatFilter
    {
        IIncreasedStatFilter ApplyDecreasedStat(string decreasedStatName);
    }
}