using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel.CallFlow
{
    public delegate bool guardCond(string result);

    public class Transition
    {
        public Event Event { get; set; }
        public string Target { get; set; }
        public Condition Cond { get; set; }
        public Transition() { }
        public Transition(Event Event, string Target)
        {
            this.Event = Event;
            this.Target = Target;
            this.Cond = new Condition();
        }

        public Transition(Event Event, string Target, Condition gCond)
        {
            this.Event = Event;
            this.Target = Target;
            this.Cond = gCond;
        }

        public Transition(Event Event, string Target, string condScript)
        {
            this.Event = Event;
            this.Target = Target;
            this.Cond = new Condition(condScript);
        }

    }
}
