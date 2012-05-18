using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel
{
    public class Item
    {
        string uterance { get; set; }
        string ruleref { get; set; }
        string tag { get; set; }
        OneOf oneof { get; set; }

        public Item(string uterance)
        {
            this.uterance = uterance;
        }
    }
}
