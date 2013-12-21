using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoiceModel.CallFlow;

namespace VoiceModel
{
    public class ViewStateBuilder
    {
        public static State Build(string Id, string target, VoiceModel vm)
        {
            State vState = new State(Id, target);
            vState.DataModel = vm;
            return vState;
        }

        public static State Build(string Id, VoiceModel vm)
        {
            State vState = new State(Id);
            vState.DataModel = vm;
            return vState;
        }

    }
}
