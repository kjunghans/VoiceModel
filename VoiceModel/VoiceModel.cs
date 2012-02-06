using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoiceModel
{
    public abstract class VoiceModel
    {
        public string id { get; set; }
        public string AppName { get; set; }
        public List<VxmlProperty> properties { get; set; }
        protected string viewName { get; set; }
        public string json { get; set; }
        public bool AllowSettingControllerName { get; set; }
        public string ControllerName { get; set; }

        public  bool ControllerNameHasFullPath
        {
            get { return ControllerName.Contains("http"); }
        }


        public VoiceModel()
        {
            this.AppName = null;
            this.properties = new List<VxmlProperty>();
            this.ControllerName = "StateMachine";
            this.AllowSettingControllerName = true;
        }

        public VoiceModel(VoiceModel doc)
        {
            this.id = doc.id;
            this.AppName = doc.AppName;
            this.properties = doc.properties;
            this.viewName = doc.viewName;
            this.json = doc.json;
            this.ControllerName = doc.ControllerName;
            this.AllowSettingControllerName = true;
        }

        public VoiceModel(string appName)
        {
            this.AppName = appName;
            this.properties = new List<VxmlProperty>();
            this.ControllerName = "StateMachine";
            this.AllowSettingControllerName = true;
        }

        public VoiceModel(string appName, string next)
        {
            this.AppName = appName;
            this.properties = new List<VxmlProperty>();
            this.ControllerName = "StateMachine";
            this.AllowSettingControllerName = true;
        }

        public abstract VoiceModel BuildModel(string jsonArgs);
 
        public string ViewName
        {
            get { return this.viewName; }
        }
    }
}