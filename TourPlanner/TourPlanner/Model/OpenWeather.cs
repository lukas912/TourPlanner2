using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TourPlanner.Model
{
    class OpenWeather
    {
        public string KEY = "49720b74d46a9b89d62da24abbce9400";
        public string BASE_URL = "https://www.mapquestapi.com/staticmap/v5/map?start=";

        public WeatherData getWeatherData(string city)
        {
            string url = "api.openweathermap.org/data/2.5/weather?q=" + city + "&appid=" + KEY;

            var client = new RestClient(url);

            var request = new RestRequest();

            var response = client.Get(request);

            JObject obj = JObject.Parse(response.Content);


            WeatherData wd = new WeatherData(city, (string)obj["weather"]["description"], (string)obj["main"]["temp"], (string)obj["main"]["humidity"], (string)obj["wind"]["speed"]);

            return wd;
        }
    }
}
