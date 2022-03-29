using EighthGenerationCompetitive.Business.Entities.StrategyAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.StrategyAggregate
{
    internal static class StrategyCounterMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<StrategyCounter>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(s => s.StrategyId).SetIsRequired(true).SetElementName("strategyId");
                map.MapMember(s => s.StrategyName).SetIsRequired(true).SetElementName("strategyName");
            });
        }
    }
}