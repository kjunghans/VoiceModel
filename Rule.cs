using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace VoiceModel
{
    [XmlElement("rule")]
    public class Rule
    {
        enum Scope { isPrivate, isPublic };

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
        public List<OneOf> OneOfList { get; set; }

        public Rule(string Id)
        {
            this.id = Id;
            OneOfList = new List<OneOf>();
        }
    }
}
