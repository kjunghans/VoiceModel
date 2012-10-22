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
        private static TropoCSharp.Tropo.Say AudioToSay(IAudio audio, string json)
        {
            TropoCSharp.Tropo.Say s = new TropoCSharp.Tropo.Say(AudioToSayString(audio, json));
            return s;
        }

        private static string AudioToSayString(IAudio audio, string json)
        {
            string s = "Error converting prompt VoiceModel";
            Type audioType = audio.GetType();
            if (audioType == typeof(TtsMessage))
            {
                s = ((TtsMessage)audio).message;
            }
            else if (audioType == typeof(TtsVariable))
            {
                char[] delims = { '.' };
                string varName = ((TtsVariable)audio).varName.Split(delims)[1];
                var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                try
                {
                    s = values[varName];
                }
                catch
                {
                    s = "Could not find variable " + varName;
                };
            }
            else if (audioType == typeof(Audio))
            {
                s = ((Audio)audio).message + " " + ((Audio)audio).src;
            }
            else if (audioType == typeof(Silence))
            {
                s = "<?xml version='1.0'?><speak><break time='" + ((Silence)audio).msPause.ToString() + "ms' /></speak>";
            }
             return s;
        }


        private static string ConvertExit(Exit model)
        {
            Tropo tmodel = new Tropo();
            ConvertPromptList(model.ExitPrompt, model.json, ref tmodel);
            tmodel.Hangup();

            return tmodel.RenderJSON();
        }

        private static string ConversionError()
        {
            Tropo tmodel = new Tropo();

            tmodel.Say("Error translating VoiceModel to Tropo object.");

            return tmodel.RenderJSON();
        }

        private static void ConvertPrompt(global::VoiceModel.Prompt prompt, string json, ref Tropo tmodel, string sayOnEvent = null)
        {
            foreach (IAudio audio in prompt.audios)
            {

                TropoCSharp.Tropo.Say s = AudioToSay(audio, json);
                tmodel.Say(s);
            }

        }

        private static TropoCSharp.Tropo.Say ConvertPromptList(List<global::VoiceModel.Prompt> prompts, string json)
        {
            TropoCSharp.Tropo.Say say = null;
            string strPromptList = string.Empty;

            foreach (Prompt prompt in prompts)
            {
                foreach (IAudio audio in prompt.audios)
                {
                    strPromptList += AudioToSayString(audio, json);
                }
            }
            say = new TropoCSharp.Tropo.Say(strPromptList);
            return say;
        }

        private static void ConvertPromptList(List<global::VoiceModel.Prompt> prompts, string json, ref Tropo tmodel, string sayOnEvent = null)
        {
            foreach (Prompt prompt in prompts)
            {
                ConvertPrompt(prompt, json, ref tmodel);
            }
        }

        private static string GetGrammarValue(Grammar grammar)
        {
            string gval = string.Empty;
            if (grammar.isBuiltin)
            {
                switch (grammar.builtin.Type)
                {
                    case BuiltinGrammar.GrammarType.digits:
                        string length = "10";
                        if (grammar.builtin.MaxLength > 0)
                            length = grammar.builtin.MaxLength.ToString();
                        else if (grammar.builtin.MinLength > 0)
                            length = grammar.builtin.MinLength.ToString();
                        gval = "[ " + length + " DIGITS]";
                        break;
                    case BuiltinGrammar.GrammarType.boolean:
                        gval = "yes(1, yes), no(2, no)";
                        break;
                }
            }
            else if (grammar.isExternalRef)
            {
                gval = grammar.source;
            }
            else
            {
                gval = GrammarHelper.GrammarRulesToString(grammar);
            }

            return gval;
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
            tmodel.Ask(3, true, ConvertGrammar(model.grammar), null, "result", null, ConvertPromptList(model.initialPrompt, model.json), null);

            return tmodel.RenderJSON();
        }

        private static string ConvertSay(global::VoiceModel.Say model)
        {
            Tropo tmodel = new Tropo();
            ConvertPromptList(model.prompts, model.json, ref tmodel);
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

        public static void TropoResultToEventAndData(Result tResult, out string vEvent, out string vData, out string vErrorMsg)
        {
            vEvent = "continue";
            vData = string.Empty;
            vErrorMsg = string.Empty;
            if (tResult.state == "REJECTING" || tResult.state == "REJECTED" || tResult.state == "DISCONNECTED" || tResult.state == "FAILED")
                vEvent = "error";
            if (tResult.actions != null)
                vData = tResult.actions.value;
            vErrorMsg = tResult.error;
        }

    }
}
