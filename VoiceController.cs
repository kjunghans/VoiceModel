using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VoiceModel.CallFlow;
using System.IO;

namespace VoiceModel
{
    public class VoiceController : Controller
    {
        protected IVoiceModels voiceModels {get; set;}
        protected ICallFlow callFlow {get; set;}
        protected string recordingPath { get; set; }

        protected ActionResult VoiceView(string id, string vEvent, string json)
        {

            string nextStateID;
            string nextStateArgs;
            callFlow.FireEvent(id, vEvent, json, out nextStateID, out nextStateArgs);

            VxmlDocument doc = voiceModels.Get(nextStateID, nextStateArgs);
            return View(doc.ViewName, doc);
        }

        public ActionResult StateMachine(string vm_id, string vm_event, string vm_result)
        {

            return VoiceView(vm_id, vm_event, vm_result);
        }

        [HttpPost]
        public ActionResult SaveRecording(HttpPostedFileBase CallersMessage)
        {
            if (CallersMessage != null && CallersMessage.ContentLength > 0)
            {
                // extract only the fielname
                var fileName = Path.GetFileName(CallersMessage.FileName);
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath(recordingPath), fileName);
                CallersMessage.SaveAs(path);
            }
            
            string vm_id = Request.QueryString["vm_id"];
            string vm_event = Request.QueryString["vm_event"];
            string vm_result = "";
            return VoiceView(vm_id, vm_event, vm_result);
        }

     }
}
