using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GetDateDtmf;
using ReusableComponentEx.States;
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
            //This tells ViewModel to get the views from the reusable component.
            views.Add(new GetDateDtmfView("getStartDate"));
            views.Add(new GetDateDtmfView("getFinishDate"));
            Prompt sayDiff = new Prompt();
            sayDiff.audios.Add(new TtsMessage("The difference between the start and finish dates is "));
            sayDiff.audios.Add(new TtsVariable("d.daysDiff"));
            sayDiff.audios.Add(new TtsMessage(" days."));
            views.Add(new Say("differenceInDays",sayDiff));
            views.Add(new Exit("goodbye", "Goodbye."));
            return views;

        }

        public override CallFlow BuildCallFlow()
        {
            CallFlow flow = new CallFlow();
            flow.AddStartState(new State("greeting", "getStartDate"));
            //This tells the state machine to use the state machine in the reusable component.
            flow.AddState(new GetDateDtmfState("getStartDate", "saveStartDate", 
                new GetDateDtmfInput()
                {ReturnAction = this.ActionName, AskDatePrompt = new Prompt("Enter the start date as a six digit number.")}));
            //When we return from the reusable component we need to do something with the information returned (i.e. the date entered).
            flow.AddState(new SaveStartDate("saveStartDate", "getFinishDate"));
            //Call the reusable component again to get the finish date.
            flow.AddState(new GetDateDtmfState("getFinishDate", "saveFinishDate",  
                new GetDateDtmfInput() 
                { ReturnAction = this.ActionName, AskDatePrompt = new Prompt("Enter the finish date as a six digit number.") }));
            //Get the finish date and calculate the difference between the start and finish in days.
            flow.AddState(new SaveFinishDate("saveFinishDate", "differenceInDays"));
            flow.AddState(new State("differenceInDays", "goodbye"));
            flow.AddState(new State("goodbye"));
            return flow;

        }

    }
}
