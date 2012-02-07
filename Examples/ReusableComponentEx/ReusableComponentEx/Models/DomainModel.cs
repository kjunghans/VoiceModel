using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoiceModel;

namespace ReusableComponentEx.Models
{
    public class DomainModel : ComponentInput
    {
        public DateTime startDate { get; set; }
        public DateTime finishDate { get; set; }
    }
}