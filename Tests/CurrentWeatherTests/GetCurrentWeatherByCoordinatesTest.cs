using NUnit.Framework;
using Newtonsoft.Json.Linq;
using WeatherApiTests.Builders;

namespace MilosV_ApiAutomation.Tests.CurrentWeatherTests
{
    public class GetCurrentWeatherByCoordinatesTest
    {
        private string apiKey;

        // Inicijalizacija API ključa za Builder.
        [SetUp]
        public void Setup()
        {
            apiKey = "302369a12ebdb551c33f213fd0a85eca";
        }

        // Test metoda koja proverava da li API vraća ime grada i temperaturu na osnovu koordinata.
        [Test]
        public async Task GetWeatherByCoordinates()
        {
            // Koordinate za lokaciju
            var response = await new WeatherRequestBuilder()
                .GetCoordinates("44.7866", "20.4489")
                .GetApiKey(apiKey)
                .ExecuteAsync();

            Assert.That(response.IsSuccessful, Is.True, "API call failed");

            var content = JObject.Parse(response.Content);
            Assert.That(content["name"]?.ToString(), Is.Not.Empty);
            Assert.That(content["main"]?["temp"], Is.Not.Null);
        }
    }
}
