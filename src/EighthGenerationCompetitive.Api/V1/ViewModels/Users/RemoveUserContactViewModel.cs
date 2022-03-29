using EighthGenerationCompetitive.Api.Identity.Models;
using System.ComponentModel.DataAnnotations;

namespace EighthGenerationCompetitive.Api.V1.ViewModels.Users
{
    /// <summary>
    /// Represents the data required for a user to remove one of their contacts
    /// </summary>
    public class RemoveUserContactViewModel
    {
        /// <summary>
        /// The user's contact type to be removed. Must have one of the following values: 0 (Facebook), 1 (Twitter), 2 (Instagram), 3 (Reddit), 4 (Github) and 5 (Website).
        /// </summary>
        [EnumDataType(typeof(ApplicationContactType), ErrorMessage = "The field must have one of the following values: 0 (Facebook), 1 (Twitter), 2 (Instagram), 3 (Reddit), 4 (Github) and 5 (Website).")]
        public ApplicationContactType Type { get; set; }
    }
}