using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace VoiceModel
{
    public class SessionData
    {
        private string _componentName;

        public SessionData(string componentName)
        {
            _componentName = componentName;
        }

        public void SetCallFlow(CallFlow.CallFlow cf)
        {
            HttpContext.Current.Session.Add( _componentName, cf);
        }

        public CallFlow.CallFlow GetCallFlow()
        {
            return (CallFlow.CallFlow)HttpContext.Current.Session[_componentName];
        }

     }
}
