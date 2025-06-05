using NUnit.Framework;
using Newtonsoft.Json.Linq;
using WeatherApiTests.Builders;

namespace MilosV_ApiAutomation.Tests.UnitsAndLanguageTests
{
    
    public class GetDefaultTemperatureTest
    {
        private string apiKey;

        [SetUp]
        public void Setup()
        {
            apiKey = "302369a12ebdb551c33f213fd0a85eca";
        }

        // Test metoda koja proverava da li API vraća temperaturu u Kelvinima (default units).
        [Test]
        public async Task GetDefaultTemperature()
        {
            var response = await new WeatherRequestBuilder()
                .GetCoordinates("44.7866", "20.4489") // Beograd
                .GetApiKey(apiKey)                   
                .ExecuteAsync();

            Assert.That(response.IsSuccessful, Is.True, "API call failed");

            var content = JObject.Parse(response.Content);
            var temperature = content["main"]?["temp"]?.ToObject<double>();

            Assert.That(temperature, Is.Not.Null, "Temperature value is missing");
            Console.WriteLine($"Trenutna temperatura u Kelvinima (default): {temperature}K");
        }
    }
}
