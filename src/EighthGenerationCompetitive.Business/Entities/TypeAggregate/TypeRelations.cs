using System.Collections.Generic;

namespace EighthGenerationCompetitive.Business.Entities.TypeAggregate
{
    public class TypeRelations
    {
        public IList<PokemonType> TypeRelationsNoDamageTo { get; set; }

        public IList<PokemonType> TypeRelationsHalfDamageTo { get; set; }

        public IList<PokemonType> TypeRelationsDoubleDamageTo { get; set; }

        public IList<PokemonType> TypeRelationsNoDamageFrom { get; set; }

        public IList<PokemonType> TypeRelationsHalfDamageFrom { get; set; }

        public IList<PokemonType> TypeRelationsDoubleDamageFrom { get; set; }
    }
}