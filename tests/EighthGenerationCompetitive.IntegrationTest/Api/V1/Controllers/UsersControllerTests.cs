using EighthGenerationCompetitive.Api.Identity.Models;
using EighthGenerationCompetitive.Api.Response;
using EighthGenerationCompetitive.Api.V1.ViewModels.Users;
using EighthGenerationCompetitive.IntegrationTest.Priority;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace EighthGenerationCompetitive.IntegrationTest.Api.V1.Controllers
{
    [TestCaseOrderer("EighthGenerationCompetitive.IntegrationTest.Priority.PriorityOrderer", "EighthGenerationCompetitive.IntegrationTest")]
    public class UsersControllerTests : IClassFixture<BaseIntegrationTest>
    {
        private readonly BaseIntegrationTest _baseIntegrationTest;

        private string UserNameBeingTested => _baseIntegrationTest.TestedUser.UserNameBeingTested;
        private string UserPasswordBeingTested => _baseIntegrationTest.TestedUser.UserPasswordBeingTested;

        private static GetUserViewModel _registeredUser;
        private static GetUserContactViewModel _registeredUserContact;

        public UsersControllerTests(
            BaseIntegrationTest baseIntegrationTest)
        {
            _baseIntegrationTest = baseIntegrationTest;
        }

        [Fact, TestPriority(1)]
        public async Task RegisterUserAsync_ValidUserData_SuccessfulRequest()
        {
            string registerUserEndpoint = $"/api/v1/users/new-account";

            var userRegistrationData = new RegisterUserViewModel()
            {
                UserName = UserNameBeingTested,
                ShowProfile = true,
                Email = "pokemonTrainerLukas@gmail.com",
                ShowdownNickname = "pkmnTrainerLukas",
                Password = UserPasswordBeingTested,
                ConfirmPassword = UserPasswordBeingTested,
                Nintendo3dsFriendCode = "0834-8134-7774",
                NintendoSwitchFriendCode = "SW-6393-3432-8132",
                ApplicationContacts = new List<RegisterUserContactViewModel>()
                {
                    new RegisterUserContactViewModel()
                    {
                        Description = "pkmnTrainerLukas",
                        Type = ApplicationContactType.Twitter
                    }
                }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, registerUserEndpoint)
            {
                Content = JsonContent.Create(userRegistrationData)
            };

            using var response = await _baseIntegrationTest.SendRequestAsync(request);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact, TestPriority(2)]
        public async Task GetUserByNameAsync_ExistingUserWhoDisplaysYourProfile_SuccessfulRequest()
        {
            string getUserByNameEndpoint = $"/api/v1/users/{UserNameBeingTested}";

            var request = new HttpRequestMessage(HttpMethod.Get, getUserByNameEndpoint);

            string accessToken = await _baseIntegrationTest.AuthenticateUserRequest(UserNameBeingTested, UserPasswordBeingTested);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using var response = await _baseIntegrationTest.SendRequestAsync(request);

            var responseContent = await response.Content.ReadAsStringAsync();

            var serializeOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            _registeredUser = JsonSerializer.Deserialize<ApiResponse<GetUserViewModel>>(responseContent, serializeOptions).Data;

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(UserNameBeingTested, _registeredUser.UserName);
        }

        [Fact, TestPriority(3)]
        public async Task GetUserContactsAsync_ExistingUserContacts_SuccessfulRequest()
        {
            string getUserContactsEndpoint = $"/api/v1/users/{UserNameBeingTested}/contacts";

            var request = new HttpRequestMessage(HttpMethod.Get, getUserContactsEndpoint);

            string accessToken = await _baseIntegrationTest.AuthenticateUserRequest(UserNameBeingTested, UserPasswordBeingTested);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using var response = await _baseIntegrationTest.SendRequestAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(4)]
        public async Task UpdateUser3dsFriendCodeAsync_ValidNew3dsFriendCode_SuccessfulRequest()
        {
            string updateUser3dsFriendCodeEndpoint = $"/api/v1/users/{UserNameBeingTested}/update-3ds-friend-code";

            string new3dsFriendCode = "3368-8522-2722";

            var updateUser3dsFriendCode = new UpdateUser3DsFriendCodeViewModel() { Nintendo3dsFriendCode = new3dsFriendCode };

            var request = new HttpRequestMessage(HttpMethod.Patch, updateUser3dsFriendCodeEndpoint)
            {
                Content = JsonContent.Create(updateUser3dsFriendCode)
            };

            string accessToken = await _baseIntegrationTest.AuthenticateUserRequest(UserNameBeingTested, UserPasswordBeingTested);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using var response = await _baseIntegrationTest.SendRequestAsync(request);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact, TestPriority(5)]
        public async Task RegisterUserContactAsync_NewContactType_SuccessfulRequest()
        {
            string registerUserContactEndpoint = $"/api/v1/users/{UserNameBeingTested}/contacts/register-contact";

            var registerUserContact = new RegisterUserContactViewModel() 
            { 
                Description = "pokemonTrainerLukas", Type = ApplicationContactType.Reddit 
            };

            var request = new HttpRequestMessage(HttpMethod.Post, registerUserContactEndpoint)
            {
                Content = JsonContent.Create(registerUserContact)
            };

            string accessToken = await _baseIntegrationTest.AuthenticateUserRequest(UserNameBeingTested, UserPasswordBeingTested);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using var response = await _baseIntegrationTest.SendRequestAsync(request);

            var responseContent = await response.Content.ReadAsStringAsync();

            var serializeOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            _registeredUserContact = JsonSerializer.Deserialize<ApiResponse<GetUserContactViewModel>>(responseContent, serializeOptions).Data;

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(UserNameBeingTested, _registeredUserContact.ApplicationUser.UserName);
        }

        [Fact, TestPriority(6)]
        public async Task UpdateUserContactsAsync_RegisteredContact_SuccessfulRequest()
        {
            string updateUserContactsEndpoint = $"/api/v1/users/{UserNameBeingTested}/contacts/update-contacts";

            var updateUserContacts = new List<UpdateUserContactViewModel>()
            {
                new UpdateUserContactViewModel { Description = "New Description", Type = ApplicationContactType.Reddit } 
            };

            var request = new HttpRequestMessage(HttpMethod.Put, updateUserContactsEndpoint)
            {
                Content = JsonContent.Create(updateUserContacts)
            };

            string accessToken = await _baseIntegrationTest.AuthenticateUserRequest(UserNameBeingTested, UserPasswordBeingTested);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using var response = await _baseIntegrationTest.SendRequestAsync(request);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact, TestPriority(7)]
        public async Task RemoveUserContactAsync_RegisteredContact_SuccessfulRequest()
        {
            string removeUserContactEndpoint = $"/api/v1/users/{UserNameBeingTested}/contacts/{_registeredUserContact.Id}/remove-contact";

            var removeUserContact = new RemoveUserContactViewModel()
            {
                Type = _registeredUserContact.Type
            };

            var request = new HttpRequestMessage(HttpMethod.Delete, removeUserContactEndpoint)
            {
                Content = JsonContent.Create(removeUserContact)
            };

            string accessToken = await _baseIntegrationTest.AuthenticateUserRequest(UserNameBeingTested, UserPasswordBeingTested);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using var response = await _baseIntegrationTest.SendRequestAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(8)]
        public async Task UpdateHideUserProfileAsync_ExistingUserWhoDisplaysYourProfile_SuccessfulRequest()
        {
            string updateHideUserProfileEndpoint = $"/api/v1/users/{UserNameBeingTested}/hide-profile";

            var request = new HttpRequestMessage(HttpMethod.Patch, updateHideUserProfileEndpoint);

            string accessToken = await _baseIntegrationTest.AuthenticateUserRequest(UserNameBeingTested, UserPasswordBeingTested);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using var response = await _baseIntegrationTest.SendRequestAsync(request);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}