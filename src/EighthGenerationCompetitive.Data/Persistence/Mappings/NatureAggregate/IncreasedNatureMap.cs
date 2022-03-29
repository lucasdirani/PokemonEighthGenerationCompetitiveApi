using EighthGenerationCompetitive.Business.Entities.NatureAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.NatureAggregate
{
    internal static class IncreasedNatureMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<IncreasedNature>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(i => i.NatureId).SetIsRequired(true).SetElementName("natureId");
                map.MapMember(i => i.NatureName).SetIsRequired(true).SetElementName("natureName");
            });
        }
    }
}