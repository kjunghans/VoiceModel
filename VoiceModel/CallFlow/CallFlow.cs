using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using IronJS.Hosting;

namespace VoiceModel.CallFlow
{

    public class CallFlow : ICallFlow
    {
        private Dictionary<string,State> _states = new Dictionary<string,State>();
        private State _currState = null;
        private State _startState = null;
        protected CSharp.Context ctx = new CSharp.Context();
        private bool _completedFinalState = false;

        public CSharp.Context Ctx { get { return ctx; } }

        public bool CompletedFinalState { get { return _completedFinalState; } }

        public void Restart()
        {
            _currState = null;
            _completedFinalState = false;
        }

        public void AddState(State state, bool initialState = false)
        {
            state.Ctx = this.ctx;
            _states.Add(state.Id,state);
            if (initialState)
                _startState = state;
        }

        public void FireEvent(string sEvent, string data)
        {
 
            //This means the state machine is just starting
            //so put it in the start state
            if (_currState == null)
            {
                if (_startState != null)
                {
                    _currState = _startState;
                    if (!_currState.DoingNestedStates(sEvent, data))
                    {
                        _currState.jsonArgs = data;
                        _currState.OnEntry.Execute(this, _currState, new Event(sEvent));
                    }
                 }

            }
            else
            {
                
                _currState.OnExit.Execute(this, _currState, new Event(sEvent));
                if (_currState.isFinal)
                {
                    _completedFinalState = true;
                }
                else
                {
                    State nextState;
                    string targetId = _currState.getTarget(sEvent, data);
                    if (_states.TryGetValue(targetId, out nextState))
                    {
                        _currState = nextState;
                        if (!_currState.DoingNestedStates(sEvent, data))
                        {
                            _currState.jsonArgs = data;
                            _currState.OnEntry.Execute(this, _currState, new Event(sEvent));
                        }
                    }
                }
                
            }
        }

        public VoiceModel GetCurrentVoiceModel()
        {
            return ctx.GetGlobalAs<VoiceModel>("VoiceModel");
        }

        public void SetCurrentVoiceModel(VoiceModel vm)
        {
            ctx.SetGlobal<VoiceModel>("VoiceModel", vm);
        }

        public State CurrState
        {
            get { return _currState; }
        }

     }
}
