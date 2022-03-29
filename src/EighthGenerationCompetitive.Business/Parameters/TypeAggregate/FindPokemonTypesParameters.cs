using EighthGenerationCompetitive.Business.Entities.TypeAggregate;
using System;
using System.Linq.Expressions;

namespace EighthGenerationCompetitive.Business.Parameters.TypeAggregate
{
    public class FindPokemonTypesParameters<TFindResult>
    {
        public Expression<Func<Entities.TypeAggregate.Type,TFindResult>> Projection { get; set; }

        public TypeRelationsParameters TypeRelationsConditions { get; set; }

        public int SkipDocuments { get; set; }

        public int LimitDocuments { get; set; }

        public string OrderBy { get; set; }
    }
}