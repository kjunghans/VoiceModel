using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoiceModel.CallFlow;
using GetDateDtmf;
using ReusableComponentEx.Models;
using System.Web.Script.Serialization;

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
            string daysDiff = output.Date.Subtract(model.startDate).Days.ToString();
            var d = new { daysDiff = daysDiff };
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(d);
            this.Flows.FireEvent(this.Id, "continue", json);
        }

    }
}