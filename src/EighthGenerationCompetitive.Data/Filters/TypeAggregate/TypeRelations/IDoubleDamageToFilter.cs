namespace EighthGenerationCompetitive.Data.Filters.TypeAggregate.TypeRelations
{
    internal interface IDoubleDamageToFilter
    {
        INoDamageFromFilter ApplyDoubleDamageTo(string[] doubleDamageTo);
    }
}