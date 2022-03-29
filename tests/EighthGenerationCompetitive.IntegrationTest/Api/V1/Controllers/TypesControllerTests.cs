using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace EighthGenerationCompetitive.IntegrationTest.Api.V1.Controllers
{
    public class TypesControllerTests : IClassFixture<BaseIntegrationTest>
    {
        private readonly BaseIntegrationTest _baseIntegrationTest;

        public TypesControllerTests(
            BaseIntegrationTest baseIntegrationTest)
        {
            _baseIntegrationTest = baseIntegrationTest;
        }

        [Theory]
        [InlineData("Fire")]
        [InlineData("Dragon")]
        [InlineData("Fairy")]
        public async Task GetPokemonTypeByNameAsync_ValidPokemonTypeName_SuccessfulRequest(string pokemonTypeName)
        {
            string getPokemonTypeByNameEndpoint = $"/api/v1/types/{pokemonTypeName}";

            var request = new HttpRequestMessage(HttpMethod.Get, getPokemonTypeByNameEndpoint);

            var response = await _baseIntegrationTest.SendRequestAsync(request);
  
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("-typeName", 2, 5)]
        [InlineData("typeName", 1, 20)]
        public async Task GetPokemonTypesAsync_ValidParameters_SuccessfulRequest(
            string sortByClause, 
            int pageNumber, 
            int pageSize)
        {
            string getPokemonTypesEndpoint = $"/api/v1/types?sortByClause={sortByClause}&pageNumber={pageNumber}&pageSize={pageSize}";

            var request = new HttpRequestMessage(HttpMethod.Get, getPokemonTypesEndpoint);

            var response = await _baseIntegrationTest.SendRequestAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(response.Headers.Contains("X-Pagination"));
        }
    }
}