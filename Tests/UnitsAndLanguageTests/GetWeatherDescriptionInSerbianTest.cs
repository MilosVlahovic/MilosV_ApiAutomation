using NUnit.Framework;
using Newtonsoft.Json.Linq;
using WeatherApiTests.Builders;

namespace MilosV_ApiAutomation.Tests.UnitsAndLanguageTests
{
    
    public class GetWeatherDescriptionInSerbianTest
    {
        private string apiKey;

        [SetUp]
        public void Setup()
        {
            apiKey = "302369a12ebdb551c33f213fd0a85eca";
        }

        // Test metoda koja proverava da li API vraća opis vremena i osnovne podatke na srpskom jeziku.
        [Test]
        public async Task GetWeatherDescriptionInSerbian()
        {
            var response = await new WeatherRequestBuilder()
                .GetCoordinates("45.2671", "19.8335") 
                .GetLanguage("sr")
                .GetApiKey(apiKey)
                .ExecuteAsync();

            Assert.That(response.IsSuccessful, Is.True, "API call failed");

            var content = JObject.Parse(response.Content);

            var description = content["weather"]?.First?["description"]?.ToString();
            var city = content["name"]?.ToString();
            var country = content["sys"]?["country"]?.ToString();

            Assert.That(description, Is.Not.Null.And.Not.Empty, "Description is missing");
            Assert.That(city, Is.EqualTo("Novi Sad"), "City name is incorrect");
            Assert.That(country, Is.EqualTo("RS"), "Country code is incorrect");
              
            Console.WriteLine($"Grad: {city}, Zemlja: {country}");
            Console.WriteLine($"Opis vremena: {description}");
        }
    }
}
