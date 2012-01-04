﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel
{
    public class Record : VxmlDocument
    {
        public List<Prompt> prompts { get; set; }
        public string recordingUrl { get; set; }
        public bool confirm { get; set; }
        public List<Prompt> confirmationPrompts { get; set; }
        public int maxtime { get; set; }

        public string maxtimeStr
        {
            get {return maxtime.ToString() + "s";}
        }
        
        public Record()
        {
            this.confirm = true;
            this.confirmationPrompts = new List<Prompt>();
            Prompt cprompt = new Prompt("I heard you say ");
            cprompt.audios.Add(new TtsVariable("CallersMessage"));
            cprompt.audios.Add(new Silence(1000));
            cprompt.audios.Add(new TtsMessage("To save this message say yes. To discard it say no."));
            this.confirmationPrompts.Add(cprompt);
            this.viewName = "Record";
            this.prompts = new List<Prompt>();
            this.recordingUrl = "SaveRecording";
            this.maxtime = 60;
        }

        public Record(VxmlDocument doc)
           : base(doc)
        {

            this.confirm = true;
            this.confirmationPrompts = new List<Prompt>();
            Prompt cprompt = new Prompt("I heard you say ");
            cprompt.audios.Add(new TtsVariable("CallersMessage"));
            cprompt.audios.Add(new Silence(1000));
            cprompt.audios.Add(new TtsMessage("To save this message say yes. To discard it say no."));
            this.confirmationPrompts.Add(cprompt);
            this.viewName = "Record";
            this.prompts = new List<Prompt>();
            this.recordingUrl = "SaveRecording";
            this.maxtime = 60;
        }

        public Record(VxmlDocument doc, string id, string altText)
            : base(doc)
        {
            this.confirm = true;
            this.confirmationPrompts = new List<Prompt>();
            Prompt cprompt = new Prompt("I heard you say ");
            cprompt.audios.Add(new TtsVariable("CallersMessage"));
            cprompt.audios.Add(new Silence(1000));
            cprompt.audios.Add(new TtsMessage("To save this message say yes. To discard it say no."));
            this.confirmationPrompts.Add(cprompt);
            this.viewName = "Record";
            this.id = id;
            this.prompts = new List<Prompt>();
            Prompt p = new Prompt(altText);
            p.bargein = false;
            this.prompts.Add(p);
            this.recordingUrl = "SaveRecording";
            this.maxtime = 60;
        }

        public Record(string id, string altText)
        {
            this.confirm = true;
            this.confirmationPrompts = new List<Prompt>();
            Prompt cprompt = new Prompt("I heard you say ");
            cprompt.audios.Add(new TtsVariable("CallersMessage"));
            cprompt.audios.Add(new Silence(1000));
            cprompt.audios.Add(new TtsMessage("To save this message say yes. To discard it say no."));
            this.confirmationPrompts.Add(cprompt);
            this.viewName = "Record";
            this.id = id;
            this.prompts = new List<Prompt>();
            Prompt p = new Prompt(altText);
            p.bargein = false;
            this.prompts.Add(p);
            this.recordingUrl = "SaveRecording";
            this.maxtime = 60;
        }

    }
}
