using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoiceModel
{
    public class Prompt
    {
        public bool bargein { get; set; }
        public string language { get; set; }
        public List<IAudio> audios { get; set; }
        public Prompt()
        {
            this.bargein = true;
            this.language = string.Empty;
            this.audios = new List<IAudio>();
        }
        public Prompt(string textMsg)
        {
            this.bargein = true;
            this.language = string.Empty;
            this.audios = new List<IAudio>();
            this.audios.Add(new TtsMessage(textMsg));
        }
        public Prompt(IAudio audio)
        {
            this.bargein = true;
            this.language = string.Empty;
            this.audios = new List<IAudio>();
            this.audios.Add(audio);
        }

    }
}