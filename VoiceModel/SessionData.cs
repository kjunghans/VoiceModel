using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace VoiceModel
{
    public class SessionData
    {
        private string _componentName;
        private string _cacheKey;

        private Dictionary<string, CallFlow.CallFlow> GetCachedCallFlows()
        {
            Dictionary<string, CallFlow.CallFlow> callFlows = (Dictionary<string, CallFlow.CallFlow>)HttpContext.Current.Cache[_cacheKey];
            if (callFlows == null)
                callFlows = new Dictionary<string, CallFlow.CallFlow>();
            return callFlows;

        }

        private void UpdateCachedCallFlows(Dictionary<string, CallFlow.CallFlow> callFlows)
        {
            HttpContext.Current.Cache.Insert(_cacheKey, callFlows);
        }

        public SessionData(string componentName)
        {
            _componentName = componentName;
            _cacheKey = "cache." + componentName;
        }

        public void SetCallFlow(CallFlow.CallFlow cf)
        {
            HttpContext.Current.Session.Add( _componentName, cf);
        }

        public CallFlow.CallFlow GetCallFlow()
        {
            return (CallFlow.CallFlow)HttpContext.Current.Session[_componentName];
        }

        public void SetCallFlow(CallFlow.CallFlow cf, string sessionId)
        {
            Dictionary<string, CallFlow.CallFlow> callFlows = GetCachedCallFlows();
            CallFlow.CallFlow callFlow;
            if (callFlows.TryGetValue(sessionId, out callFlow))
                callFlow = cf;
            else
                callFlows.Add(sessionId, cf);
            UpdateCachedCallFlows(callFlows);

        }

        public CallFlow.CallFlow GetCallFlow(string sessionId)
        {
            Dictionary<string, CallFlow.CallFlow> callFlows = GetCachedCallFlows();
            CallFlow.CallFlow callFlow = null;
            callFlows.TryGetValue(sessionId, out callFlow);
            return callFlow;
        }

    }
}
