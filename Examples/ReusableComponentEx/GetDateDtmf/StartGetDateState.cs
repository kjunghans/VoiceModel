using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoiceModel.CallFlow;
using VoiceModel;

namespace GetDateDtmf
{
    class StartGetDateState : State
    {
        private VoiceController _controller;

        public StartGetDateState(string Id, string target, VoiceController controller) : base(Id, target)
        {
            _controller = controller;
        }

        public override void OnEntry()
        {
            Ask getDate = (Ask) _controller.GetVoiceModel(base.Id,"");
            var input = (GetDateDtmfInput) Flows.SessionMgr.GetComponentInput();
            getDate.initialPrompt.Clear();
            getDate.initialPrompt.Add(input.AskDatePrompt);
        }


    }
}
