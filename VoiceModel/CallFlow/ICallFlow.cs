using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel.CallFlow
{
    public interface ICallFlow
    {

        void AddState(State state, bool initialState);
        
        void FireEvent(string sEvent, string data);

        VoiceModel CurrentVoiceModel { get; }

        State CurrState { get; }
        string SessionId { get; set; }
        string RecordedAudioUri { get; set; }
    }
}
