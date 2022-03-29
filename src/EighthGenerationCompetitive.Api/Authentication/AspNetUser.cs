using EighthGenerationCompetitive.Api.Extensions;
using EighthGenerationCompetitive.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace EighthGenerationCompetitive.Api.Authentication
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AspNetUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Name => _httpContextAccessor.HttpContext.User.GetUserName();

        public Claim GetClaimByType(string type)
        {
            return GetClaimsIdentity().Where(c => c.Type == type).FirstOrDefault();
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _httpContextAccessor.HttpContext.User.Claims;
        }

        public string GetUserEmail()
        {
            return IsAuthenticated() ? _httpContextAccessor.HttpContext.User.GetUserEmail() : string.Empty;
        }

        public Guid GetUserId()
        {
            return IsAuthenticated() ? Guid.Parse(_httpContextAccessor.HttpContext.User.GetUserId()) : Guid.Empty;
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public bool IsInRole(string role)
        {
            return _httpContextAccessor.HttpContext.User.IsInRole(role);
        }

        public bool IsNotAuthenticatedUser(string userName)
        {
            return !string.Equals(Name, userName, StringComparison.OrdinalIgnoreCase);
        }
    }
}