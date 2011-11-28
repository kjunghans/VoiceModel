using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel
{
    public class Grammar
    {
        public string source { get; set; }
        public string type { get; set; }
        public string inline { get; set; }
        public Grammar(string type)
        {
            this.type = type;
        }
        public Grammar(string type, ResourceLocation loc, string source)
        {
            this.type = type;
            this.source = loc.url + source;
        }
        public Grammar(string type, string inline)
        {
            this.type = type;
            this.inline = inline;
        }
    }
}
