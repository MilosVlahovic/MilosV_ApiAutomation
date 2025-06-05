using NUnit.Framework;
using Newtonsoft.Json.Linq;
using WeatherApiTests.Builders;

namespace WeatherApiTests.Tests.FieldValidationTests
{

    public class ValidateCloudinessPercentageTest
    {
        private string apiKey;

        [SetUp]
        public void Setup()
        {
            apiKey = "302369a12ebdb551c33f213fd0a85eca";
        }

        // Test koji proverava da li je oblačnost u granicama 0–100%.
        [Test]
        public async Task ValidateCloudinessPercentageIsInRange()
        {
            var response = await new WeatherRequestBuilder()
                .GetCoordinates("44.7866", "20.4489")
                .GetApiKey(apiKey)
                .ExecuteAsync();

            Assert.That(response.IsSuccessful, Is.True, "API poziv nije uspeo.");

            var content = JObject.Parse(response.Content);
            var cloudiness = content["clouds"]?["all"]?.ToObject<int?>();

            Assert.That(cloudiness, Is.Not.Null, "Polje clouds.all ne postoji.");
            Assert.That(cloudiness, Is.InRange(0, 100), "clouds.all mora biti između 0 i 100.");

            Console.WriteLine($"Oblačnost: {cloudiness}%");
        }
    }
}
