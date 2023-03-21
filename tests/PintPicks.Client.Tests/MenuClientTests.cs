
using Microsoft.Extensions.DependencyInjection;
using PintPicks.Api.Client.Abstractions;
using PintPicks.Api.Client.Factories;
using PintPicks.Client;
using PintPicks.Api.Client;
using PintPicks.Api.Client.Clients;

namespace PintPicks.Client.Tests
{
    public class MenuClientTests
    {
        protected readonly IPintPicksClientFactory _pintPicksClientFactory;

        public MenuClientTests()
        {
            IServiceCollection services = new ServiceCollection().AddLogging();
            var pintPicksUrl = "https://dl4f2wy9w7.execute-api.us-east-1.amazonaws.com";
            services.AddPintPicksClient(new Uri(pintPicksUrl));
            var serviceProvider = services.BuildServiceProvider();
            _pintPicksClientFactory = serviceProvider.GetService<IPintPicksClientFactory>();
        }

        [Fact]
        public async Task TestMenuGet()
        {
            var client = await _pintPicksClientFactory.CreateAsync<MenuClient>();
            var pints = await client.GetPintsFromMenuResource("WebBeerList120822_page-0001.jpg").ConfigureAwait(false);
            Assert.NotNull(pints);
            Assert.True(pints.Count > 0);
        }

        [Fact]
        public async Task TestUploadFileToS3()
        {
            var client = await _pintPicksClientFactory.CreateAsync<MenuClient>();
            var filePath = "C:\\Users\\ramaya\\Projects\\PintPicks\\tests\\PintPicks.Client.Tests\\assets\\skeletor.jpg";
            var fileKey = await client.UploadFileToS3(filePath).ConfigureAwait(false);
            Assert.NotNull(fileKey);
            Assert.True(fileKey.Length > 0);
        }

        [Fact]
        public async Task TestGetPintsFromMenuImage()
        {
            var client = await _pintPicksClientFactory.CreateAsync<MenuClient>();
            var filePath = "C:\\Users\\ramaya\\Projects\\PintPicks\\tests\\PintPicks.Client.Tests\\assets\\menu1.PNG";
            var pints = await client.GetPintsFromMenuImage(filePath).ConfigureAwait(false);
            Assert.NotNull(pints);
            Assert.True(pints.Count > 0);
        }
    }
}
