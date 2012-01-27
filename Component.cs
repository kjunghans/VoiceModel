using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace VoiceModel
{
    public class Component : VxmlDocument
    {
        private string jsonInput;

        public Component(string id, string name, ComponentInput input)
        {
            this.viewName = "Component";
            this.id = id;
            this.controllerName = name;
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            this.jsonInput = serializer.Serialize(input);
        }
 
    }
}
