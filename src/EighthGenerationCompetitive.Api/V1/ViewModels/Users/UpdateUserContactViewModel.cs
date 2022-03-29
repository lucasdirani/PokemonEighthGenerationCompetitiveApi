using EighthGenerationCompetitive.Api.Identity.Models;
using System.ComponentModel.DataAnnotations;

namespace EighthGenerationCompetitive.Api.V1.ViewModels.Users
{
    /// <summary>
    /// Represents the data required for a user to update one of their contacts
    /// </summary>
    public class UpdateUserContactViewModel
    {
        /// <summary>
        /// The description of the user's contact (link, username, etc.).
        /// </summary>
        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(300, ErrorMessage = "The field {0} must be between {2} and {1} characters.", MinimumLength = 4)]
        public string Description { get; set; }

        /// <summary>
        /// The user's contact type. Must have one of the following values: 0 (Facebook), 1 (Twitter), 2 (Instagram), 3 (Reddit), 4 (Github) and 5 (Website).
        /// </summary>
        [EnumDataType(typeof(ApplicationContactType), ErrorMessage = "The field must have one of the following values: 0 (Facebook), 1 (Twitter), 2 (Instagram), 3 (Reddit), 4 (Github) and 5 (Website).")]
        public ApplicationContactType Type { get; set; }
    }
}