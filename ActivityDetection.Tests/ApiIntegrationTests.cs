using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace ActivityDetection.Tests
{
    public class ApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        public ApiIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task WebhookEndpoint_ReturnsOk()
        {
            var jsonPayload = @"{
                ""type"": ""push"",
                ""repo_id"": ""repo001"",
                ""team_name"": ""devs"",
                ""timestamp"": ""2025-03-10T15:00:00Z""
            }";
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/webhook", content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}