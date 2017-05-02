using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherVoice.Models
{
    public class Weather
    {
        public string temp { get; set; }
        public string conditions { get; set; }
        public string windSpeed { get; set; }
        public string windDirection { get; set; }
    }
}