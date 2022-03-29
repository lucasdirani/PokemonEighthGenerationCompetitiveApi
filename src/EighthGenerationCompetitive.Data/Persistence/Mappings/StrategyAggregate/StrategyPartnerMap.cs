using EighthGenerationCompetitive.Business.Entities.StrategyAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.StrategyAggregate
{
    internal static class StrategyPartnerMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<StrategyPartner>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(s => s.StrategyId).SetIsRequired(true).SetElementName("strategyId");
                map.MapMember(s => s.StrategyName).SetIsRequired(true).SetElementName("strategyName");
            });
        }
    }
}