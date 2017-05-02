using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel
{
    public class SayAs : IAudio
    {
        private string _message;
        public string interpretAs { get; set; }
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
        public SayAs(string message, string interpretAs)
        {
            this._message = message;
            this.interpretAs = interpretAs;

        }
        public string Render()
        {
            string vxml = "<say-as interpret-as=\"" + interpretAs + "\">" + message + "</say-as>";

            return vxml;
        }

    }
}
