using NUnit.Framework;
using Newtonsoft.Json.Linq;
using WeatherApiTests.Builders;

namespace WeatherApiTests.Tests.ErrorHandlingTests
{
    public class InvalidCityTest
    {
        private string apiKey;

        [SetUp]
        public void Setup()
        {
            apiKey = "302369a12ebdb551c33f213fd0a85eca"; 
        }
        // Test koji proverava da li API vraća grešku za nepostojeći grad.
        [Test]
        public async Task GetWeatherWithInvalidCityName()
        {
            var response = await new WeatherRequestBuilder()
                .GetCityName("NekiNepostojeciGrad123")
                .GetApiKey(apiKey)
                .ExecuteAsync();

            Assert.That(response.IsSuccessful, Is.False, "API poziv sa nepostojećim gradom ne bi trebalo da uspe.");
            Assert.That((int)response.StatusCode, Is.EqualTo(404), "Očekivan je status 404 Not Found.");

            var content = JObject.Parse(response.Content);
            Assert.That(content["message"]?.ToString(), Is.EqualTo("city not found"));
        }
    }
}
