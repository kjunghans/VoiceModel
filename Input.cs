using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel
{
    public class Input : VxmlDocument
    {
        public List<Prompt> initialPrompt { get; set; }
        public List<Prompt> nomatchPrompts { get; set; }
        public List<Prompt> noinputPrompts { get; set; }
        public Grammar grammar { get; set; }

        public Input()
        {
            this.viewName = "Input";
            initialPrompt = new List<Prompt>();
            noinputPrompts = new List<Prompt>();
            nomatchPrompts = new List<Prompt>();
        }

        public Input(VxmlDocument doc)
            : base(doc)
        {
            this.viewName = "Input";
            initialPrompt = new List<Prompt>();
            noinputPrompts = new List<Prompt>();
            nomatchPrompts = new List<Prompt>();
        }

        public Input(string id)
        {
            this.viewName = "Input";
            initialPrompt = new List<Prompt>();
            noinputPrompts = new List<Prompt>();
            nomatchPrompts = new List<Prompt>();
            this.id = id;
        }

 
        public Input(string id, string textPrompt, Grammar grammar)
        {
            this.viewName = "Input";
            initialPrompt = new List<Prompt>();
            noinputPrompts = new List<Prompt>();
            nomatchPrompts = new List<Prompt>();
            this.id = id;
            this.initialPrompt.Add(new Prompt(textPrompt));
            this.grammar = grammar;
        }

        public Input(VxmlDocument doc, string id, string textPrompt, Grammar grammar)
            : base(doc) 
        {
            this.viewName = "Input";
            initialPrompt = new List<Prompt>();
            noinputPrompts = new List<Prompt>();
            nomatchPrompts = new List<Prompt>();
            this.id = id;
            this.initialPrompt.Add(new Prompt(textPrompt));
            this.grammar = grammar;
        }

        public Input(string id, Prompt prompt, Grammar grammar)
        {
            this.viewName = "Input";
            initialPrompt = new List<Prompt>();
            noinputPrompts = new List<Prompt>();
            nomatchPrompts = new List<Prompt>();
            this.id = id;
            this.initialPrompt.Add(prompt);
            this.grammar = grammar;
        }

    }
}
