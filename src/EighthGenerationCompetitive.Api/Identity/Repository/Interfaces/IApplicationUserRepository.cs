using EighthGenerationCompetitive.Api.Identity.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Api.Identity.Repository.Interfaces
{
    public interface IApplicationUserRepository : IDisposable
    {
        Task<ApplicationUser> GetApplicationUserByNameAsync(string userName);
        Task<ApplicationUser> GetApplicationUserWithContactsByNameAsync(string userName);
        Task<DateTime?> GetUserLastUpdateDateAsync(string userName);
        Task<DateTime?> GetUserContactLastUpdateDateAsync(string userName);
        Task<ApplicationContact> GetUserContactByTypeAsync(Guid userId, ApplicationContactType contactType);
        Task<ApplicationContact> GetAuthorizedUserContactAsync(string userName, Guid contactId);
        Task<IEnumerable<ApplicationContact>> GetAuthorizedUserContactsAsync(string userName);
        Task<IEnumerable<ApplicationContact>> GetUserContactsByTypesAsync(Guid userId, IEnumerable<ApplicationContactType> contactsTypes);
        Task<bool> CheckIfUserHasAContactTypeAsync(string userName, ApplicationContactType contactType);
        Task<bool> CheckIfUserHasAllContactTypesAsync(string userName, IEnumerable<ApplicationContactType> contactsTypes);
        Task UpdateUserNintendo3dsFriendCodeAsync(Guid userId, string nintendo3dsFriendCode);
        Task UpdateUserNintendoSwitchFriendCodeAsync(Guid userId, string nintendoSwitchFriendCode);
        Task UpdateUserContactAsync(Guid contactId, string contactDescription);
        Task UpdateUserContactsAsync(IEnumerable<ApplicationContact> contacts);
        Task UpdateUserShowProfileAsync(Guid userId, bool showProfile);
        Task UpdateUserShowdownNicknameAsync(Guid userId, string showdownNickname);
        Task UpdateUserMainInfoAsync(Guid userId, string showdownNickname, string nintendo3dsFriendCode, string nintendoSwitchFriendCode);
        Task DeleteUserContactAsync(Guid contactId);
        Task<bool> RegisterUserContactAsync(Guid userId, string contactDescription, ApplicationContactType contactType);
    }
}