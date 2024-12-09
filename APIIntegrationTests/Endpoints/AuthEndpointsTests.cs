using API;
using Application.Results;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace APIIntegrationTests.Endpoints
{
    public class AuthEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        private readonly WebApplicationFactory<Program> _applicationFactory;
        public AuthEndpointsTests(WebApplicationFactory<Program> applicationFactory)
        {
            _applicationFactory = applicationFactory;
            _httpClient = applicationFactory.CreateClient();
        }

        [Fact]
        public async Task Registration_ShouldReturnTokens_WhenSuccesful()
        {
            //Act
            var response = await _httpClient.PostAsJsonAsync("/register", new { UserName = "Someone", Password = "2345456JH*", Email = "hugo@mail.su", UserRole = "patient" });

            //Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.IsType<Tokens>(response.Content.ReadFromJsonAsync<Tokens>());
        }
    }
}
