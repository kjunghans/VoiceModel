using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoiceModel.CallFlow;

namespace VoiceModel
{
    public class VoiceState : State
    {
        public VoiceState(string Id, string target, VoiceModel vm)
            : base(Id, target)
        {
            this.DataModel = vm;
        }

        public VoiceState(string Id, VoiceModel vm)
            : base(Id)
        {
            this.DataModel = vm;
        }
    }
}
