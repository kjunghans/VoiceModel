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

        public Call(string id, string to)
        {
            this.id = id;
            this.viewName = "Call";
            this.to = to;
        }

        public Call(string id, string to, string from)
        {
            this.id = id;
            this.viewName = "Call";
            this.to = to;
            this.from = from;
        }

        public override VoiceModel BuildModel(string jsonArgs)
        {
            this.json = jsonArgs;
            return this;
        }

    }
}
