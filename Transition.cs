using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel
{
    public class Transition
    {
        public enum EventType { error, nomatch, noinput, filled, hangup, continueApp, semantic, badfetch };
        public EventType eventType { get; set; }
        public string nextState { get; set; }

        public Transition() 
        { 
            this.eventType = EventType.continueApp;
        }

        public Transition(string nextState)
        {
            this.eventType = EventType.continueApp;
            this.nextState = nextState;
        }
    }

    public class Transtions
    {
        private Dictionary<Transition.EventType, string> _transitions = new Dictionary<Transition.EventType, string>();

        public void Add(string nextState)
        {
            _transitions.Add(Transition.EventType.continueApp, nextState);
        }

        public void Add(Transition.EventType etype, string nextState)
        {
            _transitions.Add(etype, nextState);
        }

        public string getNextState(Transition.EventType etype)
        {
            string nextState;
            if (!_transitions.TryGetValue(etype, out nextState))
                nextState = string.Empty;
            return nextState;
        }

    }
}
