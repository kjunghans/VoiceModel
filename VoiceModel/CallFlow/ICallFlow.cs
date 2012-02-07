using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel.CallFlow
{
    public interface ICallFlow
    {
        SessionData SessionMgr { get; set; }

        void AddState(State state);
        void AddStartState(State state);
        bool GetStartState(out State state);
        void FireEvent(string stateId, string sEvent, string data, out string nextStateId, out string nextStateArgs);
    }
}
