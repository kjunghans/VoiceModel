using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel.CallFlow
{
    public delegate bool guardCond(string result);

    public class Transition
    {
        public string Event { get; set; }
        public string Target { get; set; }
        public guardCond GuardCond { get; set; }
        public Transition() { }
        public Transition(string Event, string Target)
        {
            this.Event = Event;
            this.Target = Target;
            this.GuardCond = null;
        }

        public Transition(string Event, string Target, guardCond gCond)
        {
            this.Event = Event;
            this.Target = Target;
            this.GuardCond = gCond;
        }
        
    }
}
