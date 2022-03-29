using EighthGenerationCompetitive.Api.Identity.Context;
using EighthGenerationCompetitive.Api.V1.ViewModels.Users;
using EighthGenerationCompetitive.IntegrationTest.Dependencies;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using ApiStartup = EighthGenerationCompetitive.Api.Startup;

namespace EighthGenerationCompetitive.IntegrationTest
{
    public class BaseIntegrationTest : IDisposable
    {
        private readonly WebApplicationFactory<ApiStartup> _applicationFactory;
                 
        private readonly HttpClient _eighthGenerationCompetitiveApiClient;
                 
        private readonly ApplicationDbContext _applicationDbContext;

        public readonly TestedUser TestedUser;

        private bool _testsCleaned;

        public BaseIntegrationTest(IOptions<TestedUser> optionsTestedUser)
        {
            _applicationFactory = new WebApplicationFactory<ApiStartup>();

            _eighthGenerationCompetitiveApiClient = _applicationFactory.CreateClient();

             IServiceScope scope = _applicationFactory.Services.CreateScope();

            _applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            TestedUser = optionsTestedUser.Value;
        }

        public async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage requestMessage)
        {
            return await _eighthGenerationCompetitiveApiClient.SendAsync(requestMessage);
        }

        public async Task<string> AuthenticateUserRequest(string userName, string userPassword)
        {
            string loginEndpoint = $"/api/v1/users/login";

            LoginViewModel loginData = new LoginViewModel() { UserName = userName, Password = userPassword };

            var request = new HttpRequestMessage(HttpMethod.Post, loginEndpoint)
            {
                Content = JsonContent.Create(loginData)
            };

            var response = await _eighthGenerationCompetitiveApiClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            var serializeOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var userLoginData = JsonSerializer.Deserialize<UserLoginViewModel>(responseContent, serializeOptions);

            return userLoginData.AccessToken;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_testsCleaned)
            {
                if (disposing)
                {
                    ClearTestedUser();
                    ClearDependencies();
                }

                _testsCleaned = true;
            }
        }

        private void ClearTestedUser()
        {
            string userDeleteStatement = $"DELETE FROM [AspNetUsers] WHERE UserName='{TestedUser.UserNameBeingTested}'";

           _applicationDbContext.Database.ExecuteSqlRaw(userDeleteStatement);
        }

        private void ClearDependencies()
        {
            _applicationDbContext.Dispose();
            _eighthGenerationCompetitiveApiClient.Dispose();
            _applicationFactory.Dispose();
        }
    }
}