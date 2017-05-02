using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel
{
    public class BuiltinGrammar
    {
        public enum GrammarType { boolean, date, digits, currency, number, phone, time };
        public GrammarType Type { get; set; }
        public int MinLength { get; set; }
        public int MaxLength { get; set; }

        public BuiltinGrammar(GrammarType gType, int minlength = 0, int maxlength = 0)
        {
            Type = gType;
            MinLength = minlength;
            MaxLength = maxlength;
        }
    }
}
