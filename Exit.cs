using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel
{
    public class Exit : VxmlDocument
    {
        public List<Prompt> ExitPrompt { get; set; }
        public Exit()
        {
            this.ExitPrompt = new List<Prompt>();
            this.viewName = "Exit";
        }
        public Exit(string Id)
        {
            this.ExitPrompt = new List<Prompt>();
            this.viewName = "Exit";
            this.id = Id;
        }
        public Exit(string Id, string exitPrompt)
        {
            this.ExitPrompt = new List<Prompt>();
            this.viewName = "Exit";
            this.id = Id;
            this.ExitPrompt.Add(new Prompt(exitPrompt));
        }
        public Exit(string Id, Prompt exitPrompt)
        {
            this.ExitPrompt = new List<Prompt>();
            this.viewName = "Exit";
            this.id = Id;
            this.ExitPrompt.Add(exitPrompt);
        }
    }
}
