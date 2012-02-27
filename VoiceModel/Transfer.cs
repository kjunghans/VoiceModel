using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel
{
    public class Transfer: VoiceModel
    {
        public List<Prompt> prompts { get; set; }
        public string destination { get; set; }
        public bool bridge { get; set; }

        public Transfer(string id, string destination, Prompt prompt)
        {
            this.id = id;
            this.viewName = "Transfer";
            this.destination = destination;
            this.bridge = true;
            this.prompts = new List<Prompt>();
            this.prompts.Add(prompt);

        }

        public override VoiceModel BuildModel(string jsonArgs)
        {
            this.json = jsonArgs;
            return this;
        }

    }
}
