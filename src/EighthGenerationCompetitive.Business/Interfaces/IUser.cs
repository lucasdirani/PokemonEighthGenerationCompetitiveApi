using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace EighthGenerationCompetitive.Business.Interfaces
{
    public interface IUser
    {
        string Name { get; }
        Guid GetUserId();
        string GetUserEmail();
        bool IsAuthenticated();
        bool IsNotAuthenticatedUser(string userName);
        bool IsInRole(string role);
        IEnumerable<Claim> GetClaimsIdentity();
        Claim GetClaimByType(string type);
    }
}