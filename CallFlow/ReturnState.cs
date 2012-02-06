using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoiceModel.CallFlow;

namespace VoiceModel.CallFlow
{
    class ReturnState : State
    {
        public ReturnState(string Id, string target) : base(Id, target)
        {
        }

        public override void OnEntry()
        {
            base.OnEntry();
        }
    }
}
