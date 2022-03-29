using RiskFirst.Hateoas.Models;

namespace EighthGenerationCompetitive.Api.V1.ViewModels.Natures
{
    /// <summary>
    /// Represents the natures returned from the endpoint GetNatures
    /// </summary>
    public class GetNaturesViewModel : LinkContainer
    {
        /// <summary>
        /// The nature name
        /// </summary>
        public string NatureName { get; set; }

        /// <summary>
        /// The stat that is decreased by the effect of the fetched nature
        /// </summary>
        public NatureStatViewModel NatureDecreasedStat { get; set; }

        /// <summary>
        /// The stat that is increased by the effect of the fetched nature
        /// </summary>
        public NatureStatViewModel NatureIncreasedStat { get; set; }

        /// <summary>
        /// The percentage decreased by the effect of nature
        /// </summary>
        public decimal NatureDecreasedStatIn { get; set; }

        /// <summary>
        /// The percentage increased by the effect of nature
        /// </summary>
        public decimal NatureIncreasedStatIn { get; set; }
    }
}