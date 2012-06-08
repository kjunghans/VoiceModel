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
            flow.AddState(ViewStateBuilder.Build("greeting", "assist", new Say("greeting", "This is Mahem.")), true);
            string apiUrl = ConfigurationManager.AppSettings["commandMgrUrl"];
            CommandMgr.Sdk.CommandMgr commService = new CommandMgr.Sdk.CommandMgr(apiUrl);
            List<Command> commands = commService.ListCommands();
            List<string> commandNames = new List<string>();
            foreach (Command c in commands)
                commandNames.Add(c.Name);
            flow.AddState(ViewStateBuilder.Build("assist", "goodbye", new Ask("assist", "How may I assist you?", new Grammar("commands", commandNames))));
            flow.AddState(ViewStateBuilder.Build("goodbye", new Exit("goodbye", "Thank you for using Mayhem. Goodbye")));
            return flow;

        }

    }
}
