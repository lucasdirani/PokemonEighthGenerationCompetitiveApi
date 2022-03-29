using EighthGenerationCompetitive.Business.Entities.StrategyAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.StrategyAggregate
{
    internal static class PokemonItemMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<PokemonItem>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(p => p.ItemDetails).SetIsRequired(true).SetElementName("itemDetails");
                map.MapMember(p => p.ItemName).SetIsRequired(true).SetElementName("itemName");
                map.MapMember(p => p.ItemId).SetIsRequired(true).SetElementName("itemId");
            });
        }
    }
}