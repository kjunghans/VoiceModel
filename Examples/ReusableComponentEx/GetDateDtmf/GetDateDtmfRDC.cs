using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoiceModel;
using VoiceModel.CallFlow;

namespace GetDateDtmf
{
    public class GetDateDtmfRDC : CallFlow
    {
        public GetDateDtmfRDC(Prompt AskDatePrompt)
        {
            BuildCallFlow(AskDatePrompt);

        }

        private void BuildCallFlow(Prompt AskDatePrompt)
        {
            AddState(ViewStateBuilder.Build("getDate", "validateDate", 
                new Ask("getDate", AskDatePrompt, new Grammar("digits?minlength=6"))), true);
            AddState(new State("validateDate", "confirmDate")
                .AddTransition("error","invalidDate",null)
                .AddOnEntryAction(delegate(CallFlow cf, State state, Event e)
                {
                    ValidateDate validator = new ValidateDate(cf, state);
                    validator.Validate();
                }));
            Prompt confirmPrompt = new Prompt();
            confirmPrompt.audios.Add(new TtsMessage("You Entered"));
            confirmPrompt.audios.Add(new TtsVariable("d.Month"));
            confirmPrompt.audios.Add(new TtsVariable("d.Day"));
            confirmPrompt.audios.Add(new TtsVariable("d.Year"));

            AddState(ViewStateBuilder.Build("confirmDate", new Say("confirmDate", confirmPrompt)));
            AddState(ViewStateBuilder.Build("invalidDate", new Say("invalidDate", "You entered and invalid date.")));
        }

        public GetDateDtmfOutput GetResults()
        {
            return ctx.GetGlobalAs<GetDateDtmfOutput>("GetDateDtmfOutput");
        }

    }
}
