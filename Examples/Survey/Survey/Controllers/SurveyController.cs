using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoiceModel;
using VoiceModel.CallFlow;

namespace Survey.Controllers
{
    public class SurveyController : VoiceController
    {
        public override CallFlow BuildCallFlow()
        {
            CallFlow flow = new CallFlow();
            flow.AddState(ViewStateBuilder.Build("greeting", new Say("greeting", "Please take our short survey")), true);
            return flow;

        }

    }
}
