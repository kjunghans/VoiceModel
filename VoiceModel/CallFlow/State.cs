using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel.CallFlow
{
    public class State
    {
        public string Id { get; set; }
        public CallFlow Flows { get; set; }
        private List<Transition> Transitions = new List<Transition>();
        public string jsonArgs { get; set; }
        public virtual string CurrentId
        {
            get { return Id; }
        }

        public State(string Id)
        {
            this.Id = Id;
        }
        public State(string Id, string target)
        {
            this.Id = Id;
            AddTransition("continue", target, null);
        }
        public State(string Id, string sEvent, string target)
        {
            this.Id = Id;
            AddTransition(sEvent, target, null);
        }

        public virtual void OnEntry() { }
        public virtual void OnExit() { }

        public State AddTransition(string sEvent, string target, guardCond gCond)
        {
            Transitions.Add(new Transition(sEvent, target, gCond ));
            return this;
        }

        public string getTarget(string sEvent, string result)
        {
            string target = "error";
            foreach (Transition trans in Transitions)
            {
                if (trans.Event.Equals(sEvent))
                {
                    if (trans.GuardCond == null)
                    {
                        target = trans.Target;
                        break;
                    }
                    else if (trans.GuardCond(result))
                    {
                        target = trans.Target;
                        break;
                    }
                }
            }

            return target;
        }
    }
}
