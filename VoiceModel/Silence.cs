using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel
{
    public class Silence : IAudio
    {
        private string _message;
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }

        private int _msPause;
        public int msPause { get { return _msPause; } }

        public Silence(int msPause)
        {
            _msPause = msPause;
        }

        public string Render()
        {
            return "<break time=\"" + _msPause.ToString() + "\"/>";
        }

    }
}
