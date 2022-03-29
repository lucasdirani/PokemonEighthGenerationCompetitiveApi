using EighthGenerationCompetitive.Api.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EighthGenerationCompetitive.Api.Identity.Models
{
    public class ApplicationUser : IdentityUser<Guid>, ITraceableEntity
    {
        public ApplicationUser()
        {
            EmailConfirmed = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public string Nintendo3dsFriendCode { get; set; }

        public string NintendoSwitchFriendCode { get; set; }

        public string ShowdownNickname { get; set; }

        public bool ShowProfile { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public ICollection<ApplicationContact> ApplicationContacts { get; set; }

        [JsonIgnore]
        public override int AccessFailedCount { get; set; }

        [JsonIgnore]
        public override string ConcurrencyStamp { get; set; }

        [JsonIgnore]
        public override string Email { get; set; }

        [JsonIgnore]
        public override bool EmailConfirmed { get; set; }

        [JsonIgnore]
        public override bool LockoutEnabled { get; set; }

        [JsonIgnore]
        public override DateTimeOffset? LockoutEnd { get; set; }

        [JsonIgnore]
        public override string NormalizedEmail { get; set; }

        [JsonIgnore]
        public override string NormalizedUserName { get; set; }

        [JsonIgnore]
        public override string PasswordHash { get; set; }

        [JsonIgnore]
        public override string PhoneNumber { get; set; }

        [JsonIgnore]
        public override bool PhoneNumberConfirmed { get; set; }

        [JsonIgnore]
        public override string SecurityStamp { get; set; }

        [JsonIgnore]
        public override bool TwoFactorEnabled { get; set; }

        [NotMapped]
        public bool DoesNotAuthorizeDisplayingTheProfile => !ShowProfile;
    }
}