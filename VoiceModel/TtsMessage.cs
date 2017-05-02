using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoiceModel
{
    public class TtsMessage: IAudio
    {
        private string _staticMsg;
        private Var _varMsg;

        public string message
        {
            get 
            {
                if (!string.IsNullOrEmpty(_staticMsg))
                    return _staticMsg;
                else if (_varMsg != null)
                    return _varMsg.Value;
                else
                    return "";
            }

            set
            {
                _staticMsg = value;
                _varMsg = null;
            }
        }

        public TtsMessage(string message)
        {
            _staticMsg = message;
            _varMsg = null;
        }

        public TtsMessage(Var varMsg)
        {
            _varMsg = varMsg;
            _staticMsg = null;
        }

        public string Render()
        {
             return message;
        }

    }
}