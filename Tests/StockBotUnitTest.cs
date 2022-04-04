
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebApiBot;
using Xunit;

namespace Tests
{
    public class StockBotUnitTest
    {
        public readonly HttpClient _client;

        public StockBotUnitTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _client = appFactory.CreateClient();
        }

        [Fact]
        public async Task GetStock_Ok()
        {

            var code = "aapl.us";
            var response = await _client.GetAsync($"api/StockApi/GetStock?stock_code={code}");
            response.EnsureSuccessStatusCode();
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Fact]
        public async Task GetStock_NotSuccess()
        {
            var code = string.Empty;
            var response = await _client.GetAsync($"api/StockApi/GetStock?stock_code={code}");
            var notSuccess = !response.IsSuccessStatusCode;
            Assert.True(notSuccess);
        }
    }
}
