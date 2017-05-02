using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel
{
    public class TtsVariable : IAudio
    {
        private string _message;
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
        private string _varName;
        public string varName { get { return _varName; } }

        public TtsVariable(string varName)
        {
            _varName = varName;
        }
        public string Render()
        {
            return "<value expr=\"" + _varName + "\"/>";
        }

    }
}
