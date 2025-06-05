using NUnit.Framework;
using Newtonsoft.Json.Linq;
using WeatherApiTests.Builders;

namespace MilosV_ApiAutomation.Tests.ForecastTests
{
    public class GetForecastByCityIdTest
    {
        private string apiKey;

        [SetUp]
        public void Setup()
        {
            apiKey = "302369a12ebdb551c33f213fd0a85eca";
        }

        // Test metoda koja proverava da li API vrati prognozu na osnovu ID-a grada.
        [Test]
        public async Task GetForecastByCityId()
        {
            var response = await new WeatherRequestBuilder("forecast")
                .GetCityId("792680")
                .GetUnits("metric") 
                .GetApiKey(apiKey)
                .ExecuteAsync();

            Console.WriteLine("STATUS: " + response.StatusCode);
            Console.WriteLine("ERROR: " + response.ErrorMessage);
            Console.WriteLine("CONTENT: " + response.Content);

            Assert.That(response.IsSuccessful, Is.True, "API poziv nije uspešan");

            var content = JObject.Parse(response.Content);

            Assert.That(content["city"]?["name"]?.ToString(), Is.EqualTo("Belgrade"));
            Assert.That(content["list"], Is.Not.Null.And.Not.Empty);

            var firstForecast = content["list"]?.First;
            var temp = firstForecast?["main"]?["temp"]?.ToObject<double>();
            var dateTime = firstForecast?["dt_txt"]?.ToString();

            Console.WriteLine($"Prva sledeca prognoza: {dateTime}, temperatura: {temp}°C");
        }
    }
}
