using System.ComponentModel.DataAnnotations;

namespace EighthGenerationCompetitive.Api.V1.ViewModels.Users
{
    /// <summary>
    /// Represents the data required for a user to login and obtain a token to access API resources
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// The name as the user is identified.
        /// </summary>
        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(256, ErrorMessage = "The field {0} must be between {2} and {1} characters.", MinimumLength = 8)]
        public string UserName { get; set; }

        /// <summary>
        /// The password required to authenticate to the API.
        /// </summary>
        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters.", MinimumLength = 10)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{10,100}$", ErrorMessage = "The password must have at least one uppercase letter, one lowercase letter, one number and one special character.")]
        public string Password { get; set; }
    }
}