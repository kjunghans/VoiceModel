using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VoiceModel.CallFlow;

namespace VoiceModel
{
    public class VoiceController : Controller
    {
        IVoiceModels _voiceModels;
        ICallFlow _callFlow;

        public ActionResult VoiceView(string id, string vEvent, string json, IVoiceModelBuilder vmBuilder, ICallFlowBuilder cfBuilder)
        {
            BuildVoiceModels(vmBuilder);
            BuildCallFlows(cfBuilder);

            _callFlow.FireEvent(id, vEvent, json);

            VxmlDocument doc = _voiceModels.Get(_callFlow.CurrStateId, _callFlow.CurrStateArgs);
            return View(doc.ViewName, doc);
        }

        private void BuildVoiceModels(IVoiceModelBuilder vmBuilder)
        {
            _voiceModels = vmBuilder.Build();
        }

        private void BuildCallFlows(ICallFlowBuilder cfBuilder)
        {
            _callFlow = cfBuilder.Build();
        }
    }
}
