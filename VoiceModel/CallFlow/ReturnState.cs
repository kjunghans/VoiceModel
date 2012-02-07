using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoiceModel.CallFlow;

namespace VoiceModel.CallFlow
{
    public class ReturnState : State
    {
        VoiceController _controller;

        public ReturnState(string Id, VoiceController controller) : base(Id)
        {
            _controller = controller;
        }

        public override void OnEntry()
        {
            ComponentInput input = _controller.SessionMgr.GetComponentInput();
            VoiceModel vm = _controller.GetVoiceModel(this.Id, "");
            vm.ControllerName = input.ReturnAction;
            vm.AllowSettingControllerName = false;
            vm.id = input.ReturnId;
        }
    }
}
