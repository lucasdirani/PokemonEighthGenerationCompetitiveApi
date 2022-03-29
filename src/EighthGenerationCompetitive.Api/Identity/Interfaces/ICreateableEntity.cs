using System;

namespace EighthGenerationCompetitive.Api.Identity.Interfaces
{
    public interface ICreateableEntity
    {
        public DateTime? CreatedAt { get; set; }
    }
}