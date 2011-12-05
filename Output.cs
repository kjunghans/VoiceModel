using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoiceModel
{
    public class Output: VxmlDocument
    {
        public List<Prompt> prompts { get; set; }
       

        public Output()
        {
            this.viewName = "Output";
            this.prompts = new List<Prompt>();
        }
        public Output(VxmlDocument doc)
           : base(doc)
        {
            
            this.viewName = "Output";
            this.prompts = new List<Prompt>();
        }
        public Output(VxmlDocument doc, string id, string altText)
            : base(doc)
        {
            this.viewName = "Output";
            this.id = id;
            this.prompts = new List<Prompt>();
            this.prompts.Add(new Prompt(altText));
        }
        public Output(string id, string altText)
        {
            this.viewName = "Output";
            this.id = id;
            this.prompts = new List<Prompt>();
            this.prompts.Add(new Prompt(altText));
        }
         public Output(string id, Prompt prompt )
        {
            this.viewName = "Output";
            this.id = id;
            this.prompts = new List<Prompt>();
            this.prompts.Add(prompt);
         }
         public Output(VxmlDocument doc, string id, Prompt prompt)
             : base(doc)
         {
             this.viewName = "Output";
             this.id = id;
             this.prompts = new List<Prompt>();
             this.prompts.Add(prompt);
         }

    }
}