using System.Collections.Generic;

namespace EighthGenerationCompetitive.Api.V1.ViewModels.Users
{
    public class UserTokenViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Nintendo3dsFriendCode { get; set; }
        public string NintendoSwitchFriendCode { get; set; }
        public string ShowdownNickname { get; set; }
        public IEnumerable<UserClaimViewModel> Claims { get; set; }
    }
}