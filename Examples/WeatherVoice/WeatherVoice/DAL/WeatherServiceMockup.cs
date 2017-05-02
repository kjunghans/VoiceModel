using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherVoice.Models;

namespace WeatherVoice.DAL
{
    public class WeatherServiceMockup : IWeatherService
    {
        public Weather getWeather(string zipcode)
        {
            Weather w = new Weather();
            w.temp = "72";
            w.conditions = "sunny";
            w.windSpeed = "10";
            w.windDirection = "south";
            return w;
        }
        
    }
}