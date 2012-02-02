using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoiceModel;
using VoiceModel.CallFlow;
using System.Web.Routing;

namespace RecordingExample.Controllers
{
    public class RecordController : VoiceController
    {

        public override VoiceModels BuildVoiceModels()
        {
            VoiceModels views = new VoiceModels();
            Record getRecording = new Record("getRecording", "Please record your information after the beep.  Press the pound key when you are finished.")
            {
                confirm = true
            };
            views.Add(getRecording);
            views.Add(new Exit("goodbye", "Your message has been saved. Goodbye."));

            return views;
            
        }

        public override CallFlow BuildCallFlow()
        {
            CallFlow flow = new CallFlow();
            flow.AddStartState(new State("getRecording", "goodbye"));
            flow.AddState(new State("goodbye"));
            return flow;
           
        }

    }
}
