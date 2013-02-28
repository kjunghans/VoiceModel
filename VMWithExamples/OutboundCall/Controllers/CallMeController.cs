using System;
using System.Collections.Generic;
using System.Configuration;
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
            string phoneNumber = ConfigurationManager.AppSettings["phoneNumber"];
            CallFlow flow = new CallFlow();
            flow.AddState(ViewStateBuilder.Build("call", "greeting", new Call("callMe", phoneNumber)), true);
            flow.AddState(ViewStateBuilder.Build("greeting", new Exit("greeting", "Hello World")));
            return flow;

        }

    }
}
