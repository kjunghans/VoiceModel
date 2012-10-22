using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoiceModel
{
    public class Audio : IAudio
    {
        public ResourceLocation location { get; set; }
        public string fileName { get; set; }
        public string src { get { return location + fileName; } }
        private string _message;
        public Audio() { }
        public Audio(ResourceLocation location, string fileName, string message)
        {
            this.location = location;
            this.fileName = fileName;
            this.message = message;
        }

        public string message
        {
            get { return _message; }
            set { _message = value; }
        }

        public string Render()
        {
            string vxml = "<audio src=\"" + location.url + fileName + "\">\n";
            vxml += message + "\n</audio>";

            return vxml;
        }
    }
}