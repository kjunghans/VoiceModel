using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoiceModel.CallFlow;
using GetDateDtmf;
using ReusableComponentEx.Models;

namespace ReusableComponentEx.States
{
    public class SaveFinishDate : State
    {
        public SaveFinishDate(string Id, string target)
            : base(Id, target)
        {
        }

        public override void OnEntry()
        {
            GetDateDtmfController getDateComponent = new GetDateDtmfController();
            getDateComponent.InitVoiceController();
            GetDateDtmfOutput output = (GetDateDtmfOutput)getDateComponent.SessionMgr.GetComponentOutput();
            Models.DomainModel model = (DomainModel)Flows.SessionMgr.GetComponentInput();
            model.finishDate = output.Date;
            Flows.SessionMgr.SetComponentInput(model);
            this.Flows.FireEvent(this.Id, "continue", null);
        }

    }
}