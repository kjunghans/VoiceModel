using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoiceModel
{
    public class ResourceLocation
    {
        public string url { get; set; }
        public ResourceLocation() { }
        public ResourceLocation(string url)
        {
            this.url = url;
        }
    }
}