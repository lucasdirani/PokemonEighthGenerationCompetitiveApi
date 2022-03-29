using EighthGenerationCompetitive.Api.Cache.Keys;
using EighthGenerationCompetitive.Api.Identity.Models;
using EighthGenerationCompetitive.Api.Identity.Repository.Interfaces;
using EighthGenerationCompetitive.Business.Monads;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Api.Cache.Decorators
{
    public class ApplicationUserCachingDecorator<T> : BaseCaching<ApplicationUser>, IApplicationUserRepository
        where T : IApplicationUserRepository
    {
        private readonly T _applicationUserRepository;

        private bool _disposed = false;

        public ApplicationUserCachingDecorator(T applicationUserRepository, IDistributedCache cache)
            : base(cache)
        {
            _applicationUserRepository = applicationUserRepository;
        }

        public async Task<bool> CheckIfUserHasAContactTypeAsync(string userName, ApplicationContactType contactType) =>
            await _applicationUserRepository.CheckIfUserHasAContactTypeAsync(userName, contactType);

        public async Task<bool> CheckIfUserHasAllContactTypesAsync(
            string userName,
            IEnumerable<ApplicationContactType> contactsTypes) =>
            await _applicationUserRepository.CheckIfUserHasAllContactTypesAsync(userName, contactsTypes);

        public async Task<ApplicationUser> GetApplicationUserByNameAsync(string userName)
        {
            string userCacheId = $"{ApplicationUserCacheKey.UserByName}: {userName}";

            Maybe<string> userJsonFromCache = await RetrieveResourceFromCacheAsync(userCacheId);

            if (userJsonFromCache.HasNoValue)
            {
                var userFromDatabase = await _applicationUserRepository.GetApplicationUserByNameAsync(userName);

                if (userFromDatabase is not null)
                {
                    await SetResourceOnCacheAsync(userCacheId, userFromDatabase, TimeSpan.FromDays(10));  
                }

                return userFromDatabase;
            }

            var userFromCache = JsonSerializer.Deserialize<ApplicationUser>(userJsonFromCache.Value);

            DateTime? lastUserUpdate = await _applicationUserRepository.GetUserLastUpdateDateAsync(userName);

            bool userFromCacheIsOutdated = lastUserUpdate > userFromCache.UpdatedAt;

            if (userFromCacheIsOutdated)
            {
                ApplicationUser updatedUser = await _applicationUserRepository.GetApplicationUserByNameAsync(userName);

                await SetResourceOnCacheAsync(userCacheId, updatedUser, TimeSpan.FromDays(10));

                return updatedUser;
            }

            return userFromCache;
        }

        public async Task<ApplicationUser> GetApplicationUserWithContactsByNameAsync(string userName)
        {
            string userCacheId = $"{ApplicationUserCacheKey.UserWithContactsByName}: {userName}";

            Maybe<string> userJsonFromCache = await RetrieveResourceFromCacheAsync(userCacheId);

            if (userJsonFromCache.HasNoValue)
            {
                var userFromDatabase = await _applicationUserRepository.GetApplicationUserWithContactsByNameAsync(userName);

                if (userFromDatabase is not null)
                {
                    await SetResourceOnCacheAsync(userCacheId, userFromDatabase, TimeSpan.FromDays(10));
                }

                return userFromDatabase;
            }

            var userFromCache = JsonSerializer.Deserialize<ApplicationUser>(userJsonFromCache.Value);

            DateTime? lastUserUpdate = await _applicationUserRepository.GetUserLastUpdateDateAsync(userName);

            DateTime? lastContactUpdateOnDatabase = await _applicationUserRepository.GetUserContactLastUpdateDateAsync(userName);

            DateTime? lastContactUpdateOnCache = userFromCache.ApplicationContacts.Max(a => a.UpdatedAt);

            bool userFromCacheIsOutdated = (lastUserUpdate > userFromCache.UpdatedAt) || (lastContactUpdateOnDatabase > lastContactUpdateOnCache);

            if (userFromCacheIsOutdated)
            {
                ApplicationUser updatedUser = await _applicationUserRepository.GetApplicationUserWithContactsByNameAsync(userName);

                await SetResourceOnCacheAsync(userCacheId, updatedUser, TimeSpan.FromDays(10));

                return updatedUser;
            }

            return userFromCache;
        }

        public async Task<ApplicationContact> GetUserContactByTypeAsync(
            Guid userId,
            ApplicationContactType contactType) =>
            await _applicationUserRepository.GetUserContactByTypeAsync(userId, contactType);

        public async Task<ApplicationContact> GetAuthorizedUserContactAsync(string userName, Guid contactId) =>
            await _applicationUserRepository.GetAuthorizedUserContactAsync(userName, contactId);

        public async Task<IEnumerable<ApplicationContact>> GetAuthorizedUserContactsAsync(string userName) =>
            await _applicationUserRepository.GetAuthorizedUserContactsAsync(userName);

        public async Task<IEnumerable<ApplicationContact>> GetUserContactsByTypesAsync(
            Guid userId,
            IEnumerable<ApplicationContactType> contactsTypes) =>
            await _applicationUserRepository.GetUserContactsByTypesAsync(userId, contactsTypes);

        public async Task<DateTime?> GetUserLastUpdateDateAsync(string userName) =>
            await _applicationUserRepository.GetUserLastUpdateDateAsync(userName);

        public async Task<DateTime?> GetUserContactLastUpdateDateAsync(string userName) =>
            await _applicationUserRepository.GetUserContactLastUpdateDateAsync(userName);

        public async Task UpdateUserNintendo3dsFriendCodeAsync(Guid userId, string nintendo3dsFriendCode) =>
            await _applicationUserRepository.UpdateUserNintendo3dsFriendCodeAsync(userId, nintendo3dsFriendCode);

        public async Task UpdateUserNintendoSwitchFriendCodeAsync(Guid userId, string nintendoSwitchFriendCode) =>
            await _applicationUserRepository.UpdateUserNintendoSwitchFriendCodeAsync(userId, nintendoSwitchFriendCode);

        public async Task UpdateUserContactAsync(Guid contactId, string contactDescription) =>
            await _applicationUserRepository.UpdateUserContactAsync(contactId, contactDescription);

        public async Task DeleteUserContactAsync(Guid contactId) =>
            await _applicationUserRepository.DeleteUserContactAsync(contactId);

        public async Task UpdateUserContactsAsync(IEnumerable<ApplicationContact> contacts) =>
            await _applicationUserRepository.UpdateUserContactsAsync(contacts);

        public async Task UpdateUserShowProfileAsync(Guid userId, bool showProfile) =>
            await _applicationUserRepository.UpdateUserShowProfileAsync(userId, showProfile);

        public async Task UpdateUserShowdownNicknameAsync(Guid userId, string showdownNickname) =>
            await _applicationUserRepository.UpdateUserShowdownNicknameAsync(userId, showdownNickname);

        public async Task UpdateUserMainInfoAsync(
            Guid userId,
            string showdownNickname,
            string nintendo3dsFriendCode,
            string nintendoSwitchFriendCode) =>
            await _applicationUserRepository.UpdateUserMainInfoAsync(
                userId, showdownNickname, nintendo3dsFriendCode, nintendoSwitchFriendCode);

        public async Task<bool> RegisterUserContactAsync(
            Guid userId,
            string contactDescription,
            ApplicationContactType contactType) =>
            await _applicationUserRepository.RegisterUserContactAsync(userId, contactDescription, contactType);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _applicationUserRepository.Dispose();
                _disposed = true;
            }
        }
    }
}