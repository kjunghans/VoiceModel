using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoiceModel;
using VoiceModel.CallFlow;
using System.Configuration;

namespace Transfer.Controllers
{
    public class TransferController : VoiceController
    {
 
        public override CallFlow BuildCallFlow()
        {
            CallFlow flow = new CallFlow();
            //Get the transfer number from the web.confg
            string xferNum = ConfigurationManager.AppSettings["xferNum"];
            flow.AddState(ViewStateBuilder.Build("xfer", "xferComplete", 
                new VoiceModel.Transfer("xfer", xferNum, new Prompt("Please hold while you are transfered to the next available agent.")))
                .AddTransition("busy","xferBusy",null)
                .AddTransition("noanswer","xferNoAnswer",null),true);
            flow.AddState(ViewStateBuilder.Build("xferBusy",new Exit("xferBusy", "That line is busy.")));
            flow.AddState(ViewStateBuilder.Build("xferNoAnswer",new Exit("xferNoAnswer", "Sorry, no one answered the phone.")));
            return flow;
        }

    }
}
