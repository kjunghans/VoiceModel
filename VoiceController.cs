using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using VoiceModel.CallFlow;
using System.IO;
using VoiceModel.TropoModel;
using VoiceModel.Logger;

namespace VoiceModel
{
    public abstract class VoiceController : Controller
    {
        string recordingPath { get; set; }
        SessionData sessionMgr = new SessionData("VoiceController");
        ILoggerService _log = LoggerFactory.GetInstance();


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

        private ICallFlow GetCallFlow(string sessionId)
        {
            CallFlow.CallFlow flow = sessionMgr.GetCallFlow(sessionId);
            if (flow == null)
            {
                flow = BuildCallFlow();
                sessionMgr.SetCallFlow(flow, sessionId);
            }
            return flow;
        }

        private void SetCallFlow(ICallFlow cf)
        {
            sessionMgr.SetCallFlow((CallFlow.CallFlow)cf);
        }

        private void SetCallFlow(ICallFlow cf, string sessionId)
        {
            sessionMgr.SetCallFlow((CallFlow.CallFlow)cf, sessionId);
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
            get { return  GetApplicationUri() + ControllerName + "/StateMachine"; }
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

        public string TropoUri
        {
            get { return GetApplicationUri() + ControllerName + "/Tropo"; }
        }

        public string RecordingUri
        {
            get { return GetApplicationUri() + ControllerName + "/SaveRecording"; }
        }

        public string ActionFullPath
        {
            get { return GetApplicationUri() + ActionName; }
        }

        public void InitVoiceController()
        {
            recordingPath = RecordingPath;
            sessionMgr = new SessionData(ControllerName + ".cf");
        }

        protected override void Initialize(RequestContext rc)
        {
            base.Initialize(rc);
            InitVoiceController();
        }

        private bool isJson(string json)
        {
            bool isIt = false;
            if (!string.IsNullOrEmpty(json))
            {
                if (json[0] == '{' || json[0] == '[')
                    isIt = true;
            }
            return isIt;
        }
 
        protected ActionResult VoiceView(string id, string vEvent, string json)
        {
            CallFlow.ICallFlow callFlow = GetCallFlow();
            callFlow.FireEvent(vEvent, json);

            VoiceModel doc = callFlow.CurrState.DataModel;
            if (isJson(callFlow.CurrState.jsonArgs))
                doc.json = callFlow.CurrState.jsonArgs;
            doc.ControllerName = ActionName;
            SetCallFlow(callFlow);

            return View(doc.ViewName, doc);

        }

        [OutputCache(Duration = 0, NoStore = true, VaryByParam = "*")]
        public ActionResult StateMachine(string vm_id, string vm_event, string vm_result)
        {
            _log.Debug("Recieved VoiceXML request:[" + Request.RawUrl + "]");
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
                _log.Debug("Received recording and will save as " + path);
                CallersMessage.SaveAs(path);
            }
            
            string vm_id = Request.QueryString["vm_id"];
            string vm_event = Request.QueryString["vm_event"];
            string vm_result = "";
            return VoiceView(vm_id, vm_event, vm_result);
        }

        [HttpPost]
        public string Tropo(Result result)
        {
            _log.Debug("Recieved Tropo request: ", result);
            string vEvent;
            string vData;
            string vErrMsg;
            TropoUtilities.TropoResultToEventAndData(result, out vEvent, out vData, out vErrMsg);
            CallFlow.ICallFlow callFlow = GetCallFlow(result.sessionId);
            callFlow.FireEvent(vEvent, vData);

            VoiceModel doc = callFlow.CurrState.DataModel;
            if (isJson(callFlow.CurrState.jsonArgs))
                doc.json = callFlow.CurrState.jsonArgs;
            doc.ControllerName = ActionName;
            SetCallFlow(callFlow, result.sessionId);
            string json = TropoUtilities.ConvertVoiceModelToWebApi(doc, TropoUri, RecordingUri);
            _log.Debug("Sending Tropo response:[" + json + "]");
            return json;
        }

        [HttpPost]
        public string StartTropo(Session session)
        {
            _log.Debug("Recieved Tropo start request: ", session );
            string vEvent = "";
            string vData = "";
            CallFlow.ICallFlow callFlow = BuildCallFlow();
            callFlow.FireEvent(vEvent, vData);

            VoiceModel doc = callFlow.CurrState.DataModel;
            if (isJson(callFlow.CurrState.jsonArgs))
                doc.json = callFlow.CurrState.jsonArgs;
            doc.ControllerName = ActionName;
            SetCallFlow(callFlow, session.id);
            string json = TropoUtilities.ConvertVoiceModelToWebApi(doc, TropoUri, RecordingUri);
            _log.Debug("Sending Tropo response:[" + json + "]");
            return json;
        }

     }
}
