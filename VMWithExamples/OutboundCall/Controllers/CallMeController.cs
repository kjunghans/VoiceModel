using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoiceModel;
using VoiceModel.CallFlow;

namespace OutboundCall.Controllers
{
    public class CallMeController : VoiceController
    {

        public override CallFlow BuildCallFlow()
        {
            CallFlow flow = new CallFlow();
            //Use the Exit object instead of the Say object
            //because this just plays a prompt and exits.  The 
            //Say object expects a transition to another state
            //or object.
            flow.AddState(ViewStateBuilder.Build("greeting", new Exit("greeting", "Hello World")), true);
            return flow;

        }

    }
}
