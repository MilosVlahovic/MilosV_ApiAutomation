using NUnit.Framework;
using Newtonsoft.Json.Linq;
using WeatherApiTests.Builders;

namespace MilosV_ApiAutomation.Tests.CurrentWeatherTests
{
    public class GetCurrentWeatherByCityNameTest
    {
        private string apiKey;

        // Inicijalizacija API ključa za Builder.
        [SetUp]
        public void Setup()
        {
            apiKey = "302369a12ebdb551c33f213fd0a85eca";
        }

        // Test metoda koja proverava da li API vraća ime grada i temperaturu na osnovu imena grada.
        [Test]
        public async Task GetWeatherByCityName()
        {
            var response = await new WeatherRequestBuilder()
                .GetCityName("Belgrade")
                .GetApiKey(apiKey)
                .ExecuteAsync();

            Assert.That(response.IsSuccessful, Is.True, "API call failed");

            var content = JObject.Parse(response.Content);
            Assert.That(content["name"]?.ToString(), Is.EqualTo("Belgrade"));
            Assert.That(content["main"]?["temp"], Is.Not.Null);
        }
    }
}
