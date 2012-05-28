using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoiceModel;
using VoiceModel.CallFlow;
using System.Configuration;
using CommandMgr.ServiceMgr;

namespace MayhemVoice.Controllers
{
    public class MayhemController : VoiceController
    {
        public override CallFlow BuildCallFlow()
        {
            CallFlow flow = new CallFlow();
            flow.AddState(ViewStateBuilder.Build("greeting", "assist", new Say("greeting", "This is Mahem.")), true);
            CommandService commService = new CommandService();
            List<string> commands = commService.ListCommandNames();
            flow.AddState(ViewStateBuilder.Build("assist", "goodbye", new Ask("assist", "How may I assist you?", new Grammar("commands", commands))));
            flow.AddState(ViewStateBuilder.Build("goodbye", new Exit("goodbye", "Thank you for using Mayhem. Goodbye")));
            return flow;

        }

    }
}
