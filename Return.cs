using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel
{
    class Return : VoiceModel
    {
        Return(string id)
        {
            this.id = id;
        }

        public override VoiceModel BuildModel(string jsonArgs)
        {
            this.json = jsonArgs;
            return this;
        }

    }
}
