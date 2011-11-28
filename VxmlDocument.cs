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
        public Transtions transitions { get; set; }
        protected string viewName { get; set; }
        public string json { get; set; }

        public VxmlDocument()
        {
            this.AppName = null;
            this.properties = new List<VxmlProperty>();
            this.transitions = new Transtions();
        }

        public VxmlDocument(string appName)
        {
            this.AppName = appName;
            this.properties = new List<VxmlProperty>();
            this.transitions = new Transtions();
        }

        public VxmlDocument(string appName, string next)
        {
            this.AppName = appName;
            this.properties = new List<VxmlProperty>();
            this.transitions = new Transtions();
            this.transitions.Add(next);
        }

        public void addTransition(string nextState)
        {
            transitions.Add(nextState);
        }

        public string getNextState()
        {
            return transitions.getNextState(Transition.EventType.continueApp);
        }

        public string ViewName
        {
            get { return this.viewName; }
        }
    }
}