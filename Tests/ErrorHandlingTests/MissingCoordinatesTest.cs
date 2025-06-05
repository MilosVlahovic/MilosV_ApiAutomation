using NUnit.Framework;
using Newtonsoft.Json.Linq;
using WeatherApiTests.Builders;

namespace WeatherApiTests.Tests.ErrorHandlingTests
{
    
    public class MissingCoordinatesTest
    {
        private string apiKey;

        [SetUp]
        public void Setup()
        {
            apiKey = "302369a12ebdb551c33f213fd0a85eca";
        }

        [Test]
        public async Task GetWeatherWithoutCoordinatest()
        {
            var response = await new WeatherRequestBuilder()
                .GetApiKey(apiKey)
                .ExecuteAsync(); // Nema WithCoordinates() poziva.

            Assert.That(response.IsSuccessful, Is.False, "Poziv bez koordinata ne bi trebalo da uspe.");
            Assert.That((int)response.StatusCode, Is.EqualTo(400), "Očekivan je status 400 Bad Request.");

            var content = JObject.Parse(response.Content);
            var message = content["message"]?.ToString();

            Assert.That(message, Is.Not.Null.And.Not.Empty, "Poruka o grešci nije prisutna.");
            Console.WriteLine("Error poruka: " + message);
        }
    }
}
