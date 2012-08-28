using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel.TropoModel
{
    public class Result
    {
       public List<TAction>  actions {get; set;}
       public string callId {get; set;}
       public bool complete {get; set;}
       public string error {get; set;}
       public int sequence {get; set;}
       public int sessionDuration {get; set;}
       public string sessionId {get; set;}
       public string state { get; set; } 

    }
}
