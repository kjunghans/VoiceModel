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
        public enum CFStatus { Empty, NotStarted, Active, Completed }

        private Dictionary<string,State> _states = new Dictionary<string,State>();
        private State _currState = null;
        private State _startState = null;
        protected CSharp.Context ctx = new CSharp.Context();
        private bool _completedFinalState = false;

        public CSharp.Context Ctx { get { return ctx; } }

        public string SessionId { get; set; }

        public bool CompletedFinalState { get { return _completedFinalState; } }

        public CFStatus Status
        {
            get
            {
                if (_states.Count <= 0)
                    return CFStatus.Empty;
                if (_currState == null)
                    return CFStatus.NotStarted;
                else
                    if (CompletedFinalState)
                        return CFStatus.Completed;
                    else
                        return CFStatus.Active;
                
            }
        }

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

        public CFStatus NestedCFStatus
        {
            get
            {
                if (_currState == null || _currState.NestedCF == null)
                    return CFStatus.Empty;
                else
                    return _currState.NestedCF.Status;
            }
        }

        private void FireEventInNestedCF(string sEvent, string data)
        {
            _currState.NestedCF.FireEvent(sEvent, data);
            //Move the data model and json args from the nested SM to this one for access.
            _currState.DataModel = _currState.NestedCF.CurrState.DataModel;
            _currState.jsonArgs = _currState.NestedCF.CurrState.jsonArgs;
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
                    if (NestedCFStatus == CFStatus.Empty)
                    {
                        _currState.jsonArgs = data;
                        _currState.OnEntry.Execute(this, _currState, new Event(sEvent));
                    }
                    else
                    {
                        FireEventInNestedCF(sEvent, data);
                    }
                 }

            }
            else
            {
                _currState.jsonArgs = data;
                
                if (_currState.isFinal)
                {
                    _currState.OnExit.Execute(this, _currState, new Event(sEvent));
                    _completedFinalState = true;
                }
                else
                {
                    CFStatus status = NestedCFStatus;
                    if (status == CFStatus.Active )
                    {
                        FireEventInNestedCF(sEvent, data);
             
                    }

                    status = NestedCFStatus;
                    if (status == CFStatus.Completed || status == CFStatus.Empty)
                    {
                        if (status == CFStatus.Completed)
                            _currState.NestedCF.Restart();

                        _currState.OnExit.Execute(this, _currState, new Event(sEvent));
                        State nextState;
                        string targetId = _currState.getTarget(sEvent, data);
                        if (_states.TryGetValue(targetId, out nextState))
                        {
                            _currState = nextState;
                            //Get the status of our current state's composite states
                            status = NestedCFStatus;
                            if (status == CFStatus.Empty)
                            {
                                _currState.jsonArgs = data;
                                _currState.OnEntry.Execute(this, _currState, new Event(sEvent));
                            }
                            else
                            {
                                FireEventInNestedCF(sEvent, data);
                            }
                        }
                    }
                }
                
            }
        }

        public VoiceModel CurrentVoiceModel
        {
            get { return ctx.GetGlobalAs<VoiceModel>("VoiceModel"); }
            set { ctx.SetGlobal<VoiceModel>("VoiceModel", value); }
        }

        public string RecordedAudioUri
        {
            get { return ctx.GetGlobalAs<string>("RecordedAudioUri"); }
            set { ctx.SetGlobal<string>("RecordedAudioUri", value); }
        }

        public string this[string index]
        {
            get { return ctx.GetGlobalAs<string>(index); }
            set { ctx.SetGlobal<string>(index, value); }
        }

        public State CurrState
        {
            get { return _currState; }
        }

     }
}
