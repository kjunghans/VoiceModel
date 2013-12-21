using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoiceModel
{
    public interface IAudio
    {
        string message { get; set; }
        //TODO: Get rid of render method. It couples information on the model with the view.
        string Render();
    }
}