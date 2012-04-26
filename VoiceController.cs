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
        //ICallFlow callFlow {get; set;}
        string recordingPath { get; set; }
        SessionData sessionMgr = new SessionData("VoiceController");


        //private  ICallFlow GetCallFlow()
        //{
        //    CallFlow.CallFlow flow = (CallFlow.CallFlow)HttpRuntime.Cache.Get(CfCacheId);
        //    if (flow == null)
        //    {
        //        flow = BuildCallFlow();
        //        System.Web.HttpContext.Current.Cache.Insert(CfCacheId, flow);
        //    }
        //    return flow;

        //}

        private ICallFlow GetCallFlow()
        {
            CallFlow.CallFlow flow = sessionMgr.GetCallFlow();
            if (flow == null)
            {
                flow = BuildCallFlow();
                sessionMgr.SetCallFlow(flow);
            }
            return flow;
        }

        private void SetCallFlow(ICallFlow cf)
        {
            sessionMgr.SetCallFlow((CallFlow.CallFlow)cf);
        }

        public abstract CallFlow.CallFlow BuildCallFlow();

        public  virtual string RecordingPath
        {
            get { return "~/App_Data/recordings"; }
        }

        public virtual string ControllerName
        {
            get { string controllerFullName = this.GetType().Name; return controllerFullName.Replace("Controller", ""); }
        }

 
        public string ActionName
        {
            get { return ControllerName + "/StateMachine"; }
        }

        public string GetApplicationUri()
        {
            string fpath = string.Empty;
            if (Request != null)
            {
                fpath = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath;
                if (!fpath.EndsWith("/"))
                    fpath += "/";
            }
            return fpath;

        }

        public string ActionFullPath
        {
            get { return GetApplicationUri() + ActionName; }
        }

        public void InitVoiceController()
        {
            recordingPath = RecordingPath;
            //callFlow = GetCallFlow();
            sessionMgr = new SessionData(ControllerName + ".cf");
        }

        protected override void Initialize(RequestContext rc)
        {
            base.Initialize(rc);
            InitVoiceController();
        }

 
        // protected ActionResult VoiceView(string id, string vEvent, string json)
        //{

        //    callFlow.FireEvent(vEvent, json);

        //    VoiceModel doc = callFlow.CurrState.DataModel;
        //    doc.json = callFlow.CurrState.jsonArgs;
        //    doc.ControllerName = ActionName;
        //    //if (doc.AllowSettingControllerName)
        //    //{
        //    //    doc.ControllerName = ActionFullPath;
        //    //}
        //    //else
        //    //{
        //    //    if (!doc.ControllerNameHasFullPath)
        //    //        doc.ControllerName = GetApplicationUri() + doc.ControllerName;

        //    //}
        //    return View(doc.ViewName, doc);
        //}


        protected ActionResult VoiceView(string id, string vEvent, string json)
        {
            CallFlow.ICallFlow callFlow = GetCallFlow();
            callFlow.FireEvent(vEvent, json);

            VoiceModel doc = callFlow.CurrState.DataModel;
            doc.json = callFlow.CurrState.jsonArgs;
            doc.ControllerName = ActionName;
            SetCallFlow(callFlow);

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
