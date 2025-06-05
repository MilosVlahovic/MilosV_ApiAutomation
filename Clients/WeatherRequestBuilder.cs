using RestSharp;
using WeatherApiTests.Clients;

namespace WeatherApiTests.Builders
{
    public class WeatherRequestBuilder : BaseApiClient
    {
        private readonly RestRequest request;

        // Konstruktor koji omogućava biranje endpointa (default je "weather").
        public WeatherRequestBuilder(string endpoint = "weather")
        {
            request = new RestRequest(endpoint, Method.Get);
        }

        //Metoda za dodavanje koordinata.
        public WeatherRequestBuilder GetCoordinates(string lat, string lon)
        {
            request.AddParameter("lat", lat);
            request.AddParameter("lon", lon);
            return this;
        }

        //Metoda za dodavanje merne jedinice.
        public WeatherRequestBuilder GetUnits(string units)
        {
            request.AddParameter("units", units);
            return this;
        }

        //Metoda za dodavanje jezika odgovora.
        public WeatherRequestBuilder GetLanguage(string lang)
        {
            request.AddParameter("lang", lang);
            return this;
        }

        //Metoda za dodavanje API ključa.
        public WeatherRequestBuilder GetApiKey(string apiKey)
        {
            request.AddParameter("appid", apiKey);
            return this;
        }

        // Metoda za dodavanje imena grada.
        public WeatherRequestBuilder GetCityName(string city)
        {
            request.AddParameter("q", city);
            return this;
        }

        // Metoda za dodavanje City ID-a (za prognozu).
        public WeatherRequestBuilder GetCityId(string cityId)
        {
            request.AddParameter("id", cityId);
            return this;
        }

        // Metoda za dodavanje ZIP koda (za prognozu).
        public WeatherRequestBuilder GetZipCode(string zipCode)
        {
            request.AddParameter("zip", zipCode);
            return this;
        }

        // Metoda za izvršavanje zahteva.
        public async Task<RestResponse> ExecuteAsync()
        {
            return await client.ExecuteAsync(request);
        }
    }
}
