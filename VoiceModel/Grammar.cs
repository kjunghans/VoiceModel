﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace VoiceModel
{
    [XmlRoot("grammar")]
    public class Grammar
    {
        
        public enum Mode { voice, dtmf };
        public enum GrammarType { xml, gsl };

        [XmlAttribute("src")]
        public string source { get; set; }
        [XmlAttribute("type")]
        public string type 
        { 
            get 
            {
                if (gType == GrammarType.gsl)
                    return "text/gsl";
                else
                    return "application/grammar-xml";
            } 
        }

        [XmlIgnore]
        public bool isBuiltin { get { return (!string.IsNullOrEmpty(_builtin)); } }
        [XmlIgnore]
        public bool isExternalRef { get { return (!string.IsNullOrEmpty(source)); } }
        [XmlAttribute("xml:lang")]
        public string language { get; set; }
        [XmlAttribute("mode")]
        public Mode mode { get; set; }
        [XmlIgnore]
        public GrammarType gType { get; set; }
        [XmlAttribute("root")]
        public string root { get; set; }
        [XmlIgnore]
        string _builtin;
        [XmlIgnore]
        public string builtin { get { return _builtin; } }

        [XmlElement("rule")]
        public Rule rule { get; set; }

        private void setDefaults()
        {
            this.gType = GrammarType.xml;
            this.mode = Mode.voice;
            this.language = "en-US";
        }

        public Grammar()
        {
            setDefaults();
        }

        public Grammar(string builtinGrammar)
        {
            _builtin = builtinGrammar;
        }

        public Grammar(ResourceLocation loc, string source)
        {
            setDefaults();
            this.source = loc.url + source;
        }

        public Grammar(string Id, List<string> utternances)
        {
            setDefaults();
            root = Id;
            rule = new Rule(Id);
            foreach(string utterance in utternances)
                rule.OneOfList.Add(new Item(utterance));
        }


    }
}
