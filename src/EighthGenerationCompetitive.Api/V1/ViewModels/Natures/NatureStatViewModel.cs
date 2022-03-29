using System.Collections.Generic;

namespace EighthGenerationCompetitive.Api.V1.ViewModels.Natures
{
    /// <summary>
    /// Represents a stat affected by a nature
    /// </summary>
    public class NatureStatViewModel
    {
        /// <summary>
        /// The stat name
        /// </summary>
        public string StatName { get; set; }

        /// <summary>
        /// Other natures that increase the stat
        /// </summary>
        public IList<IncreasedNatureViewModel> StatIncreasedNatures { get; set; }

        /// <summary>
        /// Other natures that decrease the stat
        /// </summary>
        public IList<DecreasedNatureViewModel> StatDecreasedNatures { get; set; }
    }
}