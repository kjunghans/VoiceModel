using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoiceModel;

namespace GetDateDtmf
{
    public class GetDateDtmfOutput : ComponentOutput
    {
        public DateTime Date { get; set; }
        public bool IsValidDate { get; set; }
    }
}
