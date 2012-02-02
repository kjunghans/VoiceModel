using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VoiceModel;
using VoiceModel.CallFlow;

namespace GetDateDtmf
{
    public class GetDateDtmfController : VoiceController
    {
         public override VoiceModels BuildVoiceModels()
        {
            VoiceModels views = new VoiceModels();
            views.Add(new Ask("getDate", "Enter the date as a six digit number in the format month month day day year year", new Grammar("digits?minlength=6")));
             Prompt confirmPrompt = new Prompt();
             confirmPrompt.audios.Add(new TtsMessage("You Entered"));
             confirmPrompt.audios.Add(new TtsVariable("d.Month"));
             confirmPrompt.audios.Add(new TtsVariable("d.Day"));
             confirmPrompt.audios.Add(new TtsVariable("d.Year"));
            views.Add(new Say("confirmDate", confirmPrompt));
             views.Add(new Say("invalidDate","You entered and invalid date."));

            return views;

        }

        public override CallFlow BuildCallFlow()
        {
            CallFlow flow = new CallFlow();
            flow.AddStartState(new State("getDate","validateDate"));
            flow.AddState(new ValidateDate("validateDate","confirmDate","invalidDate"));
            flow.AddState(new State("confirmDate"));
            flow.AddState(new State("invalidDate"));
            return flow;
        }

     }
}
