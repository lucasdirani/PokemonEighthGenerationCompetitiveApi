using EighthGenerationCompetitive.Api.Identity.Interfaces;
using System;
using System.Text.Json.Serialization;

namespace EighthGenerationCompetitive.Api.Identity.Models
{
    public class ApplicationContact : ITraceableEntity
    {
        public ApplicationContact()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Active = true;
        }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public ApplicationContactType Type { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Active { get; set; }

        [JsonIgnore]
        public ApplicationUser ApplicationUser { get; set; }
        public Guid ApplicationUserId { get; set; }
    }
}