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
        public Output(string id, string altText)
        {
            this.viewName = "Output";
            this.id = id;
            this.prompts = new List<Prompt>();
            this.prompts.Add(new Prompt(altText));
        }
        public Output(string id, string altText, string appName)
        {
            this.viewName = "Output";
            this.id = id;
            this.prompts = new List<Prompt>();
            this.prompts.Add(new Prompt(altText));
            base.AppName = appName;
        }
        public Output(string id, string altText, string appName, List<VxmlProperty> properties)
        {
            this.viewName = "Output";
            this.id = id;
            this.prompts = new List<Prompt>();
            this.prompts.Add(new Prompt(altText));
            base.AppName = appName;
            base.properties = properties;
        }
        public Output(string id, Prompt prompt, string appName, List<VxmlProperty> properties)
        {
            this.viewName = "Output";
            this.id = id;
            this.prompts = new List<Prompt>();
            this.prompts.Add(prompt);
            base.AppName = appName;
            base.properties = properties;
        }
        public Output(string id, Prompt prompt )
        {
            this.viewName = "Output";
            this.id = id;
            this.prompts = new List<Prompt>();
            this.prompts.Add(prompt);
         }

    }
}