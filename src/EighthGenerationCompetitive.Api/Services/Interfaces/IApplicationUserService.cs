using EighthGenerationCompetitive.Api.Identity.Models;
using EighthGenerationCompetitive.Api.V1.ViewModels.Users;
using EighthGenerationCompetitive.Business.Interfaces;
using EighthGenerationCompetitive.Business.Monads;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Api.Services.Interfaces
{
    public interface IApplicationUserService : IDisposable
    {
        Task<Result<ApplicationUser>> GetApplicationUserByNameAsync(string userName);
        Task<Result<ApplicationContact>> GetUserApplicationContactAsync(string userName, Guid contactId);
        Task<Result<IEnumerable<ApplicationContact>>> GetUserApplicationContactsAsync(string userName);
        Task<Result> UpdateUser3dsFriendCodeAsync(IUser loggedUser, string nintendo3dsFriendCode);
        Task<Result> UpdateUserSwitchFriendCodeAsync(IUser loggedUser, string nintendoSwitchFriendCode);
        Task<Result> UpdateUserContactAsync(IUser loggedUser, Guid contactId, string contactDescription, ApplicationContactType contactType);
        Task<Result> ShowUserProfileAsync(IUser loggedUser);
        Task<Result> HideUserProfileAsync(IUser loggedUser);
        Task<Result> UpdateUserShowdownNicknameAsync(IUser loggedUser, string showdownNickname);
        Task<Result> UpdateUserMainInfoAsync(IUser loggedUser, UpdateUserMainInfoViewModel updateUserMainInfo);
        Task<Result> UpdateUserContactsAsync(IUser loggedUser, List<UpdateUserContactViewModel> contactsToBeUpdated);
        Task<Result> RemoveUserContactAsync(IUser loggedUser, Guid contactId, ApplicationContactType contactTypeToBeRemoved);
        Task<Result<ApplicationContact>> RegisterUserContactAsync(IUser loggedUser, RegisterUserContactViewModel registerUserContact);
    }
}