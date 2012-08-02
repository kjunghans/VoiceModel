using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TropoCSharp.Structs;
using TropoCSharp.Tropo;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace VoiceModel.TropoModel
{
    public class TropoUtilities
    {
        private static TropoCSharp.Tropo.Say AudioToSay(IAudio audio)
        {
            TropoCSharp.Tropo.Say s = new TropoCSharp.Tropo.Say(AudioToSayString(audio));
            return s;
        }

        private static string AudioToSayString(IAudio audio)
        {
            string s = "Error converting prompt VoiceModel";
            switch (audio.GetType().ToString())
            {
                case "VoiceModel.TtsMessage":
                    s = ((TtsMessage)audio).message;
                    break;
            }
            return s;
        }


        private static string ConvertExit(Exit model)
        {
            Tropo tmodel = new Tropo();
            ConvertPromptList(model.ExitPrompt, ref tmodel);
            tmodel.Hangup();

            return tmodel.RenderJSON();
        }

        private static string ConversionError()
        {
            Tropo tmodel = new Tropo();

            tmodel.Say("Error translating VoiceModel to Tropo object.");

            return tmodel.RenderJSON();
        }

        private static void ConvertPrompt(global::VoiceModel.Prompt prompt, ref Tropo tmodel, string sayOnEvent = null)
        {
            foreach (IAudio audio in prompt.audios)
            {

                TropoCSharp.Tropo.Say s = AudioToSay(audio);
                tmodel.Say(audio.message);
            }

        }

        private static TropoCSharp.Tropo.Say ConvertPromptList(List<global::VoiceModel.Prompt> prompts)
        {
            TropoCSharp.Tropo.Say say = null;
            string strPromptList = string.Empty;

            foreach (Prompt prompt in prompts)
            {
                foreach (IAudio audio in prompt.audios)
                {
                    strPromptList += AudioToSayString(audio);
                }
            }
            say = new TropoCSharp.Tropo.Say(strPromptList);
            return say;
        }

        private static void ConvertPromptList(List<global::VoiceModel.Prompt> prompts, ref Tropo tmodel, string sayOnEvent = null)
        {
            foreach (Prompt prompt in prompts)
            {
                ConvertPrompt(prompt, ref tmodel);
            }
        }

        private static string GetGrammarValue(Grammar grammar)
        {
            return grammar.source;
        }

        private static TropoCSharp.Tropo.Choices ConvertGrammar(Grammar grammar)
        {
            string value = GetGrammarValue(grammar);
            TropoCSharp.Tropo.Choices choices = new TropoCSharp.Tropo.Choices(value);
            return choices;
        }

        private static string ConvertAsk(global::VoiceModel.Ask model)
        {
            Tropo tmodel = new Tropo();
            tmodel.Ask(3, true, ConvertGrammar(model.grammar), null, "result", null, ConvertPromptList(model.initialPrompt), null);

            return tmodel.RenderJSON();
        }

        private static string ConvertSay(global::VoiceModel.Say model)
        {
            Tropo tmodel = new Tropo();
            ConvertPromptList(model.prompts, ref tmodel);
            return tmodel.RenderJSON();
        }

        public static string ConvertVoiceModelToWebApi(VoiceModel vmodel)
        {
            string tropoJson = string.Empty;

            switch (vmodel.GetType().ToString())
            {
                case "VoiceModel.Exit":
                    tropoJson = ConvertExit((Exit)vmodel);
                    break;
                case "VoiceModel.Ask":
                    tropoJson = ConvertAsk((global::VoiceModel.Ask)vmodel);
                    break;
                case "VoiceModel.Say":
                    tropoJson = ConvertSay((global::VoiceModel.Say)vmodel);
                    break;
                default:
                    tropoJson = ConversionError();
                    break;

            }
            return tropoJson;
        }

        public static void TropoResultOrSessionToEventAndData(string json, out string vEvent, out string vData, out string vErrorMsg)
        {
            vEvent = "continue";
            vData = string.Empty;
            vErrorMsg = string.Empty;
            //TODO: translate result to object and get the associated events and data
            //TODO: add log4net to library for logging of data that comes back from Tropo
            if (string.IsNullOrEmpty(json))
            {
                if (json.Contains("result"))
                {
                    try
                    {
                        Result tResult = new Result(json);
                        vEvent = tResult.State;
                        JContainer Actions =  TropoCSharp.Tropo.TropoUtilities.parseActions(tResult.Actions);
                        vData = TropoCSharp.Tropo.TropoUtilities.removeQuotes(Actions["value"].ToString());
                    }
                    catch (Exception ex)
                    {
                        vErrorMsg = ex.Message;
                        vEvent = "error";

                    }
                }
                else
                {
                    try
                    {
                        Session tSession = new Session(json);
                        vEvent = "";
                    }
                    catch (Exception ex)
                    {
                        vErrorMsg = ex.Message;
                        vEvent = "error";

                    }
                }
            }
        }

    }
}
