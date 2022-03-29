using EighthGenerationCompetitive.Business.Entities.NatureAggregate;
using System;
using System.Linq.Expressions;

namespace EighthGenerationCompetitive.Business.Parameters.NatureAggregate
{
    public class FindNaturesParameters<TFindResult>
    {
        public Expression<Func<Nature, TFindResult>> Projection { get; set; }

        public string IncreasedStatName { get; set; }

        public string DecreasedStatName { get; set; }

        public int SkipDocuments { get; set; }

        public int LimitDocuments { get; set; }

        public string OrderBy { get; set; }
    }
}