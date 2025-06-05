using NUnit.Framework;
using Newtonsoft.Json.Linq;
using WeatherApiTests.Builders;

namespace WeatherApiTests.Tests.ErrorHandlingTests
{

    public class InvalidApiKeyTest
    {
        //Negativan test: Nevažeći API ključ.
        [Test]
        public async Task GetWeatherWithInvalidApiKey()
        {
            var response = await new WeatherRequestBuilder()
                .GetCoordinates("44.7866", "20.4489") // Beograd
                .GetApiKey("invalid_api_key")
                .ExecuteAsync();

            Assert.That(response.IsSuccessful, Is.False, "Poziv ne bi trebalo da uspe sa nevažećim API ključem.");
            Assert.That((int)response.StatusCode, Is.EqualTo(401), "Očekivan je status 401 Unauthorized.");

            var content = JObject.Parse(response.Content);
            Assert.That(content["message"]?.ToString(), Is.EqualTo(
                "Invalid API key. Please see https://openweathermap.org/faq#error401 for more info."
            ));
        }
    }
}
