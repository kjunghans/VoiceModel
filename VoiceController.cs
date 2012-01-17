using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using VoiceModel.CallFlow;
using System.IO;

namespace VoiceModel
{
    public abstract class VoiceController : Controller
    {
        protected IVoiceModels voiceModels {get; set;}
        protected ICallFlow callFlow {get; set;}
        protected string recordingPath { get; set; }
        const string vmCacheId = "record.vm";


        private IVoiceModels GetVoiceModel()
        {
            VoiceModels views = (VoiceModels)HttpRuntime.Cache.Get(vmCacheId);
            if (views == null)
            {
                views = BuildVoiceModels();
                System.Web.HttpContext.Current.Cache.Insert(vmCacheId, views);
            }
            return views;

        }

        public abstract VoiceModels BuildVoiceModels();

        const string cacheId = "record.cf";

        private  ICallFlow GetCallFlow()
        {
            CallFlow.CallFlow flow = (CallFlow.CallFlow)HttpRuntime.Cache.Get(cacheId);
            if (flow == null)
            {
                flow = BuildCallFlow();
                System.Web.HttpContext.Current.Cache.Insert(cacheId, flow);
            }
            return flow;

        }


        public abstract CallFlow.CallFlow BuildCallFlow();

        public  virtual string GetRecordingPath()
        {
            return "~/App_Data/recordings";
        }

        protected override void Initialize(RequestContext rc)
        {
            base.Initialize(rc);
            voiceModels = GetVoiceModel();
            callFlow = GetCallFlow();
            recordingPath = GetRecordingPath();
        }


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
