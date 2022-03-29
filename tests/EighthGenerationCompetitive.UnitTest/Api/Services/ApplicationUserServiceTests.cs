using EighthGenerationCompetitive.Api.Identity.Models;
using EighthGenerationCompetitive.Api.Identity.Repository.Interfaces;
using EighthGenerationCompetitive.Api.Services;
using EighthGenerationCompetitive.Business.Interfaces;
using EighthGenerationCompetitive.Business.Notifications;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EighthGenerationCompetitive.UnitTest.Api.Services
{
    public class ApplicationUserServiceTests
    {
        private ApplicationUserService _applicationUserService;
        private Notifier _notifier; 
        private string _userName;
        private bool _userShowProfile;
        private IApplicationUserRepository _applicationUserRepository;


        private ApplicationUserService SetupSystemUnderTest(string userName, bool userShowProfile)
        {
            _notifier = new Notifier();

            _applicationUserRepository = ApplicationUserServiceMocks.SetupApplicationUserRepository(userName, userShowProfile);

            return new ApplicationUserService(_applicationUserRepository, _notifier);
        }

        private ApplicationUserService SetupSystemUnderTest(
            IUser user,
            ApplicationContactType searchedContactType,
            List<ApplicationContact> applicationContacts)
        {
            _notifier = new Notifier();

            _applicationUserRepository = ApplicationUserServiceMocks.SetupApplicationUserRepository(user, searchedContactType, applicationContacts);

            return new ApplicationUserService(_applicationUserRepository, _notifier);
        }

        private static List<ApplicationContact> SetupApplicationContacts(
            params ApplicationContactType[] applicationContactTypes)
        {
            return applicationContactTypes.Select(c => new ApplicationContact()
            {
                Id = Guid.NewGuid(), Description = It.IsAny<string>(), Type = c
            }).ToList();
        }

        [Fact]
        public async Task GetApplicationUserByNameAsync_RegisteredUser_ReturnsSuccess()
        {
            _userName = "registeredUser";
            _userShowProfile = true;
            _applicationUserService = SetupSystemUnderTest(_userName, _userShowProfile);

            var userSearch = await _applicationUserService.GetApplicationUserByNameAsync(_userName);

            Assert.True(userSearch.IsSuccess);
            Assert.Empty(_notifier.GetNotifications());
            Assert.NotNull(userSearch.Value);
        }

        [Fact]
        public async Task GetApplicationUserByNameAsync_UnregisteredUser_ReturnsFailure()
        {
            _userName = "registeredUser";
            _userShowProfile = true;
            _applicationUserService = SetupSystemUnderTest(_userName, _userShowProfile);

            string unregisteredUserName = "unregisteredUser";
            var userSearch = await _applicationUserService.GetApplicationUserByNameAsync(unregisteredUserName);

            Assert.True(userSearch.Failure);
            Assert.NotEmpty(_notifier.GetNotifications());
            Assert.Throws<InvalidOperationException>(() => userSearch.Value);
        }

        [Fact]
        public async Task GetApplicationUserByNameAsync_RegisteredUserWhoDoesNotShareHisProfile_ReturnsFailure()
        {
            _userName = "registeredUser";
            _userShowProfile = false;
            _applicationUserService = SetupSystemUnderTest(_userName, _userShowProfile);

            var userSearch = await _applicationUserService.GetApplicationUserByNameAsync(_userName);

            Assert.True(userSearch.Failure);
            Assert.NotEmpty(_notifier.GetNotifications());
            Assert.Throws<InvalidOperationException>(() => userSearch.Value);
        }

        [Fact]
        public async Task UpdateUserContactAsync_UserWithRegisteredContactType_ReturnsSuccess()
        {
            IUser user = ApplicationUserServiceMocks.SetupUser(userName: "RegisteredUser", userId: Guid.NewGuid());
            ApplicationContactType redditContact = ApplicationContactType.Reddit;
            List<ApplicationContact> contacts = SetupApplicationContacts(redditContact);

            _applicationUserService = SetupSystemUnderTest(user, searchedContactType: redditContact, contacts);

            string newContactDescription = "New Description";
            Guid contactId = Guid.NewGuid();

            var contactUpdate = await _applicationUserService.UpdateUserContactAsync(user, contactId, newContactDescription, redditContact);

            Assert.True(contactUpdate.IsSuccess);
            Assert.Empty(_notifier.GetNotifications());
        }

        [Fact]
        public async Task UpdateUserContactAsync_UserWithoutRegisteredContactType_ReturnsFailure()
        {
            IUser user = ApplicationUserServiceMocks.SetupUser(userName: "RegisteredUser", userId: Guid.NewGuid());
            ApplicationContactType githubContact = ApplicationContactType.Github;
            ApplicationContactType twitterContact = ApplicationContactType.Twitter;
            List<ApplicationContact> contacts = SetupApplicationContacts(githubContact);

            _applicationUserService = SetupSystemUnderTest(user, searchedContactType: twitterContact, contacts);

            string newContactDescription = "New Description";
            Guid contactId = Guid.NewGuid();

            var contactUpdate = await _applicationUserService.UpdateUserContactAsync(user, contactId, newContactDescription, twitterContact);

            Assert.True(contactUpdate.Failure);
            Assert.NotEmpty(_notifier.GetNotifications());
        }

        private static class ApplicationUserServiceMocks
        {
            private static Mock<IApplicationUserRepository> _applicationUserRepository;
            private static Mock<IUser> _user;

            public static IApplicationUserRepository SetupApplicationUserRepository(
                string userName,
                bool showProfile)
            {
                _applicationUserRepository = new Mock<IApplicationUserRepository>();

                _applicationUserRepository.Setup(a => a
                    .GetApplicationUserWithContactsByNameAsync(userName))
                    .ReturnsAsync(() => new ApplicationUser()
                    {
                        Id = Guid.NewGuid(),
                        UserName = userName,
                        ShowdownNickname = userName,
                        ShowProfile = showProfile
                    });

                return _applicationUserRepository.Object;
            }

            public static IApplicationUserRepository SetupApplicationUserRepository(
                IUser user,
                ApplicationContactType searchedContactType,
                List<ApplicationContact> applicationContacts)
            {
                _applicationUserRepository = new Mock<IApplicationUserRepository>();

                _applicationUserRepository.Setup(a => a
                    .CheckIfUserHasAContactTypeAsync(user.Name, It.IsAny<ApplicationContactType>()))
                    .ReturnsAsync(() => applicationContacts.Any(a => a.Type == searchedContactType));

                _applicationUserRepository.Setup(a => a
                    .GetUserContactByTypeAsync(user.GetUserId(), searchedContactType))
                    .ReturnsAsync(() => applicationContacts.FirstOrDefault(a => a.Type == searchedContactType));

                _applicationUserRepository.Setup(a => a.UpdateUserContactAsync(It.IsAny<Guid>(), It.IsAny<string>()));

                return _applicationUserRepository.Object;
            }

            public static IUser SetupUser(string userName, Guid userId)
            {
                _user = new Mock<IUser>();

                _user.Setup(u => u.Name).Returns(userName);

                _user.Setup(u => u.GetUserId()).Returns(userId);

                return _user.Object;
            }
        }
    }
}