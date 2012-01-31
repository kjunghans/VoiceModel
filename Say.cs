using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoiceModel
{
    public class Say: VoiceModel
    {
        public List<Prompt> prompts { get; set; }
       

        public Say()
        {
            this.viewName = "Output";
            this.prompts = new List<Prompt>();
        }
        public Say(VoiceModel doc)
           : base(doc)
        {
            
            this.viewName = "Output";
            this.prompts = new List<Prompt>();
        }
        public Say(VoiceModel doc, string id, string altText)
            : base(doc)
        {
            this.viewName = "Output";
            this.id = id;
            this.prompts = new List<Prompt>();
            this.prompts.Add(new Prompt(altText));
        }
        public Say(string id, string altText)
        {
            this.viewName = "Output";
            this.id = id;
            this.prompts = new List<Prompt>();
            this.prompts.Add(new Prompt(altText));
        }
         public Say(string id, Prompt prompt )
        {
            this.viewName = "Output";
            this.id = id;
            this.prompts = new List<Prompt>();
            this.prompts.Add(prompt);
         }
         public Say(VoiceModel doc, string id, Prompt prompt)
             : base(doc)
         {
             this.viewName = "Output";
             this.id = id;
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