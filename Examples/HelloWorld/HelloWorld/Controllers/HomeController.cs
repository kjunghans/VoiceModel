using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoiceModel;
using VoiceModel.CallFlow;

namespace HelloWorld.Controllers
{
    public class HomeController : VoiceController
    {
        public override VoiceModels BuildVoiceModels()
        {
            VoiceModels views = new VoiceModels();

            //Use the Exit object instead of the Say object
            //because this just plays a prompt and exits.  The 
            //Say object expects a transition to another state
            //or object.
            views.Add(new Exit("greeting", "Hello World"));

            return views;

        }

        public override CallFlow BuildCallFlow()
        {
            CallFlow flow = new CallFlow();
            flow.AddStartState(new State("greeting"));
            return flow;

        }

 
    }
}
