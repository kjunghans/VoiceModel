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

        public override CallFlow BuildCallFlow()
        {
            CallFlow flow = new CallFlow();
            flow.AddState(ViewStateBuilder.Build("getRecording", "goodbye",
                new Record("getRecording", "Please record your information after the beep.  Press the pound key when you are finished.")
                  {confirm = true }),true);
            flow.AddState(ViewStateBuilder.Build("goodbye", new Exit("goodbye", "Your message has been saved. Goodbye.")));
            return flow;
           
        }

    }
}
