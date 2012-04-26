using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel.CallFlow
{
    public class StateAction
    {
        Action<CallFlow, State, Event> action { get; set; }
        StateAction(Action<CallFlow, State, Event> action)
        {
            this.action = action;
        }
    }
}
