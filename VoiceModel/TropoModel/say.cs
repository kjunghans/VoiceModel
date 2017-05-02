using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel.TropoModel
{
    public class say : TropoObject
    {
        public string value { get; set; }

        public say()
        {
            value = "";
        }

        public say(string textPrompt)
        {
            value = textPrompt;
        }
    }
}
