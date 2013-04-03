using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoiceModel;
using VoiceModel.CallFlow;
using RDCs.Utils;

namespace RDCs.SayDate
{
    public class SayDate : CallFlow
    {

        Var _date;
        
        public SayDate(Var date)
        {
            _date = date;
            BuildCallFlow();
        }

        private void BuildCallFlow()
        {
            AddState(new State("validateDate", "confirmDate")
            .AddTransition("error", "invalidDate", null)
            .AddOnEntryAction(delegate(CallFlow cf, State state, Event e)
            {
                var dtmfDate = _date.Value;
                try
                {
                    string jsonDate = DateTimeUtils.ConvertToJson(dtmfDate);
                    cf.FireEvent("continue", jsonDate);
                }
                catch (Exception ex)
                {
                    cf.FireEvent("error", null);
                }
             }),true);

            Prompt confirmPrompt = new Prompt();
            confirmPrompt.audios.Add(new TtsMessage("You Entered"));
            confirmPrompt.audios.Add(new TtsVariable("d.Month"));
            confirmPrompt.audios.Add(new TtsVariable("d.Day"));
            confirmPrompt.audios.Add(new TtsVariable("d.Year"));
            confirmPrompt.bargein = false;

            AddState(ViewStateBuilder.Build("confirmDate", new Say("confirmDate", confirmPrompt)));
            AddState(ViewStateBuilder.Build("invalidDate", new Say("invalidDate", new Prompt("You entered and invalid date.") { bargein = false }) ));

        }
    }
}
