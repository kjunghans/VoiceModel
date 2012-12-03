using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoiceModel
{
    public class Audio : IAudio
    {
        public ResourceLocation location { get; set; }
        public Var fileName { get; set; }
        public string src { get { return location.url + fileName.Value; } }
        private string _message;
        public Audio() { }
        public Audio(ResourceLocation location, Var fileName, string message)
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
            string vxml = "<audio src=\"" + src + "\">\n";
            vxml += message + "\n</audio>";

            return vxml;
        }
    }
}