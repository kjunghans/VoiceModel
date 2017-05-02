using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace VoiceModel.TropoModel
{
    [DataContract]
    public class SayOnEvent : TropoObject
    {
        [DataMember]
        public string value { get; set; }
        [DataMember(Name = "event")]
        public string Event { get; set; }

  
        public SayOnEvent(string textPrompt, string Event)
        {
            value = textPrompt;
            this.Event = Event;
        }
    }
}
