using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoiceModel;
using VoiceModel.CallFlow;
using WeatherVoice.Models;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace WeatherVoice.Controllers
{
    public class WeatherController : VoiceController
    {

        public override CallFlow BuildCallFlow()
        {
            CallFlow flow = new CallFlow();
            flow.AddState(ViewStateBuilder.Build("greeting", "getZip", new Say("greeting", "Welcome to Weather Voice.")), true);
            flow.AddState(ViewStateBuilder.Build("getZip", "getWeather", new Ask("getZip", "Enter the five digit zip code for the area where you would like the weather report on.",
                new Grammar(new BuiltinGrammar(BuiltinGrammar.GrammarType.digits,5)))));
            State GetWeatherState = new State("getWeather", "voiceWeather");
            GetWeatherState.OnEntry.Add(delegate(CallFlow cf, State state, Event e)
            {
                //This is a mockup weather service that can be used for testing
                // DAL.IWeatherService service = new DAL.WeatherServiceMockup();

                //This is an implementation of the Google weather service
                //This was an unofficial API and service has been stopped by Google
                //DAL.IWeatherService service = new DAL.GoogleWeatherService();

                //This implementation uses the MSN weather service
                DAL.IWeatherService service = new DAL.MsnWeatherService();
                Weather currWeather = service.getWeather(state.jsonArgs);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string jsonWeather = serializer.Serialize(currWeather);
                cf.FireEvent("continue", jsonWeather);
            });
            flow.AddState(GetWeatherState);

            Prompt weatherPrompt = new Prompt();
            weatherPrompt.audios.Add(new TtsMessage("The temperature today is "));
            weatherPrompt.audios.Add(new TtsVariable("d.temp"));
            weatherPrompt.audios.Add(new Silence(1000));
            weatherPrompt.audios.Add(new TtsMessage("The conditions are "));
            weatherPrompt.audios.Add(new TtsVariable("d.conditions"));

            flow.AddState(ViewStateBuilder.Build("voiceWeather", "goodbye", new Say("voiceWeather", weatherPrompt)));
            flow.AddState(ViewStateBuilder.Build("goodbye", new Exit("goodbye", "Thank you for using Weather Voice. Goodbye.")));
            return flow;

        }

     }
}
