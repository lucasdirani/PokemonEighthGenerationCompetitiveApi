using EighthGenerationCompetitive.Business.Entities;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings
{
    internal static class EntityTypeMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Entity>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdMember(e => e.Id);
            });
        }
    }
}