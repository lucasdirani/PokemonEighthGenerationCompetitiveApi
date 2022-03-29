using EighthGenerationCompetitive.Data.Persistence.Mappings;
using EighthGenerationCompetitive.Data.Persistence.Mappings.Registration;
using MongoDB.Bson.Serialization.Conventions;

namespace EighthGenerationCompetitive.Data.Persistence
{
    public static class MongoDbConfiguration
    {
        public static void Configure()
        {
            RegisterMappings();

            RegisterConventionPack();
        }

        private static void RegisterMappings()
        {
            EntityTypeMap.Configure();

            TypeAggregateMapRegister.Register();

            NatureAggregateMapRegister.Register();

            StrategyAggregateMapRegister.Register();
        }

        private static void RegisterConventionPack()
        {
            var conventionPack = new ConventionPack
            {
                new IgnoreExtraElementsConvention(true),
                new IgnoreIfDefaultConvention(true)
            };

            ConventionRegistry.Register("Database Conventions", conventionPack, t => true);
        }
    }
}