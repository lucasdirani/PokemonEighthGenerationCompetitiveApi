namespace EighthGenerationCompetitive.Data.Filters.TypeAggregate.TypeRelations
{
    internal interface INoDamageFromFilter
    {
        IHalfDamageFromFilter ApplyNoDamageFrom(string[] noDamageFrom);
    }
}