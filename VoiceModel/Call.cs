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

        public Call(string id, string to, string dialogUri)
        {
            this.id = id;
            this.viewName = "Call";
            this.to = to;
            this.nextUri = dialogUri;
        }

        public Call(string id, string to, string from, string dialogUri)
        {
            this.id = id;
            this.viewName = "Call";
            this.to = to;
            this.from = from;
            this.nextUri = dialogUri;
        }

        public override VoiceModel BuildModel(string jsonArgs)
        {
            this.json = jsonArgs;
            return this;
        }

    }
}
