using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoiceModel;

namespace VoiceModel.TropoModel
{
    public class Convert
    {

        private static Say AudioToSay(IAudio audio)
        {
            Say s = new Say("Error converting prompt VoiceModel");
            switch(audio.GetType().ToString())
            {
                case "VoiceModel.TtsMessage":
                  s = new Say(((TtsMessage)audio).message);
                  break;
            }
            return s;
        }

        private static TropoModel ConvertExit(Exit model)
        {
            TropoModel tmodel = new TropoModel();
            ConvertPromptList(model.ExitPrompt, ref tmodel);

            hangup h = null;
            tmodel.tropo.Add("hangup", h);

            return tmodel;
        }

        //TODO: Build say object that provides error message
        private static TropoModel Error()
        {
             TropoModel tmodel = new TropoModel();

            return tmodel;
        }

        private static void ConvertPrompt(global::VoiceModel.Prompt prompt, ref TropoModel tmodel, string sayOnEvent = null)
        {
            foreach (IAudio audio in prompt.audios)
            {
                tmodel.tropo.Add("say", new global::VoiceModel.TropoModel.say(audio.message));
            }

        }

        private static void ConvertPromptList(List<global::VoiceModel.Prompt> prompts, ref TropoModel tmodel, string sayOnEvent = null)
        {
            foreach (Prompt prompt in prompts)
            {
                ConvertPrompt(prompt, ref tmodel);
            }
        }

       private static TropoModel ConvertAsk(global::VoiceModel.Ask model)
       {
           TropoModel tmodel = new TropoModel();
           ConvertPromptList(model.initialPrompt, ref tmodel);

           return tmodel;
       }

        private static TropoModel ConvertSay(global::VoiceModel.Say model)
        {
            TropoModel tmodel = new TropoModel();
            ConvertPromptList(model.prompts, ref tmodel);
            return tmodel;
        }

        public static TropoModel VoiceToTropo(VoiceModel vmodel)
        {
            TropoModel tmodel = new TropoModel();

            switch(vmodel.GetType().ToString())
            {
                case "VoiceModel.Exit":
                    tmodel = ConvertExit((Exit)vmodel);
                    break;
                case "VoiceModel.Ask":
                    tmodel = ConvertAsk((global::VoiceModel.Ask)vmodel);
                    break;
                case "VoiceModel.Say":
                    tmodel = ConvertSay((global::VoiceModel.Say)vmodel);
                    break;
                default:
                    tmodel = Error();
                    break;

            }

            return tmodel;
        }

        public static void TropResultOrSessionToEventAndData(string result, out string vEvent, out string vData)
        {
            vEvent = string.Empty;
            vData = string.Empty;
            //TODO: translate result to object and get the associated events and data
            //TODO: add log4net to library for logging of data that comes back from Tropo
            if (result != null && result.Contains("result"))
            {
                vEvent = "continue";
            }
        }
    }
}
