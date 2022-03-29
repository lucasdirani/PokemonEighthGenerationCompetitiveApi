namespace EighthGenerationCompetitive.Data.Filters.TypeAggregate.TypeRelations
{
    internal interface INoDamageToFilter
    {
        IDoubleDamageToFilter ApplyNoDamageTo(string[] noDamageTo);
    }
}