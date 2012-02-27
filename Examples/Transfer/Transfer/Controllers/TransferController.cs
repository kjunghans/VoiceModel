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
        public override VoiceModels BuildVoiceModels()
        {
            VoiceModels views = new VoiceModels();

            views.Add(new VoiceModel.Transfer("xfer", "sip:test@127.0.0.1", new Prompt("Please hold while you are transfered to the next available agent." )));
            views.Add(new Exit("xferComplete", "The transfer has been completed."));
            views.Add(new Exit("xferBusy", "That line is busy."));
            views.Add(new Exit("xferNoAnswer", "Sorry, no one answered the phone."));

            return views;

        }

        public override CallFlow BuildCallFlow()
        {
            CallFlow flow = new CallFlow();

            flow.AddStartState(new State("xfer", "xferComplete")
                .AddTransition("busy","xferBusy",null)
                .AddTransition("noanswer","xferNoAnswer",null));
            flow.AddState(new State("xferBusy"));
            flow.AddState(new State("xferNoAnswer"));
            return flow;

        }

    }
}
