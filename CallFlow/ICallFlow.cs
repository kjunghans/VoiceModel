using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel.CallFlow
{
    public interface ICallFlow
    {
        void AddState(State state);
        void AddStartState(State state);
        void FireEvent(string stateId, string sEvent, string result);
        string CurrStateId { get;  }
        string CurrStateArgs { get;  }
    }
}
