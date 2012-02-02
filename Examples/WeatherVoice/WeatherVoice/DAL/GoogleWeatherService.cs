using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherVoice.Models;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;

namespace WeatherVoice.DAL
{
    public class GoogleWeatherService : IWeatherService
    {
        private string getResponseFromGoogle(string zipcode)
        {
            StringBuilder sb = new StringBuilder();
            byte[] buf = new byte[8192];

            HttpWebRequest request = (HttpWebRequest)
                WebRequest.Create("http://www.google.com/ig/api?weather=" + zipcode);

            HttpWebResponse response = (HttpWebResponse)
                request.GetResponse();

            Stream resStream = response.GetResponseStream();
            string tempString = null;
            int count = 0;

            do
            {
                count = resStream.Read(buf, 0, buf.Length);
                if (count != 0)
                {
                    tempString = Encoding.ASCII.GetString(buf, 0, count);
                    sb.Append(tempString);
                }
            }
            while (count > 0);

            return sb.ToString();
        }

        private Weather parseXml(string xmlWeather)
        {
            Weather w = new Weather();
            XmlDocument weatherDoc = new XmlDocument();
            weatherDoc.LoadXml(xmlWeather);
            XmlNode wNode = weatherDoc.SelectSingleNode("/xml_api_reply/weather/current_conditions/condition");
            w.conditions = wNode.Attributes["data"].Value;
            wNode = weatherDoc.SelectSingleNode("/xml_api_reply/weather/current_conditions/temp_f");
            w.temp = wNode.Attributes["data"].Value;
            wNode = weatherDoc.SelectSingleNode("/xml_api_reply/weather/current_conditions/wind_condition");
            string windCondition = wNode.Attributes["data"].Value;
            char[] delims = { ' ' };
            string[] windParts = windCondition.Split(delims);
            w.windDirection = windParts[1];
            w.windSpeed = windParts[3];
            return w;
        }

        public Weather getWeather(string zipcode)
        {
            string weatherSerializedXml = getResponseFromGoogle(zipcode);
            Weather w = parseXml(weatherSerializedXml);
            return w;
        }
    }
}