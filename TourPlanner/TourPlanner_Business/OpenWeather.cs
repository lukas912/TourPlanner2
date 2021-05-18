using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using TourPlanner_Model;

namespace TourPlanner_Business
{
    public class OpenWeather
    {
        public string KEY = "49720b74d46a9b89d62da24abbce9400";
        public string BASE_URL = "https://www.mapquestapi.com/staticmap/v5/map?units=metric&start=";

        public WeatherData getWeatherData(string city)
        {
            string url = "https://api.openweathermap.org/data/2.5/weather?units=metric&q=" + city + "&appid=" + KEY;

            var client = new RestClient(url);

            var request = new RestRequest();

            var response = client.Get(request);

            Debug.Write(response.Content);

            JObject obj = JObject.Parse(response.Content);


            WeatherData wd = new WeatherData(city, (string)obj["weather"][0]["description"], (string)obj["main"]["temp"], (string)obj["main"]["humidity"], (string)obj["wind"]["speed"]);

            return wd;
        }
    }
}
