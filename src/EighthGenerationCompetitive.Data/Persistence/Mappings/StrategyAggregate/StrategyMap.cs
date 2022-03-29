using EighthGenerationCompetitive.Business.Entities.StrategyAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.StrategyAggregate
{
    internal static class StrategyMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Strategy>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(p => p.StrategyAbilities).SetIsRequired(true).SetElementName("strategyAbilities");
                map.MapMember(p => p.StrategyCheckers).SetIsRequired(false).SetElementName("strategyCheckers");
                map.MapMember(p => p.StrategyCounters).SetIsRequired(false).SetElementName("strategyCounters");
                map.MapMember(p => p.StrategyCreationDate).SetIsRequired(true).SetElementName("strategyCreationDate");
                map.MapMember(p => p.StrategyCreator).SetIsRequired(true).SetElementName("strategyCreator");
                map.MapMember(p => p.StrategyDetails).SetIsRequired(true).SetElementName("strategyDetails");
                map.MapMember(p => p.StrategyEffortValues).SetIsRequired(true).SetElementName("strategyEffortValues");
                map.MapMember(p => p.StrategyId).SetIsRequired(true).SetElementName("strategyId");
                map.MapMember(p => p.StrategyIndividualValues).SetIsRequired(true).SetElementName("strategyIndividualValues");
                map.MapMember(p => p.StrategyItems).SetIsRequired(false).SetElementName("strategyItems");
                map.MapMember(p => p.StrategyName).SetIsRequired(true).SetElementName("strategyName");
                map.MapMember(p => p.StrategyNatures).SetIsRequired(true).SetElementName("strategyNatures");
                map.MapMember(p => p.StrategyPartners).SetIsRequired(false).SetElementName("strategyPartners");
                map.MapMember(p => p.StrategyPokemon).SetIsRequired(false).SetElementName("strategyPokemon");
                map.MapMember(p => p.StrategyPokemonForm).SetIsRequired(false).SetElementName("strategyPokemonForm");
                map.MapMember(p => p.StrategyTier).SetIsRequired(true).SetElementName("strategyTier");
                map.MapMember(p => p.StrategyMove).SetIsRequired(true).SetElementName("strategyMove");
            });
        }
    }
}