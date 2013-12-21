﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel
{
    public class Ask : VoiceModel
    {
        public List<Prompt> initialPrompt { get; set; }
        public List<Prompt> nomatchPrompts { get; set; }
        public List<Prompt> noinputPrompts { get; set; }
        public Grammar grammar { get; set; }

        public Ask()
        {
            this.viewName = "Input";
            initialPrompt = new List<Prompt>();
            noinputPrompts = new List<Prompt>();
            nomatchPrompts = new List<Prompt>();
        }

        public Ask(VoiceModel doc)
            : base(doc)
        {
            this.viewName = "Input";
            initialPrompt = new List<Prompt>();
            noinputPrompts = new List<Prompt>();
            nomatchPrompts = new List<Prompt>();
        }

        public Ask(string id)
        {
            this.viewName = "Input";
            initialPrompt = new List<Prompt>();
            noinputPrompts = new List<Prompt>();
            nomatchPrompts = new List<Prompt>();
            this.id = id;
        }

 
        public Ask(string id, string textPrompt, Grammar grammar)
        {
            this.viewName = "Input";
            initialPrompt = new List<Prompt>();
            noinputPrompts = new List<Prompt>();
            nomatchPrompts = new List<Prompt>();
            this.id = id;
            this.initialPrompt.Add(new Prompt(textPrompt));
            this.grammar = grammar;
        }

        public Ask(VoiceModel doc, string id, string textPrompt, Grammar grammar)
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

        public Ask(string id, Prompt prompt, Grammar grammar)
        {
            this.viewName = "Input";
            initialPrompt = new List<Prompt>();
            noinputPrompts = new List<Prompt>();
            nomatchPrompts = new List<Prompt>();
            this.id = id;
            this.initialPrompt.Add(prompt);
            this.grammar = grammar;
        }

        
        public override VoiceModel BuildModel(string jsonArgs)
        {
            this.json = jsonArgs;
            return this;
        }
    }
}
