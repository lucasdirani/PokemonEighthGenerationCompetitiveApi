using System;

namespace EighthGenerationCompetitive.Api.Identity.Interfaces
{
    public interface IUpdatableEntity
    {
        public DateTime? UpdatedAt { get; set; }
    }
}