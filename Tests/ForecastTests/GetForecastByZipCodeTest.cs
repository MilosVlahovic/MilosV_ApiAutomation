using NUnit.Framework;
using Newtonsoft.Json.Linq;
using WeatherApiTests.Builders;

namespace MilosV_ApiAutomation.Tests.ForecastTests
{
    public class GetForecastByZipCodeTest
    {
        private string apiKey;

        [SetUp]
        public void Setup()
        {
            apiKey = "302369a12ebdb551c33f213fd0a85eca";
        }

        // Test metoda koja proverava da li API vrati prognozu na osnovu ZIP koda.
        [Test]
        public async Task GetForecastByZipCode()
        {
            var response = await new WeatherRequestBuilder("forecast")
                .GetZipCode("10001,us")
                .GetUnits("metric")
                .GetApiKey(apiKey)
                .ExecuteAsync();

            Assert.That(response.IsSuccessful, Is.True, "API poziv nije uspešan");

            var content = JObject.Parse(response.Content);

            Assert.That(content["city"]?["name"]?.ToString(), Is.EqualTo("New York"));
            Assert.That(content["list"], Is.Not.Null.And.Not.Empty);

            var firstForecast = content["list"]?.First;
            var temp = firstForecast?["main"]?["temp"]?.ToString();
            var dateTime = firstForecast?["dt_txt"]?.ToString();

            Console.WriteLine($"Prva sledeca prognoza: {dateTime}, temperatura: {temp}°C");
        }
    }
}
