using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoiceModel;
using VoiceModel.CallFlow;

namespace TestGrammar.Controllers
{
    public class HomeController : VoiceController
    {
        public override CallFlow BuildCallFlow()
        {
            CallFlow flow = new CallFlow();
            flow.AddState(ViewStateBuilder.Build("greeting", 
                new Ask("greeting", new Prompt("Please respond to this grammar"), 
                    new Grammar(new ResourceLocation(new Var(flow, "grammarLoc", "/grammars/")),"myGrammar.xml"))), true);
            return flow;

        }

    }
}
