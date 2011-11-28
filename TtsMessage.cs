using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoiceModel
{
    public class TtsMessage: IAudio
    {
        private string _message;
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }

        public TtsMessage() { }
        public TtsMessage(string message)
        {
            this.message = message;
        }
        public string Render()
        {
             return message;
        }

    }
}