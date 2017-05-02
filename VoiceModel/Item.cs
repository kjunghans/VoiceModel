using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace VoiceModel
{
    public class Item
    {
        [XmlText]
        public string uterance { get; set; }
        [XmlElement]
        public string ruleref { get; set; }
        [XmlElement]
        public string tag { get; set; }
        //[XmlElement("one-of")]
        //OneOf oneof { get; set; }

        public Item()
        {

        }

        public Item(string uterance)
        {
            this.uterance = uterance;
        }
    }
}
