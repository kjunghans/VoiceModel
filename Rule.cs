using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace VoiceModel
{
   
    public class Rule
    {
        public enum Scope { isPrivate, isPublic };

        [XmlAttribute("id")]
        public string id { get; set; }
        [XmlIgnore]
        public Scope scope { get; set; }

        [XmlAttribute("scope")]
        public string strScope 
        { 
            get 
            {
                if (scope == null)
                    return "private";
                else if (scope == Scope.isPrivate)
                    return "private";
                else
                    return "public";
            } 
        }

        //[XmlElement("one-of")]
        //public List<OneOf> OneOfList { get; set; }

        [XmlArray("one-of")]
        [XmlArrayItem("item")]
        public List<Item> OneOfList { get; set; }

        public Rule()
        {
            OneOfList = new List<Item>();
        }

        public Rule(string Id)
        {
            this.id = Id;
            OneOfList = new List<Item>();
        }
    }
}
