using RiskFirst.Hateoas.Models;

namespace EighthGenerationCompetitive.Api.V1.ViewModels.Users
{
    public class UserLoginViewModel : LinkContainer
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenViewModel UserToken { get; set; }
    }
}