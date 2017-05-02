using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherVoice.Models;

namespace WeatherVoice.DAL
{
    public interface IWeatherService
    {
        Weather getWeather(string zipcode);
    }
}