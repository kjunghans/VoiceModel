using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel.TropoModel
{
    public class TAction
    {
        public string name { get; set; }
        public int attempts { get; set; }
        public string disposition { get; set; }
        public int confidence { get; set; }
        public string interpretation { get; set; }
        public string utterrance { get; set; }
        public string value { get; set; }
        public string concept { get; set; }
        public string xml { get; set; }
    }
}
