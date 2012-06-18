using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoiceModel;
using VoiceModel.CallFlow;
using System.Configuration;
using CommandMgr.Sdk;

namespace MayhemVoice.Controllers
{
    public class MayhemController : VoiceController
    {
        public override CallFlow BuildCallFlow()
        {
            CallFlow flow = new CallFlow();
            flow.AddState(ViewStateBuilder.Build("greeting", "assist",
                new Say("greeting", new Prompt("Hello. This is Mayhem.") {bargein = false })), true);
            string apiUrl = ConfigurationManager.AppSettings["commandMgrUrl"];
            CommandMgr.Sdk.CommandMgr commService = new CommandMgr.Sdk.CommandMgr(apiUrl);
            List<Command> commands = commService.ListCommands();
            List<string> commandNames = new List<string>();
            foreach (Command c in commands)
                commandNames.Add(c.Name);
            Prompt assistNoinput = new Prompt("I could not hear you. Please let me know what you want me to do.") { bargein = false };
            List<Prompt> assistNoinputs = new List<Prompt>();
            assistNoinputs.Add(assistNoinput);
            assistNoinputs.Add(assistNoinput);
            Prompt assistNomatch = new Prompt("I could not understand you. Please let me know what you want me to do.") { bargein = false };
            List<Prompt> assistNomatches = new List<Prompt>();
            assistNomatches.Add(assistNomatch);
            assistNomatches.Add(assistNomatch);
            flow.AddState(ViewStateBuilder.Build("assist", "queueCommand",
                new Ask("assist", new Prompt("How may I assist you?") { bargein = false }, new Grammar("commands", commandNames))
                {
                    noinputPrompts = assistNoinputs,
                    nomatchPrompts = assistNomatches
                })
                .AddTransition("nomatch", "didNotUnderstand", null)
                .AddTransition("noinput", "didNotUnderstand", null));

            flow.AddState(ViewStateBuilder.Build("didNotUnderstand", "goodbye", 
                new Say("didNotUnderstand", "I did not understand your request.")));

            flow.AddState(new State("queueCommand", "commandSent")
                .AddTransition("error","errSendingCommand",null)
                .AddOnEntryAction(delegate(CallFlow cf, State state, Event e)
                {
                    try
                    {
                        string url = ConfigurationManager.AppSettings["commandMgrUrl"];
                        CommandMgr.Sdk.CommandMgr service = new CommandMgr.Sdk.CommandMgr(url);
                        service.Queue(state.jsonArgs);
                        cf.FireEvent("continue", "");
                    }
                    catch
                    {
                        cf.FireEvent("error", "");
                    }

                }));
            flow.AddState(ViewStateBuilder.Build("commandSent", "doMore", new Say("commandSent", new Prompt("Your request has been sent.") { bargein = false })));
            flow.AddState(ViewStateBuilder.Build("errSendingCommand", "doMore", new Say("errSendingCommand", "There was an error sending your request.")));

            Prompt doMoreNoinput = new Prompt("I could not hear you. Let me know if I can assist with anything else by saying yes or no.") { bargein = false };
            List<Prompt> doMoreNoinputs = new List<Prompt>();
            doMoreNoinputs.Add(assistNoinput);
            doMoreNoinputs.Add(assistNoinput);
            Prompt doMoreNomatch = new Prompt("I could not understand you. Let me know if I can assist with anything else by saying yes or no.") { bargein = false };
            List<Prompt> doMoreNomatches = new List<Prompt>();
            doMoreNomatches.Add(doMoreNomatch);
            doMoreNomatches.Add(doMoreNomatch);
            List<string> doMoreOptions = new List<string>();
            doMoreOptions.Add("yes");
            doMoreOptions.Add("no");
            flow.AddState(ViewStateBuilder.Build("doMore",
                new Ask("doMore", new Prompt("May I assist you with anything else?") { bargein = false }, new Grammar("assistOptions", doMoreOptions))
                {
                    noinputPrompts = doMoreNoinputs,
                    nomatchPrompts = doMoreNomatches
                })
                .AddTransition("continue", "goodbye", new Condition("result == 'no'"))
                .AddTransition("continue", "assist", new Condition("result == 'yes'"))
                .AddTransition("nomatch", "goodbye", null)
                .AddTransition("noinput", "goodbye", null));


            flow.AddState(ViewStateBuilder.Build("goodbye", new Exit("goodbye", "Thank you for using Mayhem. Goodbye")));
            return flow;

        }

    }
}
