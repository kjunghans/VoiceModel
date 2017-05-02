using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoiceModel;
using VoiceModel.CallFlow;
using DynamicMenu.Service;

namespace DynamicMenu.Controllers
{
    public class MenuController : VoiceController
    {
        public override CallFlow BuildCallFlow()
        {
            CallFlow flow = new CallFlow();

            flow.AddState(ViewStateBuilder.Build("greeting", "myMenu", new Say("greeting", "Welcome to the dynamic menu example.")), true);
            //This is a fake service that mimics getting meta-data for the menus from a web service or database
            DynamicMenuService service = new DynamicMenuService();
            VoiceMenu myMenu = service.GetMenu("myMenu");
            Prompt menuOptions = new Prompt();
            //Build the prompts for the menu options form the meta-data
            foreach (MenuOption option in myMenu.Options)
                menuOptions.audios.Add(new TtsMessage(option.PromptMsg + service.GetSelectionPrompt(option.Number)));
            //Create the initial state and view model without any transitions
            State myMenuState = ViewStateBuilder.Build("myMenu", new Ask("myMenu", menuOptions,
                new Grammar(new BuiltinGrammar(BuiltinGrammar.GrammarType.digits, 1, 1))));
            //Add the transitions to the state
            foreach (MenuOption option in myMenu.Options)
                myMenuState.AddTransition("continue", option.TransitionTarget, new Condition("result == '" + option.Number.ToString() + "'"));
            //Add the state to the call flow
            flow.AddState(myMenuState);
            flow.AddState(ViewStateBuilder.Build("doThis", new Exit("doThis", "You selected to do this. Goodbye.")));
            flow.AddState(ViewStateBuilder.Build("doThat", new Exit("doThat", "You selected to do that. Goodbye.")));
            flow.AddState(ViewStateBuilder.Build("doWhatever", new Exit("doWhatever", "You selected to do whatever. Goodbye.")));
            flow.AddState(ViewStateBuilder.Build("invalidSelect", new Exit("invalidSelect", "That was an invalid selection. Goodbye.")));
            return flow;
        }
    }
}
