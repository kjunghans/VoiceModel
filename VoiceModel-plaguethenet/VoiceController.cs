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
        ILoggerService _log = LoggerFactory.GetInstance();
        private static ISessionProvider _sessionProvider = new SessionDataProvider();

        // This was added to provide a simple way to replace the way session data is stored
        // The default provider object uses SessionData, but can be replaced by setting this property.
        // If this property is set to null, the default provider will be used.
        public static ISessionProvider SessionProvider { get { return _sessionProvider ?? (_sessionProvider = new SessionDataProvider()); } set { _sessionProvider = value; } }


        protected virtual CallFlow.CallFlow GetCallFlow(string sessionId)
        {
            //CallFlow.CallFlow flow = SessionData.GetCallFlow(sessionId);
            CallFlow.CallFlow flow = SessionProvider.GetObject(sessionId) as CallFlow.CallFlow;
            if (flow == null)
            {
                flow = BuildCallFlow();
                //SessionData.SetCallFlow(flow, sessionId);
                SetCallFlow(flow, sessionId);
            }
            return flow;
        }

        // For PCI Compliance, in IVR's that accept credit card information
        // We cannot use GET as the HTTP Method, because the URL is quite
        // likely to be logged. Rather than require implementors to override the views
        // to achieve the behavior they are looking for, they can set the method to post
        // here. The default value is the existing behavior. Most Actions will accept
        // either GET or POST, and having implemented a rather large IVR App using this
        // I have never seen post cause a problem. That being said, Treat this as an
        // experimental feature.

        static VoiceController() { SubmitMethod = HttpMethod.Get; }
        public static HttpMethod SubmitMethod { get; set; }

        protected virtual void SetCallFlow(ICallFlow cf, string sessionId)
        {
            cf.SessionId = sessionId;
            cf.RecordedAudioUri = AudioPathUri;
            SessionProvider.StoreObject(sessionId, cf as CallFlow.CallFlow);
            //SessionData.SetCallFlow((CallFlow.CallFlow)cf, sessionId);
        }

        public abstract CallFlow.CallFlow BuildCallFlow();

        public virtual string RecordingPath
        {
            get { return "~/App_Data/recordings"; }
        }

        public virtual string ControllerName
        {
            get { string controllerFullName = this.GetType().Name; return controllerFullName.Replace("Controller", ""); }
        }


        public virtual string VxmlUri
        {
            get { return ControllerUri + "/StateMachine"; }
        }

        public virtual string ControllerUri
        {
            get { return ApplicationUri + ControllerName; }
        }

        public virtual string ApplicationUri
        {
            get
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

        }

        public virtual string AudioPathUri
        {
            get { return ControllerUri + "/Recording/"; }
        }

        public virtual string TropoUri
        {
            get { return ControllerUri + "/Tropo"; }
        }

        public virtual string TropoRecordingUri
        {
            get { return ControllerUri + "/SaveRecordingTropo"; }
        }

        public virtual string VxmlRecordingUri
        {
            get { return ControllerUri + "/SaveRecording"; }
        }

        public void InitVoiceController()
        {
            recordingPath = RecordingPath;
        }

        protected override void Initialize(RequestContext rc)
        {
            base.Initialize(rc);
            InitVoiceController();
        }

        protected virtual bool isJson(string json)
        {
            bool isIt = false;
            if (!string.IsNullOrEmpty(json))
            {
                if (json[0] == '{' || json[0] == '[')
                    isIt = true;
            }
            return isIt;
        }

        protected virtual ActionResult VoiceView(string id, string vEvent, string json, string sessionId)
        {
            CallFlow.CallFlow callFlow = GetCallFlow(sessionId);
            callFlow["Channel"] = "VOICE";

            callFlow.SessionId = sessionId;
            callFlow.FireEvent(vEvent, json);

            VoiceModel doc = callFlow.CurrState.DataModel;
            if (isJson(callFlow.CurrState.jsonArgs))
                doc.json = callFlow.CurrState.jsonArgs;
            if (doc.ViewName == "Call")
                doc.nextUri = doc.nextUri + "/StateMachine";
            else if (doc.ViewName == "Record")
                doc.nextUri = VxmlRecordingUri;
            else
                doc.nextUri = VxmlUri;

            SetCallFlow(callFlow, sessionId);

            return View(doc.ViewName, doc);

        }

        [OutputCache(Duration = 0, NoStore = true, VaryByParam = "*")]
        public virtual ActionResult StateMachine(string vm_id, string vm_event, string vm_result, string vm_sessionid)
        {
            _log.Debug("Recieved VoiceXML request:[" + Request.RawUrl + "]");
            string qstring = "Parameters: ";
            var items = Request.QueryString.AllKeys.SelectMany(Request.QueryString.GetValues, (k, v) => new { key = k, value = v });
            foreach (var item in items)
                qstring += item.key + "=" + item.value + ";";
            _log.Debug(qstring);
            //If the vm_sessionid is null then this is the first time called by the IVR.
            //Get the session ID directly from the IVR query string.
            //TODO: make this configurable for other IVR systems. This works on Voxeo Prophecy.
            // This can now be configured by overriding this method, 
            // but there must be a better way to do it.
            // Do other IVR systems actually use something different? 
            // What do they use? - plaguethenet
            if (string.IsNullOrEmpty(vm_sessionid))
                vm_sessionid = Request.QueryString["session.sessionid"];
            return VoiceView(vm_id, vm_event, vm_result, vm_sessionid);
        }

        [OutputCache(Duration = 0, NoStore = true, VaryByParam = "*")]
        public virtual ActionResult Recording(string id)
        {
            //TODO: Need to return a 404 error if the file does not exist.
            // Done - plaguethenet
            _log.Debug("Requested recording " + id);
            string filename = id;
            string path = Path.Combine(Server.MapPath(recordingPath), filename);
            if (!System.IO.File.Exists(path))
            {
                _log.Debug(string.Format("{0} was not found, returning 404 to requestor.", path));
                return new HttpNotFoundResult();
            }
            return File(path, "audio/wav", Server.UrlEncode(filename));
        }

        [HttpPost]
        public virtual ActionResult SaveRecording(HttpPostedFileBase CallersMessage)
        {
            string sessionId = Request.QueryString["vm_sessionid"] ?? "";
            _log.Debug("vm_session_id=" + sessionId);
            if (CallersMessage != null && CallersMessage.ContentLength > 0)
            {
                // extract only the fielname
                var fileName = sessionId + ".wav";
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath(recordingPath), fileName);
                _log.Debug("Received recording and will save as " + path);
                CallersMessage.SaveAs(path);
            }
            else
            {
                if (CallersMessage == null)
                    _log.Debug("Received request to save recording but it is null.");
                else
                    _log.Debug("Received request to save recording but it is empty.");

            }

            string vm_id = Request.QueryString["vm_id"];
            string vm_event = Request.QueryString["vm_event"];
            string vm_result = "";
            return VoiceView(vm_id, vm_event, vm_result, sessionId);
        }

        [HttpPost]
        public virtual string SaveRecordingTropo(HttpPostedFileBase filename)
        {
            string msg = "Successfully saved recording.";
            if (filename != null && filename.ContentLength > 0)
            {
                string sessionId = Request.QueryString["vm_session_id"] ?? "";
                _log.Debug("vm_session_id=" + sessionId);
                // extract only the fielname
                var fileName = sessionId + ".wav";
                // store the file inside ~/App_Data/recordings folder
                var path = Path.Combine(Server.MapPath(recordingPath), fileName);
                _log.Debug("Received recording and will save as " + path);
                // save the audio file
                filename.SaveAs(path);
            }
            else
            {
                msg = "Recording was null or empty.";
                if (filename == null)
                    _log.Debug("Received request to save recording but it is null.");
                else
                    _log.Debug("Received request to save recording but it is empty.");

            }

            return msg;
        }

        [HttpPost]
        public virtual string Tropo(Result result)
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
            if (doc.ViewName == "Call")
                doc.nextUri = doc.nextUri + "/Tropo";
            else
                doc.nextUri = TropoUri;
            SetCallFlow(callFlow, result.sessionId);
            string recordingUri = TropoRecordingUri + "?vm_session_id=" + result.sessionId;
            string json = TropoUtilities.ConvertVoiceModelToWebApi(doc, recordingUri);
            _log.Debug("Sending Tropo response:[" + json + "]");
            return json;
        }

        [HttpPost]
        public virtual string StartTropo(string id, Session session)
        {
            _log.Debug("Recieved Tropo start request: ", session);
            string vEvent = "";
            string vData = "";
            CallFlow.CallFlow callFlow = BuildCallFlow();
            if (string.IsNullOrEmpty(id))
                id = "unknown";
            callFlow["AppId"] = id;
            callFlow["ANI"] = session.from.id;
            callFlow["Channel"] = session.from.channel;
            callFlow["InitialText"] = session.initialText;

            callFlow.FireEvent(vEvent, vData);

            VoiceModel doc = callFlow.CurrState.DataModel;
            if (isJson(callFlow.CurrState.jsonArgs))
                doc.json = callFlow.CurrState.jsonArgs;
            if (doc.ViewName == "Call")
                doc.nextUri = doc.nextUri + "/Tropo";
            else
                doc.nextUri = TropoUri;
            SetCallFlow(callFlow, session.id);
            string recordingUri = TropoRecordingUri + "?vm_session_id=" + session.id;
            string json = TropoUtilities.ConvertVoiceModelToWebApi(doc, recordingUri);
            _log.Debug("Sending Tropo response:[" + json + "]");
            return json;
        }

    }
}
