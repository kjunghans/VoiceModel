using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Web;

namespace VoiceModel
{
    public class Component : VoiceModel
    {
        private VoiceModel componentView;
        public Component(string id, VoiceController componentController, ComponentInput input)
        {
            this.id = id;
            componentController.InitVoiceController();
            string viewId = componentController.GetStartState().Id;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonInput = serializer.Serialize(input);
            componentView = componentController.GetVoiceModel(viewId, jsonInput);
            componentView.ControllerName = componentController.GetActionName(componentController);
            componentView.AllowSettingControllerName = false;
        }

        public override VoiceModel BuildModel(string jsonArgs)
        {
            return componentView;
        }

    }
}
