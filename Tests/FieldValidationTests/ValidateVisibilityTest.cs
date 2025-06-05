using NUnit.Framework;
using Newtonsoft.Json.Linq;
using WeatherApiTests.Builders;

namespace WeatherApiTests.Tests.FieldValidationTests
{
    public class ValidateVisibilityTest
    {
        private string apiKey;

        [SetUp]
        public void Setup()
        {
            apiKey = "302369a12ebdb551c33f213fd0a85eca";
        }

        // Test koji proverava da li je vidljivost između 0 i 10.000 metara.
        [Test]
        public async Task ValidateVisibilityIsInValidRange()
        {
            var response = await new WeatherRequestBuilder()
                .GetCoordinates("44.7866", "20.4489")
                .GetApiKey(apiKey)
                .ExecuteAsync();

            Assert.That(response.IsSuccessful, Is.True, "API poziv nije uspeo.");

            var content = JObject.Parse(response.Content);
            var visibility = content["visibility"]?.ToObject<int?>();

            Assert.That(visibility, Is.Not.Null, "Polje visibility ne postoji.");
            Assert.That(visibility, Is.InRange(0, 10000), "visibility mora biti između 0 i 10.000 metara.");

            Console.WriteLine($"Vidljivost: {visibility} m");
        }
    }
}
