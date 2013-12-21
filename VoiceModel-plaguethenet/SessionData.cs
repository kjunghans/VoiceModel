using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace VoiceModel
{
    public static class SessionData
    {
 
        public static void SetCallFlow(CallFlow.CallFlow cf, string sessionId)
        {
            ObjectCache cache = MemoryCache.Default;
            CacheItemPolicy policy = new CacheItemPolicy();
            //Expires after 5 minutes of none use. TODO: Make this configurable.
            policy.SlidingExpiration = new TimeSpan(0, 5, 0);
            cache.Set(sessionId, cf, policy);

        }

        public static CallFlow.CallFlow GetCallFlow(string sessionId)
        {
            ObjectCache cache = MemoryCache.Default;
            return cache[sessionId] as CallFlow.CallFlow;
        }

    }
}
