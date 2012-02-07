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
        IVoiceModels voiceModels {get; set;}
        ICallFlow callFlow {get; set;}
        string recordingPath { get; set; }
        string vmCacheId;
        string CfCacheId;

        public SessionData SessionMgr
        {
            get { return callFlow.SessionMgr; }
        }

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


        private  ICallFlow GetCallFlow()
        {
            CallFlow.CallFlow flow = (CallFlow.CallFlow)HttpRuntime.Cache.Get(CfCacheId);
            if (flow == null)
            {
                flow = BuildCallFlow();
                System.Web.HttpContext.Current.Cache.Insert(CfCacheId, flow);
            }
            return flow;

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

        //public string GetControllerName(VoiceController vc)
        //{
        //    string controllerFullName = vc.GetType().Name;
        //    return controllerFullName.Replace("Controller", "");
        //}

        public string ActionName
        {
            get { return ControllerName + "/StateMachine"; }
        }

        public string GetApplicationUri()
        {
            string fpath = string.Empty;
            if (Request != null)
                fpath = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath;
            return fpath;

        }

        public string ActionFullPath
        {
            get { return GetApplicationUri() + ActionName; }
        }

        public void InitVoiceController()
        {
            recordingPath = RecordingPath;
            vmCacheId = ControllerName + ".vmid";
            CfCacheId = ControllerName + ".cfid";
            voiceModels = GetVoiceModel();
            callFlow = GetCallFlow();
            callFlow.SessionMgr = new SessionData(ControllerName);

        }

        protected override void Initialize(RequestContext rc)
        {
            base.Initialize(rc);
            InitVoiceController();
        }

        public State GetStartState()
        {
            State state = null;
            callFlow.GetStartState(out state);
            return state;
        }

        public VoiceModel GetVoiceModel(string id, string args)
        {
            return voiceModels.Get(id, args);
        }

        protected ActionResult VoiceView(string id, string vEvent, string json)
        {

            string viewId;
            string nextStateArgs;
            callFlow.FireEvent(id, vEvent, json, out viewId, out nextStateArgs);

            VoiceModel doc = GetVoiceModel(viewId, nextStateArgs);
            if (doc.AllowSettingControllerName)
            {
                doc.ControllerName = ActionFullPath;
            }
            else
            {
                if (!doc.ControllerNameHasFullPath)
                    doc.ControllerName = GetApplicationUri() + doc.ControllerName;

            }
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
