using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel.CallFlow
{
    public class Condition
    {
        public string Script { get; set; }

        public Condition()
        {
            Script = null;
        }

        public Condition(string script)
        {
            Script = script;
        }

        public bool isNull()
        {
            return Script == null || Script.Equals(string.Empty);
        }
    }
}
