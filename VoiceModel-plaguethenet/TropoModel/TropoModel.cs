using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel.TropoModel
{
    public class TropoModel
    {
        public Dictionary<string, TropoObject> tropo { get; set; }

        public TropoModel()
        {
            tropo = new Dictionary<string, TropoObject>();
        }
    }
}
