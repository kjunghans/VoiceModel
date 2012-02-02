using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GetDateDtmf;
using VoiceModel;
using VoiceModel.CallFlow;

namespace ReusableComponentEx.Controllers
{
    public class MainController : VoiceController
    {
        public override VoiceModels BuildVoiceModels()
        {
            VoiceModels views = new VoiceModels();
            views.Add(new Say("greeting","Welcome to the Reusable Dialog Component Example."));
            views.Add(new Exit("goodbye", "Your message has been saved. Goodbye."));
            views.Add(new Component("getStartDate", new GetDateDtmfController(), new GetDateDtmfInput(){returnController = "Main"}));

            return views;

        }

        public override CallFlow BuildCallFlow()
        {
            CallFlow flow = new CallFlow();
            flow.AddStartState(new State("greeting", "getStartDate"));
            flow.AddState(new State("getStartDate","goodbye"));
            //flow.AddState(new StartComponentState("getDate", "goodbye", new GetDateDtmfController(), new GetDateDtmfInput() { returnController = "Main" }));
            //flow.AddState(new State("getDate","goodbye"));
            flow.AddState(new State("goodbye"));
            
            return flow;

        }

    }
}
