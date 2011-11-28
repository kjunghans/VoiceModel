using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel
{
    public interface IVoiceModels
    {
        void Add(VxmlDocument doc);
        VxmlDocument Get(string id, string json);
    }
}
