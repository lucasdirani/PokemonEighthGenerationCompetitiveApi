using MongoDB.Bson;

namespace EighthGenerationCompetitive.Business.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = ObjectId.GenerateNewId();
        }

        public ObjectId Id { get; set; }
    }
}