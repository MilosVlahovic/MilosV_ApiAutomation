using NUnit.Framework;
using Newtonsoft.Json.Linq;
using WeatherApiTests.Builders;

namespace WeatherApiTests.Tests.FieldValidationTests
{
    public class ValidateWindSpeedTest
    {
        private string apiKey;

        [SetUp]
        public void Setup()
        {
            apiKey = "302369a12ebdb551c33f213fd0a85eca";
        }

        // Test koji proverava da li je wind.speed broj i da li je >= 0
        [Test]
        public async Task ValidateWindSpeedIsNumericAndPositive()
        {
            var response = await new WeatherRequestBuilder()
                .GetCoordinates("44.7866", "20.4489")
                .GetApiKey(apiKey)
                .ExecuteAsync();

            Assert.That(response.IsSuccessful, Is.True, "API poziv nije uspeo.");

            var content = JObject.Parse(response.Content);
            var windSpeed = content["wind"]?["speed"]?.ToObject<double?>();

            Assert.That(windSpeed, Is.Not.Null, "Polje wind.speed ne postoji.");
            Assert.That(windSpeed, Is.GreaterThanOrEqualTo(0), "wind.speed mora biti >= 0.");

            Console.WriteLine($"Brzina vetra: {windSpeed} m/s");
        }
    }
}
