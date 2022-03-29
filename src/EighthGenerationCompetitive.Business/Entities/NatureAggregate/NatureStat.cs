using System.Collections.Generic;

namespace EighthGenerationCompetitive.Business.Entities.NatureAggregate
{
    public class NatureStat
    {
        public string StatName { get; set; }
        public IList<IncreasedNature> StatIncreasedNatures { get; set; }
        public IList<DecreasedNature> StatDecreasedNatures { get; set; }
    }
}