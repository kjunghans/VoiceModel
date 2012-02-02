using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoiceModel
{
    public interface IAudio
    {
        string message { get; set; }
        string Render();
    }
}