using EighthGenerationCompetitive.Api.Identity.Context;
using EighthGenerationCompetitive.Api.Identity.Models;
using EighthGenerationCompetitive.Api.Identity.Repository.Interfaces;
using EighthGenerationCompetitive.Business.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Api.Identity.Repository
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        private bool _disposed = false;

        public ApplicationUserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ApplicationUser> GetApplicationUserByNameAsync(string userName) =>
            await _applicationDbContext
                    .ApplicationUsers
                    .Where(u => u.UserName == userName)
                    .FirstOrDefaultAsync();

        public async Task<ApplicationUser> GetApplicationUserWithContactsByNameAsync(string userName) =>
            await _applicationDbContext
                    .ApplicationUsers
                    .Where(u => u.UserName == userName)
                    .Include(u => u.ApplicationContacts.Where(c => c.Active))
                    .FirstOrDefaultAsync();

        public async Task<DateTime?> GetUserLastUpdateDateAsync(string userName) =>
            await _applicationDbContext
                    .ApplicationUsers
                    .Where(u => u.UserName == userName)
                    .Select(u => u.UpdatedAt)
                    .FirstOrDefaultAsync();

        public async Task<DateTime?> GetUserContactLastUpdateDateAsync(string userName) =>
            await _applicationDbContext
                    .ApplicationUsers
                    .Where(u => u.UserName == userName)
                    .Select(u => u.ApplicationContacts.Max(c => c.UpdatedAt))
                    .FirstOrDefaultAsync();

        public async Task<ApplicationContact> GetUserContactByTypeAsync(
            Guid userId,
            ApplicationContactType contactType) =>
            await _applicationDbContext
                    .ApplicationUsers
                    .Where(u => u.Id == userId)
                    .SelectMany(u => u.ApplicationContacts)
                    .Where(c => c.Type == contactType && c.Active)
                    .Include(c => c.ApplicationUser)
                    .FirstOrDefaultAsync();

        public async Task<ApplicationContact> GetAuthorizedUserContactAsync(string userName, Guid contactId) =>
            await _applicationDbContext
                    .ApplicationUsers
                    .Where(u => string.Equals(u.UserName, userName) && u.ShowProfile)
                    .SelectMany(u => u.ApplicationContacts)
                    .Where(c => c.Active && c.Id == contactId)
                    .Include(c => c.ApplicationUser)
                    .FirstOrDefaultAsync();

        public async Task<IEnumerable<ApplicationContact>> GetAuthorizedUserContactsAsync(string userName) =>
            await _applicationDbContext
                    .ApplicationUsers
                    .Where(u => string.Equals(u.UserName, userName) && u.ShowProfile)
                    .SelectMany(u => u.ApplicationContacts)
                    .Where(c => c.Active)
                    .Include(c => c.ApplicationUser)
                    .ToListAsync();

        public async Task<IEnumerable<ApplicationContact>> GetUserContactsByTypesAsync(
            Guid userId, 
            IEnumerable<ApplicationContactType> contactsTypes) =>
            await _applicationDbContext
                    .ApplicationUsers
                    .Where(u => u.Id == userId)
                    .SelectMany(u => u.ApplicationContacts)
                    .Where(c => contactsTypes.Contains(c.Type) && c.Active)
                    .ToListAsync();

        public async Task<bool> CheckIfUserHasAContactTypeAsync(string userName, ApplicationContactType contactType) =>
            await _applicationDbContext
                    .ApplicationUsers
                    .AnyAsync(u => u.UserName == userName 
                        && u.ApplicationContacts.Any(c => c.Type == contactType && c.Active));

        public async Task<bool> CheckIfUserHasAllContactTypesAsync(
            string userName, 
            IEnumerable<ApplicationContactType> contactsTypes)
        {
            var userContactsTypesFromDatabase =  await _applicationDbContext
                                                         .ApplicationUsers
                                                         .Where(u => u.UserName == userName)
                                                         .SelectMany(u => u.ApplicationContacts)
                                                         .Where(c => c.Active)
                                                         .Select(c => c.Type)
                                                         .ToListAsync();

            return !contactsTypes.Except(userContactsTypesFromDatabase).Any();
        }

        public async Task UpdateUserNintendo3dsFriendCodeAsync(Guid userId, string nintendo3dsFriendCode)
        {
            var updateCommand = $"UPDATE AspNetUsers " +
                                $"SET Nintendo3dsFriendCode='{nintendo3dsFriendCode}', " +
                                $"UpdatedAt=GETDATE() " +
                                $"WHERE Id='{userId}'";

            await _applicationDbContext.Database.ExecuteSqlRawAsync(updateCommand);
        }

        public async Task UpdateUserNintendoSwitchFriendCodeAsync(Guid userId, string nintendoSwitchFriendCode)
        {
            var updateCommand = $"UPDATE AspNetUsers " +
                                $"SET NintendoSwitchFriendCode='{nintendoSwitchFriendCode}', " +
                                $"UpdatedAt=GETDATE() " +
                                $"WHERE Id='{userId}'";

            await _applicationDbContext.Database.ExecuteSqlRawAsync(updateCommand);
        }

        public async Task UpdateUserContactAsync(Guid contactId, string contactDescription)
        {
            var updateCommand = $"UPDATE ApplicationContact " +
                                $"SET Description='{contactDescription}', " +
                                $"UpdatedAt=GETDATE() " +
                                $"WHERE Id='{contactId}'";

            await _applicationDbContext.Database.ExecuteSqlRawAsync(updateCommand);
        }

        public async Task DeleteUserContactAsync(Guid contactId)
        {
            var updateCommand = $"UPDATE ApplicationContact " +
                                $"SET Active=0, " +
                                $"UpdatedAt=GETDATE() " +
                                $"WHERE Id='{contactId}'";

            await _applicationDbContext.Database.ExecuteSqlRawAsync(updateCommand);
        }

        public async Task UpdateUserContactsAsync(IEnumerable<ApplicationContact> contacts)
        {
            try
            {
                await _applicationDbContext.Database.BeginTransactionAsync();

                foreach (var contact in contacts)
                {
                    var updateCommand = $"UPDATE ApplicationContact " +
                                        $"SET Description='{contact.Description}', " +
                                        $"UpdatedAt=GETDATE() " +
                                        $"WHERE Id='{contact.Id}'";

                    await _applicationDbContext.Database.ExecuteSqlRawAsync(updateCommand);
                }

                await _applicationDbContext.Database.CommitTransactionAsync();
            }
            catch (DbUpdateException)
            {
                await _applicationDbContext.Database.RollbackTransactionAsync();

                throw;
            }
        }

        public async Task UpdateUserShowProfileAsync(Guid userId, bool showProfile)
        {
            var updateCommand = $"UPDATE AspNetUsers " +
                                $"SET ShowProfile='{showProfile}', " +
                                $"UpdatedAt=GETDATE() " +
                                $"WHERE Id='{userId}'";

            await _applicationDbContext.Database.ExecuteSqlRawAsync(updateCommand);
        }

        public async Task UpdateUserShowdownNicknameAsync(Guid userId, string showdownNickname)
        {
            var updateCommand = $"UPDATE AspNetUsers " +
                                $"SET ShowdownNickname='{showdownNickname}', " +
                                $"UpdatedAt=GETDATE() " +
                                $"WHERE Id='{userId}'";

            await _applicationDbContext.Database.ExecuteSqlRawAsync(updateCommand);
        }

        public async Task UpdateUserMainInfoAsync(
            Guid userId, 
            string showdownNickname, 
            string nintendo3dsFriendCode, 
            string nintendoSwitchFriendCode)
        {
            var updateCommand = $"UPDATE AspNetUsers " +
                                $"SET ShowdownNickname='{showdownNickname}', " +
                                $"NintendoSwitchFriendCode='{nintendoSwitchFriendCode}', " +
                                $"Nintendo3dsFriendCode='{nintendo3dsFriendCode}', " + 
                                $"UpdatedAt=GETDATE() " +
                                $"WHERE Id='{userId}'";

            await _applicationDbContext.Database.ExecuteSqlRawAsync(updateCommand);
        }

        public async Task<bool> RegisterUserContactAsync(
            Guid userId, 
            string contactDescription, 
            ApplicationContactType contactType)
        {
            try
            {
                await _applicationDbContext.Database.BeginTransactionAsync();

                var insertCommand = $"INSERT INTO ApplicationContact(Id, Description, Type, CreatedAt, UpdatedAt, ApplicationUserId) " +
                                    $"VALUES(NEWID(), '{contactDescription}', {contactType.GetIdentification()}, GETDATE(), GETDATE(), '{userId}') ";

                await _applicationDbContext.Database.ExecuteSqlRawAsync(insertCommand);

                await _applicationDbContext.Database.CommitTransactionAsync();

                return true;
            }
            catch (DbUpdateException)
            {
                await _applicationDbContext.Database.RollbackTransactionAsync();

                return false;
            }
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
                _applicationDbContext?.Dispose();
                _disposed = true;
            }
        }
    }
}