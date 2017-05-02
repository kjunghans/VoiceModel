using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherVoice.Models;
using System.Text;
using System.Net;
using System.IO;
using System.Xml.Linq;

namespace WeatherVoice.DAL
{
    public class MsnWeatherService : IWeatherService
    {
        private XDocument getResponseFromMsn(string zipcode)
        {
            StringBuilder sb = new StringBuilder();
            byte[] buf = new byte[8192];

            HttpWebRequest request = (HttpWebRequest)
                WebRequest.Create("http://weather.service.msn.com/find.aspx?outputview=search&weasearchstr=" + zipcode);

            HttpWebResponse response = (HttpWebResponse)
                request.GetResponse();

            Stream resStream = response.GetResponseStream();
            XDocument weatherDoc = XDocument.Load(resStream);
 
            return weatherDoc;
        }

        private Weather parseXml(XDocument xmlWeather)
        {
            Weather weather = new Weather();

            var current = xmlWeather.Element("weatherdata").Element("weather").Element("current");
            weather.temp = current.Attribute("temperature").Value;
            weather.conditions = current.Attribute("skytext").Value;

            return weather;
        }

        public Weather getWeather(string zipcode)
        {
            XDocument xmlWeather = getResponseFromMsn(zipcode);
            return parseXml(xmlWeather);
        }
    }
}