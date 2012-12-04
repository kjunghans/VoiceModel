using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoiceModel;
using VoiceModel.CallFlow;

namespace Menus.Controllers
{
    public class HomeController : VoiceController
    {
        public override CallFlow BuildCallFlow()
        {
            CallFlow flow = new CallFlow();

            flow.AddState(ViewStateBuilder.Build("mainMenu", new Ask("mainMenu", "Press one for option one. Press two for option two.",
                new Grammar(new BuiltinGrammar(BuiltinGrammar.GrammarType.digits, 1, 1))))
                .AddTransition("continue", "optionOne", new Condition("result == '1'"))
                .AddTransition("continue", "optionTwo", new Condition("result == '2'"))
                .AddTransition("continue", "invalidSelect", new Condition("result != '1' && result != '2'")),true);
            flow.AddState(ViewStateBuilder.Build("optionOne",new Exit("optionOne","You selected option one. Goodbye.")));
            flow.AddState(ViewStateBuilder.Build("optionTwo",new Exit("optionTwo","You selected option two. Goodbye.")));
            flow.AddState(ViewStateBuilder.Build("invalidSelect",new Exit("invalidSelect","That was an invalid selection. Goodbye.")));
            return flow;
        }

    }
}
