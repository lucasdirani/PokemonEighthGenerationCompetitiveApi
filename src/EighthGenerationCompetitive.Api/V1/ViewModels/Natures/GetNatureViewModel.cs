using RiskFirst.Hateoas.Models;
using System.Collections.Generic;

namespace EighthGenerationCompetitive.Api.V1.ViewModels.Natures
{
    /// <summary>
    /// Represents the Nature returned from the endpoint GetNatureByName
    /// </summary>
    public class GetNatureViewModel : LinkContainer
    {
        public GetNatureViewModel()
        {
            NaturePokemon = new List<NaturePokemonViewModel>();
            NaturePokemonForms = new List<NaturePokemonFormViewModel>();
        }

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

        /// <summary>
        /// The Pokémon that have competitively strategies with this nature
        /// </summary>
        public IList<NaturePokemonViewModel> NaturePokemon { get; set; }

        /// <summary>
        /// The Pokémon forms that have competitively strategies with this nature
        /// </summary>
        public IList<NaturePokemonFormViewModel> NaturePokemonForms { get; set; }
    }
}