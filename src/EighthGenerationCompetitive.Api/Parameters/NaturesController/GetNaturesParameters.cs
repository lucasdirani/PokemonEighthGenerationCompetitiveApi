using EighthGenerationCompetitive.Business.Parameters.NatureAggregate;
using EighthGenerationCompetitive.Business.Projections.NatureAggregate;
using System.ComponentModel.DataAnnotations;

namespace EighthGenerationCompetitive.Api.Parameters.NaturesController
{
    /// <summary>
    /// Filters the search by natures
    /// </summary>
    public class GetNaturesParameters : PaginationParameters, ISortParameters, IFilterNaturesParameters
    {
        /// <summary>
        /// Defines in which order the natures are returned. The "-natureName" filter returns natures in descending order by their name, and the default filter "natureName" in ascending order.
        /// </summary>
        public string SortByClause { get; set; } = "natureName";

        /// <summary>
        /// Filters the natures that increase a certain stat. Accepted values for this filter are HP, Attack, Defense, Special Attack, Special Defense or Speed.
        /// </summary>
        [MaxLength(15)]
        [RegularExpression("^([hH][pP]|[aA]ttack|[dD]efense|[sS]pecial [aA]ttack|[sS]pecial [dD]efense|[sS]peed|)$", ErrorMessage = "The IncreasedStat property must have one of the following values: HP, Attack, Defense, Special Attack, Special Defense, or Speed.")]
        public string IncreasedStat { get; set; }

        /// <summary>
        /// Filters the natures that lower a certain stat. Accepted values for this filter are HP, Attack, Defense, Special Attack, Special Defense or Speed.
        /// </summary>
        [MaxLength(15)]
        [RegularExpression("^([hH][pP]|[aA]ttack|[dD]efense|[sS]pecial [aA]ttack|[sS]pecial [dD]efense|[sS]peed|)$", ErrorMessage = "The DecreasedStat property must have one of the following values: HP, Attack, Defense, Special Attack, Special Defense, or Speed.")]
        public string DecreasedStat { get; set; }

        public FindNaturesParameters<NatureNameAndStatsProjection> MakeFindNatureParameters() =>
            new FindNaturesParameters<NatureNameAndStatsProjection>()
            {
                LimitDocuments = PageSize,
                SkipDocuments = PageNumber,
                OrderBy = SortByClause,
                Projection = nature => new NatureNameAndStatsProjection()
                {
                    NatureName = nature.NatureName,
                    NatureDecreasedStat = nature.NatureDecreasedStat,
                    NatureDecreasedStatIn = nature.NatureDecreasedStatIn,
                    NatureIncreasedStat = nature.NatureIncreasedStat,
                    NatureIncreasedStatIn = nature.NatureIncreasedStatIn
                },
                DecreasedStatName = DecreasedStat,
                IncreasedStatName = IncreasedStat,
            };
    }
}