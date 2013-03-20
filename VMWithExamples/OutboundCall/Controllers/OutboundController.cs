using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoiceModel;
using VoiceModel.CallFlow;

namespace OutboundCall.Controllers
{
    public class OutboundController : VoiceController
    {
        public override CallFlow BuildCallFlow()
        {
            CallFlow flow = new CallFlow();
            flow.AddState(ViewStateBuilder.Build("greeting", new Exit("greeting", "You have been called by an automated system. Goodbye.")),true);
            return flow;

        }

    }
}
