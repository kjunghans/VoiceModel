using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel
{
    public interface IVoiceModels
    {
        void Add(VoiceModel doc);
        VoiceModel Get(string id, string json);
    }
}
