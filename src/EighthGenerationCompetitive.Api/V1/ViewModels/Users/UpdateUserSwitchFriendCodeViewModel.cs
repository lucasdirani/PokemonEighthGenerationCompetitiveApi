using EighthGenerationCompetitive.Api.Attributes;
using System.ComponentModel.DataAnnotations;

namespace EighthGenerationCompetitive.Api.V1.ViewModels.Users
{
    /// <summary>
    /// Represents the data required for a user to update its Nintendo Switch Friend Code
    /// </summary>
    public class UpdateUserSwitchFriendCodeViewModel
    {
        /// <summary>
        /// User's Friend Code number on Nintendo Switch.
        /// </summary>
        [StringLength(17, ErrorMessage = "The field {0} must have {2} characters.", MinimumLength = 17)]
        [NintendoSwitchFriendCode(ErrorMessage = "The Nintendo Switch friend code is in an invalid format.")]
        public string NintendoSwitchFriendCode { get; set; }
    }
}