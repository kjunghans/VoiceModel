using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronJS;
using IronJS.Hosting;

namespace VoiceModel.CallFlow
{

    public class State
    {
        public string Id { get; set; }
        public CallFlow NestedCF { get; set; }
        private List<Transition> Transitions = new List<Transition>();
        public CSharp.Context Ctx {get; set;}
        public bool isFinal { get; set; }
        public Actions OnEntry { get; set; }
        public Actions OnExit { get; set; }
        public dynamic DataModel { get; set; }

        public virtual string CurrentId
        {
            get { return Id; }
        }

        public State(string Id)
        {
            this.Id = Id;
            this.Ctx = null;
            this.isFinal = false;
            this.OnEntry = new Actions();
            this.OnExit = new Actions();
        }

        public State(string Id, string target)
        {
            this.Id = Id;
            AddTransition("continue", target, null);
            this.Ctx = null;
            this.isFinal = false;
            this.OnEntry = new Actions();
            this.OnExit = new Actions();
        }

        public State(string Id, string sEvent, string target)
        {
            this.Id = Id;
            AddTransition(sEvent, target, null);
            this.Ctx = null;
            this.isFinal = false;
            this.OnEntry = new Actions();
            this.OnExit = new Actions();
        }


        public State AddTransition(string sEvent, string target, Condition gCond)
        {
            Transitions.Add(new Transition(new Event(sEvent), target, gCond ));
            return this;
        }

        private bool EvaluateCond(Condition cond)
        {

            string strResult = Ctx.Execute<string>(cond.Script);
            return strResult.Equals("true");
            
        }

        public string jsonArgs
        {
            get
            {
                string args;
                try
                {
                    args = Ctx.GetGlobalAs<string>("jsonArgs");
                }
                catch
                {
                    args = null;
                }
                return args;
            }
            set { Ctx.SetGlobal<string>("jsonArgs", value); }
        }

        public string getTarget(string sEvent, string result)
        {
            string target = "error";
            Ctx.SetGlobal<string>("result", result);
            Event e = new Event(sEvent);
            foreach (Transition trans in Transitions)
            {
                if (trans.Event.Equals(e))
                {
                    if (trans.Cond == null)
                    {
                        target = trans.Target;
                        break;
                    }
                    else if (EvaluateCond(trans.Cond))
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
