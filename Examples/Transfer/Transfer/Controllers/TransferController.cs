using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoiceModel;
using VoiceModel.CallFlow;

namespace Transfer.Controllers
{
    public class TransferController : VoiceController
    {
 
        public override CallFlow BuildCallFlow()
        {
            CallFlow flow = new CallFlow();
            flow.AddState(ViewStateBuilder.Build("xfer", "xferComplete", 
                new VoiceModel.Transfer("xfer", "sip:test@127.0.0.1", new Prompt("Please hold while you are transfered to the next available agent.")))
                .AddTransition("busy","xferBusy",null)
                .AddTransition("noanswer","xferNoAnswer",null),true);
            flow.AddState(ViewStateBuilder.Build("xferBusy",new Exit("xferBusy", "That line is busy.")));
            flow.AddState(ViewStateBuilder.Build("xferNoAnswer",new Exit("xferNoAnswer", "Sorry, no one answered the phone.")));
            return flow;
        }

    }
}
