using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoiceModel;

namespace GetDateDtmf
{
    public class GetDateDtmfView : Component
    {
        public GetDateDtmfView(string id) : base(id, new GetDateDtmfController())
        {
        
        }
            
    }
}
