using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace VoiceModel.TropoModel
{
    [DataContract]
    public class Say : TropoObject
    {
        [DataMember]
        public string value { get; set; }

        public Say(string textPrompt)
        {
            value = textPrompt;
        }
    }
}
