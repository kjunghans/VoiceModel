using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Web;

namespace VoiceModel.CallFlow
{
    public class StartComponentState : State
    {
        private string _componentViewId;

        public StartComponentState(string id, string target, VoiceController componentController, ComponentInput input) :base(id, target)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            this.jsonArgs = HttpContext.Current.Server.UrlEncode(serializer.Serialize(input));
            _componentViewId = componentController.GetStartState().Id;

        }

        public override string CurrentId
        {
            get
            {
                return _componentViewId;
            }
        }

    }
}
