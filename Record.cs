using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel
{
    public class Record : VxmlDocument
    {
        public List<Prompt> prompts { get; set; }
        public string recordingUrl { get; set; }
        
        public Record()
        {
            this.viewName = "Record";
            this.prompts = new List<Prompt>();
            this.recordingUrl = "SaveRecording";
        }

        public Record(VxmlDocument doc)
           : base(doc)
        {

            this.viewName = "Record";
            this.prompts = new List<Prompt>();
            this.recordingUrl = "SaveRecording";
        }

        public Record(VxmlDocument doc, string id, string altText)
            : base(doc)
        {
            this.viewName = "Record";
            this.id = id;
            this.prompts = new List<Prompt>();
            Prompt p = new Prompt(altText);
            p.bargein = false;
            this.prompts.Add(p);
            this.recordingUrl = "SaveRecording";
        }

        public Record(string id, string altText)
         {
            this.viewName = "Record";
            this.id = id;
            this.prompts = new List<Prompt>();
            Prompt p = new Prompt(altText);
            p.bargein = false;
            this.prompts.Add(p);
            this.recordingUrl = "SaveRecording";
        }

    }
}
