using EighthGenerationCompetitive.Business.Entities.NatureAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.NatureAggregate
{
    internal static class DecreasedNatureMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<DecreasedNature>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(d => d.NatureId).SetIsRequired(true).SetElementName("natureId");
                map.MapMember(d => d.NatureName).SetIsRequired(true).SetElementName("natureName");
            });
        }
    }
}