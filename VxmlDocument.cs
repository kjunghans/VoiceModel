using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoiceModel
{
    public class VxmlDocument
    {
        public string id { get; set; }
        public string AppName { get; set; }
        public List<VxmlProperty> properties { get; set; }
        protected string viewName { get; set; }
        public string json { get; set; }
        public string controllerName { get; set; }

        public VxmlDocument()
        {
            this.AppName = null;
            this.properties = new List<VxmlProperty>();
        }

        public VxmlDocument(VxmlDocument doc)
        {
            this.id = doc.id;
            this.AppName = doc.AppName;
            this.properties = doc.properties;
            this.viewName = doc.viewName;
            this.json = doc.json;
            this.controllerName = doc.controllerName;
        }

        public VxmlDocument(string appName)
        {
            this.AppName = appName;
            this.properties = new List<VxmlProperty>();
        }

        public VxmlDocument(string appName, string next)
        {
            this.AppName = appName;
            this.properties = new List<VxmlProperty>();
        }

 

        public string ViewName
        {
            get { return this.viewName; }
        }
    }
}