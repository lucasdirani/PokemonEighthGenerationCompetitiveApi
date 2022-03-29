using EighthGenerationCompetitive.Api.Attributes;
using System.ComponentModel.DataAnnotations;

namespace EighthGenerationCompetitive.Api.V1.ViewModels.Users
{
    /// <summary>
    /// Represents the data required for a user to update its Nintendo 3ds Friend Code
    /// </summary>
    public class UpdateUser3DsFriendCodeViewModel
    {
        /// <summary>
        /// User's Friend Code number on Nintendo 3DS.
        /// </summary>
        [StringLength(14, ErrorMessage = "The field {0} must have {2} characters.", MinimumLength = 14)]
        [Nintendo3DsFriendCode(ErrorMessage = "The Nintendo 3DS friend code is in an invalid format.")]
        public string Nintendo3dsFriendCode { get; set; }
    }
}