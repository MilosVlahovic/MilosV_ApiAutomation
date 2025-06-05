using NUnit.Framework;
using Newtonsoft.Json.Linq;
using WeatherApiTests.Builders;

namespace MilosV_ApiAutomation.Tests.UnitsAndLanguageTests
{
    
    public class GetTemperatureInCelsiusTest
    {
        private string apiKey;

        [SetUp]
        public void Setup()
        {
            apiKey = "302369a12ebdb551c33f213fd0a85eca"; 
        }

        //Test metoda koja proverava da li API vraća trenutnu temperaturu u Celzijusima na osnovu koordinata.
        [Test]
        public async Task GetTemperatureInCelsius()
        {
            var response = await new WeatherRequestBuilder()
                .GetCoordinates("44.7866", "20.4489") 
                .GetUnits("metric")                   
                .GetApiKey(apiKey)
                .ExecuteAsync();

            Assert.That(response.IsSuccessful, Is.True, "API call failed");

            var content = JObject.Parse(response.Content);
            var temperature = content["main"]?["temp"]?.ToObject<double>();

            Assert.That(temperature, Is.Not.Null, "Temperature value is missing");
            Console.WriteLine($" Trenutna temperatura u Celzijusima: {temperature}°C");
        }
    }
}
