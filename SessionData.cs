using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace VoiceModel
{
    public class SessionData
    {
        private const string _componentInputPrefix = "vmc.input.";
        private const string _componentOutputPrefix = "vmc.output.";
        private string _componentName;

        public SessionData(string componentName)
        {
            _componentName = componentName;
        }

        public void SetComponentInput(ComponentInput input)
        {
            HttpContext.Current.Session.Add(_componentInputPrefix + _componentName, input);
        }

        public ComponentInput GetComponentInput()
        {
            return (ComponentInput)HttpContext.Current.Session[_componentInputPrefix + _componentName];
        }

        public void SetComponentOutput(ComponentOutput output)
        {
            HttpContext.Current.Session.Add(_componentOutputPrefix + _componentName, output);
        }

        public ComponentInput GetComponentOutput()
        {
            return (ComponentInput)HttpContext.Current.Session[_componentOutputPrefix + _componentName];
        }
    }
}
