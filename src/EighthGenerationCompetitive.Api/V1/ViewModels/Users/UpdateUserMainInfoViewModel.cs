using EighthGenerationCompetitive.Api.Attributes;
using System.ComponentModel.DataAnnotations;

namespace EighthGenerationCompetitive.Api.V1.ViewModels.Users
{
    /// <summary>
    /// Represents the data required for a user to update its main registration info
    /// </summary>
    public class UpdateUserMainInfoViewModel
    {
        /// <summary>
        /// User's Friend Code number on Nintendo 3DS.
        /// </summary>
        [StringLength(14, ErrorMessage = "The field {0} must have {2} characters.", MinimumLength = 14)]
        [Nintendo3DsFriendCode(ErrorMessage = "The Nintendo 3DS friend code is in an invalid format.")]
        public string Nintendo3dsFriendCode { get; set; }

        /// <summary>
        /// User's Friend Code number on Nintendo Switch.
        /// </summary>
        [StringLength(17, ErrorMessage = "The field {0} must have {2} characters.", MinimumLength = 17)]
        [NintendoSwitchFriendCode(ErrorMessage = "The Nintendo Switch friend code is in an invalid format.")]
        public string NintendoSwitchFriendCode { get; set; }

        /// <summary>
        /// The username in PlayPokemonShowdown.
        /// </summary>
        [StringLength(40, ErrorMessage = "The field {0} must be between {2} and {1} characters.", MinimumLength = 1)]
        public string ShowdownNickname { get; set; }
    }
}