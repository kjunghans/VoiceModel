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
        protected IVoiceModels voiceModels {get; set;}
        protected ICallFlow callFlow {get; set;}

        public ActionResult VoiceView(string id, string vEvent, string json)
        {

            string nextStateID;
            string nextStateArgs;
            callFlow.FireEvent(id, vEvent, json, out nextStateID, out nextStateArgs);

            VxmlDocument doc = voiceModels.Get(nextStateID, nextStateArgs);
            return View(doc.ViewName, doc);
        }

     }
}
