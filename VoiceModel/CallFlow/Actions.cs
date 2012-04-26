using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel.CallFlow
{
    public class Actions
    {
        private List<Action<CallFlow, State, Event>> _actionList = new List<Action<CallFlow, State, Event>>();

        public void Add(Action<CallFlow, State, Event> action)
        {
            _actionList.Add(action);
        }

        public void Execute(CallFlow cf, State state, Event e)
        {
            foreach (Action<CallFlow, State, Event> action in _actionList)
                action(cf, state, e);
        }
    }
}
