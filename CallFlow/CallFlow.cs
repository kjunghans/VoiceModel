using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace VoiceModel.CallFlow
{
    public class CallFlow : ICallFlow
    {
        private Dictionary<string,State> _states = new Dictionary<string,State>();
        private State _nextState = null;
        private string _startStateId;

        public void AddState(State state)
        {
            state.Flows = this;
            _states.Add(state.Id,state);
        }

        public void AddStartState(State state)
        {
            _startStateId = state.Id;
            state.Flows = this;
            _states.Add(state.Id, state);

        }
        
        public void FireEvent(string stateId, string sEvent, string result)
        {
            State currState;

            if (stateId == null || stateId == string.Empty)
            {
                if (_states.TryGetValue(_startStateId, out _nextState))
                {
                    _nextState.OnEntry();
                }

            }
            else
            {
                if (_states.TryGetValue(stateId, out currState))
                {
                    currState.OnExit();
                    string targetId = currState.getTarget(sEvent, result);
                    if (_states.TryGetValue(targetId, out _nextState))
                    {
                        _nextState.results = result;
                        _nextState.OnEntry();
                    }
                }
            }
        }

        public string CurrStateId
        {
            get { return _nextState.Id; }
        }

        public string CurrStateArgs
        {
            get { return _nextState.results; }
        }
    }
}
