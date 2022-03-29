using EighthGenerationCompetitive.Api.Identity.Models;
using RiskFirst.Hateoas.Models;
using System;

namespace EighthGenerationCompetitive.Api.V1.ViewModels.Users
{
    /// <summary>
    /// Represents an user contact
    /// </summary>
    public class GetUserContactViewModel : LinkContainer
    {
        /// <summary>
        /// The user contact unique identification.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The description of the user's contact (link, username, etc.).
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The user's contact type. Can have one of the following values: 0 (Facebook), 1 (Twitter), 2 (Instagram), 3 (Reddit), 4 (Github) and 5 (Website).
        /// </summary>
        public ApplicationContactType Type { get; set; }

        /// <summary>
        /// The user who owns the contact
        /// </summary>
        public UserOwnerContact ApplicationUser { get; set; }
    }
}