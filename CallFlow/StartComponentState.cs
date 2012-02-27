using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Web;

namespace VoiceModel.CallFlow
{
    public class StartComponentState : State
    {
        private State _componentState;
        private ComponentInput _input;
        private SessionData _sessionMgr;


        public StartComponentState(string id, string target, VoiceController componentController, ComponentInput input) :base(id, target)
        {
            componentController.InitVoiceController();
            _componentState = componentController.GetStartState();
            _input = input;
            _input.ReturnId = id;
            _sessionMgr = new SessionData(componentController.ControllerName);

        }

        public override void OnEntry()
        {
            _sessionMgr.SetComponentInput(_input);
            _componentState.OnEntry();
        }

    }
}
