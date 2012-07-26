using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel.TropoModel
{
    public class Convert
    {

        private static say AudioToSay(IAudio audio)
        {
            say s = new say("Error converting prompt VoiceModel");
            switch(audio.GetType().ToString())
            {
                case "VoiceModel.TtsMessage":
                  s = new say(((TtsMessage)audio).message);
                  break;
            }
            return s;
        }

        private static TropoModel Exit(Exit model)
        {
            TropoModel tmodel = new TropoModel();

            foreach (Prompt prompt in model.ExitPrompt)
            {
                foreach (IAudio audio in prompt.audios)
                {

                    say s = AudioToSay(audio);
                    tmodel.tropo.Add("say", s);
                }
            }
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

        public static TropoModel VoiceToTropo(VoiceModel vmodel)
        {
            TropoModel tmodel = new TropoModel();

            switch(vmodel.GetType().ToString())
            {
                case "VoiceModel.Exit":
                    tmodel = Exit((Exit)vmodel);
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
