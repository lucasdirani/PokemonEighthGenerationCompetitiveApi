using EighthGenerationCompetitive.Business.Entities.TypeAggregate;
using MongoDB.Bson.Serialization;

namespace EighthGenerationCompetitive.Data.Persistence.Mappings.TypeAggregate
{
    internal static class PokemonMoveMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<PokemonMove>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(p => p.MoveAccuracy).SetIsRequired(false).SetElementName("moveAccuracy");
                map.MapMember(p => p.MoveCategory).SetIsRequired(true).SetElementName("moveCategory");
                map.MapMember(p => p.MoveDetails).SetIsRequired(true).SetElementName("moveDetails");
                map.MapMember(p => p.MoveId).SetIsRequired(true).SetElementName("moveId");
                map.MapMember(p => p.MoveName).SetIsRequired(true).SetElementName("moveName");
                map.MapMember(p => p.MovePower).SetIsRequired(false).SetElementName("movePower");
                map.MapMember(p => p.MovePP).SetIsRequired(false).SetElementName("movePP");
            });
        }
    }
}