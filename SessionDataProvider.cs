using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel
{
    using CallFlow;

    public class SessionDataProvider : ISessionProvider
    {
        public void StoreObject(string key, object o)
        {

            var flow = o as CallFlow.CallFlow;
            if (flow == null) throw new InvalidOperationException("This class only supports CallFlow based objects");

            SessionData.SetCallFlow(flow, key);
        }

        public object GetObject(string key)
        {
            return SessionData.GetCallFlow(key);
        }
    }
}
