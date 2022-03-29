using EighthGenerationCompetitive.Api.Authentication.Claims;
using EighthGenerationCompetitive.Api.Extensions;
using EighthGenerationCompetitive.Api.Identity.Models;
using EighthGenerationCompetitive.Api.V1.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Api.Authentication
{
    public class JwtGenerator
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public JwtGenerator(
            UserManager<ApplicationUser> userManager, 
            IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<UserLoginViewModel> GenerateJwtAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            var identityClaims = await GenerateIdentityClaimsAsync(user);

            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var tokenHandler = new JwtSecurityTokenHandler();

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.ValidIn,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpiresInHours),
                SigningCredentials = signingCredentials,
            });

            return new UserLoginViewModel()
            {
                AccessToken = tokenHandler.WriteToken(token),
                ExpiresIn = TimeSpan.FromHours(_jwtSettings.ExpiresInHours).TotalSeconds,
                UserToken = new UserTokenViewModel()
                {
                    Id = user.Id.ToString(),
                    Email = user.Email,
                    Nintendo3dsFriendCode = user.Nintendo3dsFriendCode,
                    NintendoSwitchFriendCode = user.NintendoSwitchFriendCode,
                    ShowdownNickname = user.ShowdownNickname,
                    UserName = user.UserName,
                    Claims = identityClaims.Claims.Select(c => new UserClaimViewModel { Type = c.Type, Value = c.Value })
                }
            };
        }

        private async Task<ClaimsIdentity> GenerateIdentityClaimsAsync(ApplicationUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);

            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.UtcNow.ToUnixEpochDate().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUnixEpochDate().ToString(), ClaimValueTypes.Integer64));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(type: "role", userRole));
            }

            claims.Add(new Claim(CustomClaimType.ShowdownNickname, user.ShowdownNickname));
            claims.Add(new Claim(CustomClaimType.NintendoSwitchFriendCode, user.NintendoSwitchFriendCode));
            claims.Add(new Claim(CustomClaimType.Nintendo3dsFriendCode, user.Nintendo3dsFriendCode));

            var identityClaims = new ClaimsIdentity();

            identityClaims.AddClaims(claims);

            return identityClaims;
        }
    }
}