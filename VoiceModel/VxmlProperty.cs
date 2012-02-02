using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoiceModel
{
    public class VxmlProperty
    {
        public string name { get; set; }
        public string value { get; set; }
        public VxmlProperty() { }
        public VxmlProperty(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
    }
}