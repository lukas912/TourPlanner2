﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner_Model
{
    public class WeatherData
    {
        public string City { get; set; }
        public string Description { get; set; }
        public string Temperature { get; set; }
        public string Humidity { get; set; }
        public string WindSpeed { get; set; }

        public WeatherData(string city, string desc, string temp, string humid, string windspd)
        {
            this.City = city;
            this.Description = desc;
            this.Temperature = temp + " °C";
            this.Humidity = humid + " %";
            this.WindSpeed = windspd + " m/s";
            
        }
    }
}
