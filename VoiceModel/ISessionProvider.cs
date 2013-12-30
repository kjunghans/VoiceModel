using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel
{
    using TropoCSharp.Structs;

    public interface ISessionProvider
    {
        void StoreObject(string key, object o);
        object GetObject(string key);
    }
}
