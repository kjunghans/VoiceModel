using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoiceModel;
using VoiceModel.CallFlow;
using RDCs.SayDate;

namespace Example.RdcDynamicVar.Controllers
{
    public class HomeController : VoiceController
    {
        public override CallFlow BuildCallFlow()
        {
            CallFlow flow = new CallFlow();
            flow.AddState(ViewStateBuilder.Build("getDate", "sayDate", 
                new Ask("getDate", 
                    new Prompt("Enter a date as six digits in the format of two digit month, two digit day, and two digit year"), 
                    new Grammar(new BuiltinGrammar(BuiltinGrammar.GrammarType.digits, 6)))), true);
            flow.AddState(new State("sayDate", "goodbye")
            .AddOnEntryAction(delegate(CallFlow cf, State state, Event e)
            {
                cf["dtmfDate"] = state.jsonArgs;
            })
            .AddNestedCallFlow(new SayDate(new Var(flow, "dtmfDate"))));
            flow.AddState(ViewStateBuilder.Build("goodbye", new Exit("goodbye", "Thank you. goodbye.")));
            return flow;

        }

    }
}
