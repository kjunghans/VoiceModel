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
            List<VxmlProperty> appProperties = new List<VxmlProperty>();
            appProperties.Add(new VxmlProperty("inputmode", "dtmf"));
            AskDatePrompt.bargein = false;
            AddState(ViewStateBuilder.Build("getDate", "validateDate",
                new Ask("getDate", AskDatePrompt, new Grammar(new BuiltinGrammar(BuiltinGrammar.GrammarType.digits, 6))) { properties = appProperties }), true);
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
            confirmPrompt.bargein = false;

            AddState(ViewStateBuilder.Build("confirmDate", new Say("confirmDate", confirmPrompt) { properties = appProperties }));
            AddState(ViewStateBuilder.Build("invalidDate", new Say("invalidDate", new Prompt("You entered and invalid date.") {bargein = false }) { properties = appProperties }));
        }

        public GetDateDtmfOutput GetResults()
        {
            return ctx.GetGlobalAs<GetDateDtmfOutput>("GetDateDtmfOutput");
        }

    }
}
