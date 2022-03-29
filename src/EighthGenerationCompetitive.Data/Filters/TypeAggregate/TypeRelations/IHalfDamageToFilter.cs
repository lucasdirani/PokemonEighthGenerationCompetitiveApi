namespace EighthGenerationCompetitive.Data.Filters.TypeAggregate.TypeRelations
{
    internal interface IHalfDamageToFilter
    {
        INoDamageToFilter ApplyHalfDamageTo(string[] halfDamageTo);
    }
}