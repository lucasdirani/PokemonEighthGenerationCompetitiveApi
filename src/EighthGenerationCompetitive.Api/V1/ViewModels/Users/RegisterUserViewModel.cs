using EighthGenerationCompetitive.Api.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EighthGenerationCompetitive.Api.V1.ViewModels.Users
{
    /// <summary>
    /// Represents the registration data required for a user to register and obtain a token to access API resources
    /// </summary>
    public class RegisterUserViewModel
    {
        /// <summary>
        /// The name as the user will be identified. It needs to be unique.
        /// </summary>
        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(256, ErrorMessage = "The field {0} must be between {2} and {1} characters.", MinimumLength = 8)]
        public string UserName { get; set; }

        /// <summary>
        /// The user's primary email. It needs to be unique.
        /// </summary>
        [Required(ErrorMessage = "The field {0} is required.")]
        [EmailAddress(ErrorMessage = "The field {0} is in an invalid format.")]
        public string Email { get; set; }

        /// <summary>
        /// The password required to authenticate to the API. Must have at least one uppercase letter, one lowercase letter, one number and one special character.
        /// </summary>
        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters.", MinimumLength = 10)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{10,100}$", ErrorMessage = "The password must have at least one uppercase letter, one lowercase letter, one number and one special character.")]
        public string Password { get; set; }

        /// <summary>
        /// Retry of the password required to authenticate to the API.
        /// </summary>
        [Required(ErrorMessage = "The field {0} is required.")]
        [Compare("Password", ErrorMessage = "The passwords are different.")]
        public string ConfirmPassword { get; set; }

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

        /// <summary>
        /// Informs whether non-sensitive registration data will be visible to other users.
        /// </summary>
        [Required(ErrorMessage = "You must inform if your non-sensitive registration information will be visible to other users.")]
        public bool ShowProfile { get; set; }

        /// <summary>
        /// Additional user contacts on other platforms.
        /// </summary>
        [Required(ErrorMessage = "You must provide at least one additional contact.")]
        [ApplicationContact(ErrorMessage = "You can only enter one contact per type.")]
        public IEnumerable<RegisterUserContactViewModel> ApplicationContacts { get; set; }
    }
}