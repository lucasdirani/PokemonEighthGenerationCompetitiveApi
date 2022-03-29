using EighthGenerationCompetitive.Business.Entities.NatureAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.NatureAggregate
{
    internal static class NatureStatMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<NatureStat>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(s => s.StatName).SetIsRequired(true).SetElementName("statName");
                map.MapMember(s => s.StatIncreasedNatures).SetIsRequired(false).SetElementName("statIncreaseNatures");
                map.MapMember(s => s.StatDecreasedNatures).SetIsRequired(false).SetElementName("statDecreaseNatures");
            });
        }
    }
}