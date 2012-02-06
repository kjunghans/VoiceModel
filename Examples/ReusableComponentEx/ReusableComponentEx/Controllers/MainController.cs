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
        GetDateDtmfController getDate = new GetDateDtmfController();

        public override VoiceModels BuildVoiceModels()
        {
            VoiceModels views = new VoiceModels();
            views.Add(new Say("greeting","Welcome to the Reusable Dialog Component Example."));
            views.Add(new Exit("goodbye", "Your message has been saved. Goodbye."));
            views.Add(new Component("getStartDate", getDate));
            views.Add(new Component("getEndDate", getDate));
            return views;

        }

        public override CallFlow BuildCallFlow()
        {
            CallFlow flow = new CallFlow();
            flow.AddStartState(new State("greeting", "getStartDate"));
            flow.AddState(new StartComponentState("getStartDate", "getEndDate", getDate, new GetDateDtmfInput(){ReturnAction = this.ActionFullPath, AskDatePrompt = new Prompt("Enter the start date as a six digit number.")}));
            flow.AddState(new StartComponentState("getEndDate", "goodbye", getDate, new GetDateDtmfInput() { ReturnAction = this.ActionFullPath, AskDatePrompt = new Prompt("Enter the finish date as a six digit number.") }));
            flow.AddState(new State("goodbye"));
            return flow;

        }

    }
}
