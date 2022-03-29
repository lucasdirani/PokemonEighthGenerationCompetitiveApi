using EighthGenerationCompetitive.Api.Identity.Models;
using EighthGenerationCompetitive.Api.Identity.Repository.Interfaces;
using EighthGenerationCompetitive.Api.Services.Interfaces;
using EighthGenerationCompetitive.Api.V1.ViewModels.Users;
using EighthGenerationCompetitive.Business.Errors;
using EighthGenerationCompetitive.Business.Errors.Factory;
using EighthGenerationCompetitive.Business.Interfaces;
using EighthGenerationCompetitive.Business.Monads;
using EighthGenerationCompetitive.Business.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Api.Services
{
    public class ApplicationUserService : BaseService, IApplicationUserService
    {
        private readonly IApplicationUserRepository _applicationUserRepository;

        private bool _disposed = false;

        public ApplicationUserService(
            IApplicationUserRepository applicationUserRepository,
            INotifier notifier) 
            : base(notifier)
        {
            _applicationUserRepository = applicationUserRepository;
        }

        public async Task<Result<ApplicationUser>> GetApplicationUserByNameAsync(string userName)
        {
            var userSearch = await GetApplicationUserWithContactsByNameAsync(userName);

            if (userSearch.Failure)
            {
                return Result.Fail<ApplicationUser>(userSearch.Error);
            }

            ApplicationUser user = userSearch.Value;

            if (user.DoesNotAuthorizeDisplayingTheProfile)
            {
                var userUnsharedProfileError = AppErrorFactory.Make(AppErrorType.UserUnsharedProfile);

                Notify(userUnsharedProfileError);

                return Result.Fail<ApplicationUser>(userUnsharedProfileError.ErrorMessage);
            }

            return Result.Success(user);
        }

        public async Task<Result<ApplicationContact>> GetUserApplicationContactAsync(string userName, Guid contactId)
        {
            var userContact = await _applicationUserRepository.GetAuthorizedUserContactAsync(userName, contactId);

            if (userContact is null)
            {
                var userContactNotFoundError = AppErrorFactory.Make(AppErrorType.UserContactNotFound);

                Notify(userContactNotFoundError);

                return Result.Fail<ApplicationContact>(userContactNotFoundError.ErrorMessage);
            }

            return Result.Success(userContact);
        }

        public async Task<Result<IEnumerable<ApplicationContact>>> GetUserApplicationContactsAsync(string userName)
        {
            var userContacts = await _applicationUserRepository.GetAuthorizedUserContactsAsync(userName);

            if (userContacts is null || !userContacts.Any())
            {
                var userContactsNotFoundError = AppErrorFactory.Make(AppErrorType.UserContactsNotFound);

                Notify(userContactsNotFoundError);

                return Result.Fail<IEnumerable<ApplicationContact>>(userContactsNotFoundError.ErrorMessage);
            }

            return Result.Success(userContacts);
        }

        public async Task<Result> UpdateUser3dsFriendCodeAsync(IUser loggedUser, string nintendo3dsFriendCode)
        {
            try
            {
                Guid userId = loggedUser.GetUserId();

                await _applicationUserRepository.UpdateUserNintendo3dsFriendCodeAsync(userId, nintendo3dsFriendCode);

                return Result.Success();
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result> UpdateUserSwitchFriendCodeAsync(IUser loggedUser, string nintendoSwitchFriendCode)
        {
            try
            {
                Guid userId = loggedUser.GetUserId();

                await _applicationUserRepository.UpdateUserNintendoSwitchFriendCodeAsync(userId, nintendoSwitchFriendCode);

                return Result.Success();
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result> UpdateUserContactAsync(
            IUser loggedUser, 
            Guid contactId,
            string contactDescription, 
            ApplicationContactType contactType)
        {
            try
            {
                bool userHasContact = await _applicationUserRepository.CheckIfUserHasAContactTypeAsync(loggedUser.Name, contactType);

                if (!userHasContact)
                {
                    var userNonExistingContactError = AppErrorFactory.Make(AppErrorType.UserNonExistingContact);

                    Notify(userNonExistingContactError);

                    return Result.Fail(userNonExistingContactError.ErrorMessage);
                }

                Guid userId = loggedUser.GetUserId();

                await _applicationUserRepository.UpdateUserContactAsync(contactId, contactDescription);

                return Result.Success();
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result> UpdateUserContactsAsync(
            IUser loggedUser, 
            List<UpdateUserContactViewModel> contactsToBeUpdated)
        {
            try
            {
                IEnumerable<ApplicationContactType> contactsTypes = contactsToBeUpdated.Select(c => c.Type).Distinct();

                bool userHasContactsTypes = await _applicationUserRepository.CheckIfUserHasAllContactTypesAsync(loggedUser.Name, contactsTypes);

                if (!userHasContactsTypes)
                {
                    var userNonExistingContactError = AppErrorFactory.Make(AppErrorType.UserNonExistingContact);

                    Notify(userNonExistingContactError);

                    return Result.Fail(userNonExistingContactError.ErrorMessage);
                }

                var contacts = await _applicationUserRepository.GetUserContactsByTypesAsync(loggedUser.GetUserId(), contactsTypes);

                foreach (var contact in contacts)
                {
                    contact.Description = contactsToBeUpdated.LastOrDefault(c => c.Type == contact.Type).Description;
                }

                await _applicationUserRepository.UpdateUserContactsAsync(contacts);

                return Result.Success();
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result> UpdateUserShowdownNicknameAsync(IUser loggedUser, string showdownNickname)
        {
            try
            {
                Guid userId = loggedUser.GetUserId();

                await _applicationUserRepository.UpdateUserShowdownNicknameAsync(userId, showdownNickname);

                return Result.Success();
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result> UpdateUserMainInfoAsync(
            IUser loggedUser, 
            UpdateUserMainInfoViewModel updateUserMainInfo)
        {
            try
            {
                Guid userId = loggedUser.GetUserId();

                string showdownNickname = updateUserMainInfo.ShowdownNickname;

                string nintendo3dsFriendCode = updateUserMainInfo.Nintendo3dsFriendCode;

                string nintendoSwitchFriendCode = updateUserMainInfo.NintendoSwitchFriendCode;

                await _applicationUserRepository.UpdateUserMainInfoAsync(
                    userId, 
                    showdownNickname, 
                    nintendo3dsFriendCode, 
                    nintendoSwitchFriendCode);

                return Result.Success();
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result> ShowUserProfileAsync(IUser loggedUser)
        {
            try
            {
                Guid userId = loggedUser.GetUserId();

                await _applicationUserRepository.UpdateUserShowProfileAsync(userId, showProfile: true);

                return Result.Success();
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result> HideUserProfileAsync(IUser loggedUser)
        {
            try
            {
                Guid userId = loggedUser.GetUserId();

                await _applicationUserRepository.UpdateUserShowProfileAsync(userId, showProfile: false);

                return Result.Success();
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<ApplicationContact>> RegisterUserContactAsync(
            IUser loggedUser,
            RegisterUserContactViewModel registerUserContact)
        {
            bool userHasContact = await _applicationUserRepository.CheckIfUserHasAContactTypeAsync(loggedUser.Name, registerUserContact.Type);

            if (userHasContact)
            {
                var userExistingContactError = AppErrorFactory.Make(AppErrorType.UserExistingContact);

                Notify(userExistingContactError);

                return Result.Fail<ApplicationContact>(userExistingContactError.ErrorMessage);
            }

            Guid userId = loggedUser.GetUserId();

            string contactDescription = registerUserContact.Description;

            ApplicationContactType contactType = registerUserContact.Type;

            bool contactRegistered = await _applicationUserRepository.RegisterUserContactAsync(userId, contactDescription, contactType);

            if (!contactRegistered)
            {
                var userRegisterContactError = AppErrorFactory.Make(AppErrorType.UserRegisterContact);

                Notify(userRegisterContactError);

                return Result.Fail<ApplicationContact>(userRegisterContactError.ErrorMessage);
            }

            return Result.Success(await _applicationUserRepository.GetUserContactByTypeAsync(userId, contactType));
        }

        public async Task<Result> RemoveUserContactAsync(
            IUser loggedUser, 
            Guid contactId, 
            ApplicationContactType contactTypeToBeRemoved)
        {
            try
            {
                bool userHasContact = await _applicationUserRepository.CheckIfUserHasAContactTypeAsync(loggedUser.Name, contactTypeToBeRemoved);

                if (!userHasContact)
                {
                    var userNonExistingContactError = AppErrorFactory.Make(AppErrorType.UserNonExistingContact);

                    Notify(userNonExistingContactError);

                    return Result.Fail(userNonExistingContactError.ErrorMessage);
                }

                Guid userId = loggedUser.GetUserId();

                await _applicationUserRepository.DeleteUserContactAsync(contactId);

                return Result.Success();
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        private async Task<Result<ApplicationUser>> GetApplicationUserWithContactsByNameAsync(string userName)
        {
            var user = await _applicationUserRepository.GetApplicationUserWithContactsByNameAsync(userName);

            if (user is null)
            {
                var userNotFoundError = AppErrorFactory.Make(AppErrorType.UserNotFound);

                Notify(userNotFoundError);

                return Result.Fail<ApplicationUser>(userNotFoundError.ErrorMessage);
            }

            return Result.Success(user);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _applicationUserRepository?.Dispose();
                _disposed = true;
            }
        }
    }
}