using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoiceModel;
using VoiceModel.CallFlow;

namespace TestTropoSms.Controllers
{
    public class SmsController : VoiceController
    {
        public override CallFlow BuildCallFlow()
        {
            CallFlow flow = new CallFlow();
            //Use the Exit object instead of the Say object
            //because this just plays a prompt and exits.  The 
            //Say object expects a transition to another state
            //or object.
            Prompt greetingPrompt = new Prompt(new TtsMessage(new Var(flow, "greetingMsg")));
            flow.AddState(ViewStateBuilder.Build("greeting", new Exit("greeting", greetingPrompt))
                .AddOnEntryAction(delegate(CallFlow cf, State state, Event e)
                {
                    if (cf["Channel"] == "TEXT")
                        cf["greetingMsg"] = "Hello " + cf["ANI"] + ". Thank you for using " + cf["AppId"]
                            + ". You said, " + cf["InitialText"];
                    else
                        cf["greetingMsg"] = "Hello Voice Caller.";
                }), true);
            return flow;

        }


    }
}
