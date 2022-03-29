using System.Collections.Generic;

namespace EighthGenerationCompetitive.Business.Entities.TypeAggregate
{
    public class Type : Entity
    {
        public string TypeName { get; set; }
        public string TypeId { get; set; }
        public IList<Pokemon> TypePokemon { get; set; }
        public IList<PokemonForm> TypePokemonForms { get; set; }
        public IList<PokemonMove> TypeMoves { get; set; }
        public TypeRelations TypeRelations { get; set; }
    }
}