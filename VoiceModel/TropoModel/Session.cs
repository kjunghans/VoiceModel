using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel.TropoModel
{
    public class Session
    {
        public string accountId {get; set;}
        public string callId {get; set;}
        public ToFrom from {get; set;}
        public string headers {get; set;}
        public string id {get; set;}
        public string initialText {get; set;}
        public Dictionary<string, string> parameters {get; set;}
        public string timestamp {get; set;}
        public ToFrom to {get; set;}
        public string userType { get; set; } 

    }
}
