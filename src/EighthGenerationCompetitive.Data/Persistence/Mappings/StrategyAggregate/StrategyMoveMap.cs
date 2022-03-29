using EighthGenerationCompetitive.Business.Entities.StrategyAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.StrategyAggregate
{
    internal static class StrategyMoveMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<StrategyMove>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(s => s.StrategyFirstMoves).SetIsRequired(true).SetElementName("strategyFirstMoves");
                map.MapMember(s => s.StrategySecondMoves).SetIsRequired(false).SetElementName("strategySecondMoves");
                map.MapMember(s => s.StrategyThirdMoves).SetIsRequired(false).SetElementName("strategyThirdMoves");
                map.MapMember(s => s.StrategyFourthMoves).SetIsRequired(false).SetElementName("strategyFourthMoves");
            });
        }
    }
}