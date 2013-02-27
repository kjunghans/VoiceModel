using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel
{
    public class Call: VoiceModel
    {
        public string to { get; set; }
        public string from { get; set; }

        public override VoiceModel BuildModel(string jsonArgs)
        {
            this.json = jsonArgs;
            return this;
        }

    }
}
