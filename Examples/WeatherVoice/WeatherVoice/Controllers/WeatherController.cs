using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoiceModel;
using VoiceModel.CallFlow;
using WeatherVoice.Models;
using System.Web.Routing;

namespace WeatherVoice.Controllers
{
    public class WeatherController : VoiceController
    {
        public override VoiceModels BuildVoiceModels()
        {
            VoiceModels views = new VoiceModels();
            //Create a base document that will be used by all of the objects
            //That specifies what action to use in the Controller.  This action
            //will be used for every object to run our state machine.
            VxmlDocument doc = new VxmlDocument();
            doc.controllerName = "StateMachine";
            views.Add(new Say(doc, "greeting", "Welcome to Weather Voice."));
            views.Add(new Ask(doc, "getZip", "Enter the five digit zip code for the area where you would like the weather report on.",
                new Grammar("digits?minlength=5")));
            Prompt weatherPrompt = new Prompt();
            weatherPrompt.audios.Add(new TtsMessage("The temperature today is "));
            weatherPrompt.audios.Add(new TtsVariable("d.temp"));
            weatherPrompt.audios.Add(new Silence(1000));
            weatherPrompt.audios.Add(new TtsMessage("The conditions are "));
            weatherPrompt.audios.Add(new TtsVariable("d.conditions"));
            views.Add(new Say(doc, "voiceWeather", weatherPrompt));
            views.Add(new Exit("goodbye", "Thank you for using Weather Voice. Goodbye."));

            return views;

        }

        public override CallFlow BuildCallFlow()
        {
            CallFlow flow = new CallFlow();
            flow.AddStartState(new State("greeting", "getZip"));
            flow.AddState(new State("getZip", "getWeather"));
            //Uncomment next line and comment the following if you want to use the mockup instead of the Google service
            //flow.AddState(new GetWeather("getWeather", "voiceWeather", new DAL.WeatherServiceMockup()));
            //This implementation uses the Google Weather API to get the current weather conditions
            flow.AddState(new GetWeather("getWeather", "voiceWeather", new DAL.GoogleWeatherService()));
            flow.AddState(new State("voiceWeather", "goodbye"));
            flow.AddState(new State("goodbye"));
            return flow;

        }

        public override string ControllerName
        {
            get { return "Weather"; }
        }
    }
}
