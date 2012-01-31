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

        public static void SetComponentInput(string componentName, ComponentInput input)
        {
            HttpContext.Current.Session.Add(_componentInputPrefix + componentName, input);
        }

        public static ComponentInput GetComponentInput(string componentName)
        {
            return (ComponentInput)HttpContext.Current.Session[_componentInputPrefix + componentName];
        }

        public static void SetComponentOutput(string componentName, ComponentOutput output)
        {
            HttpContext.Current.Session.Add(_componentOutputPrefix + componentName, output);
        }

        public static ComponentInput GetComponentOutput(string componentName)
        {
            return (ComponentInput)HttpContext.Current.Session[_componentOutputPrefix + componentName];
        }
    }
}
