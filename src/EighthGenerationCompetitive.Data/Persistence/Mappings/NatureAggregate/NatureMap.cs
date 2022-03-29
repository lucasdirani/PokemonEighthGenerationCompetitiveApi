using EighthGenerationCompetitive.Business.Entities.NatureAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.NatureAggregate
{
    internal static class NatureMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Nature>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(n => n.NatureId).SetIsRequired(true).SetElementName("natureId");
                map.MapMember(n => n.NatureName).SetIsRequired(true).SetElementName("natureName");
                map.MapMember(n => n.NatureDecreasedStat).SetIsRequired(true).SetElementName("natureDecreasedStat");
                map.MapMember(n => n.NatureDecreasedStatIn).SetIsRequired(true).SetElementName("natureDecreasedStatIn");
                map.MapMember(n => n.NatureIncreasedStat).SetIsRequired(true).SetElementName("natureIncreasedStat");
                map.MapMember(n => n.NatureIncreasedStatIn).SetIsRequired(true).SetElementName("natureIncreasedStatIn");
                map.MapMember(n => n.NaturePokemon).SetIsRequired(false).SetElementName("naturePokemon");
                map.MapMember(n => n.NaturePokemonForms).SetIsRequired(false).SetElementName("naturePokemonForms");
            });
        }
    }
}