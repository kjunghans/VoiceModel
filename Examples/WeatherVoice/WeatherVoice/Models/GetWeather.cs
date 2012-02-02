using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoiceModel.CallFlow;
using WeatherVoice.DAL;
using System.Web.Script.Serialization;

namespace WeatherVoice.Models
{
    public class GetWeather : State
    {
        IWeatherService _service;

        public override void OnEntry()
        {
            Weather currWeather = _service.getWeather(this.jsonArgs);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonWeather = serializer.Serialize(currWeather);
            string nextStateID;
            string nextStatetArgs;
            this.Flows.FireEvent(this.Id, "continue", jsonWeather,out nextStateID, out nextStatetArgs);
        }

        public GetWeather(string id, string target, IWeatherService service)
            : base(id)
        {
            this.AddTransition("continue", target, null);
            this._service = service;
        }
    }
}