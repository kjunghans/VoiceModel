using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoiceModel
{
    public class ResourceLocation
    {
        public Var UrlVar { get; set; }
        public string url { get { return UrlVar.Value; } }
        public ResourceLocation() { }
        public ResourceLocation(Var url)
        {
            this.UrlVar = url;
        }
    }
}