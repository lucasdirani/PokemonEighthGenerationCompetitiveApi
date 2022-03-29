using RiskFirst.Hateoas.Models;
using System.Collections.Generic;

namespace EighthGenerationCompetitive.Api.V1.ViewModels.Users
{
    /// <summary>
    /// Represents the User returned from the endpoint GetUserByName
    /// </summary>
    public class GetUserViewModel : LinkContainer
    {
        /// <summary>
        /// The name of the searched user
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// User's friend code on Nintendo 3DS
        /// </summary>
        public string Nintendo3dsFriendCode { get; set; }

        /// <summary>
        /// User's friend code on Nintendo Switch
        /// </summary>
        public string NintendoSwitchFriendCode { get; set; }

        /// <summary>
        /// The name of the user searched in PlayPokemonShowdown
        /// </summary>
        public string ShowdownNickname { get; set; }

        /// <summary>
        /// Additional contacts of the searched user
        /// </summary>
        public ICollection<GetUserContactViewModel> ApplicationContacts { get; set; }
    }
}