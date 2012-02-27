using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoiceModel.CallFlow;

namespace GetDateDtmf
{
    public class GetDateDtmfState : StartComponentState
    {
        public GetDateDtmfState(string id, string target, GetDateDtmfInput input) : 
            base(id, target, new GetDateDtmfController(), input) { }
    }
}
