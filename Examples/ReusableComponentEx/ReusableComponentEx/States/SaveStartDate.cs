using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ReusableComponentEx.Models;
using VoiceModel.CallFlow;
using VoiceModel;
using GetDateDtmf;

namespace ReusableComponentEx.States
{
    public class SaveStartDate : State
    {
        public SaveStartDate(string Id, string target) : base(Id, target)
        {
        }

        public override void OnEntry()
        {
            GetDateDtmfController getDateComponent = new GetDateDtmfController();
            getDateComponent.InitVoiceController();
            GetDateDtmfOutput output = (GetDateDtmfOutput)getDateComponent.SessionMgr.GetComponentOutput();
            Models.DomainModel model = new DomainModel();
            model.startDate = output.Date;
            Flows.SessionMgr.SetComponentInput(model);
            this.Flows.FireEvent(this.Id, "continue", null);
        }
    }
}