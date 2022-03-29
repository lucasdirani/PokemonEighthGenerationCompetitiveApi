using AutoMapper;
using EighthGenerationCompetitive.Api.Identity.Models;
using EighthGenerationCompetitive.Api.V1.ViewModels.Users;

namespace EighthGenerationCompetitive.Api.Profiles.V1
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMapForViewModelsOfUsersController();
        }

        private void CreateMapForViewModelsOfUsersController()
        {
            CreateMap<RegisterUserContactViewModel, ApplicationContact>();
            CreateMap<RegisterUserViewModel, ApplicationUser>();
            CreateMap<ApplicationUser, UserOwnerContact>();
            CreateMap<ApplicationContact, GetUserContactViewModel>();
            CreateMap<ApplicationContact, GetUserContactsViewModel>();
            CreateMap<ApplicationUser, GetUserViewModel>();
        }
    }
}