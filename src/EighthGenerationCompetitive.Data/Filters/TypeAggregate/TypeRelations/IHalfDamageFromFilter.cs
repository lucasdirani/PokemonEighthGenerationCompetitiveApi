namespace EighthGenerationCompetitive.Data.Filters.TypeAggregate.TypeRelations
{
    internal interface IHalfDamageFromFilter
    {
        IDoubleDamageFromFilter ApplyHalfDamageFrom(string[] halfDamageFrom);
    }
}