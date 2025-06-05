using RestSharp;

namespace WeatherApiTests.Clients
{
    public class BaseApiClient
    {
        protected readonly RestClient client;

        //Url OpenWeatherMap API-ja.
        public BaseApiClient()
        {
            client = new RestClient("https://api.openweathermap.org/data/2.5/");
        }


    }
}
