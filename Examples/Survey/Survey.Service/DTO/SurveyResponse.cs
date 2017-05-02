using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Service.DTO
{
    public class SurveyResponse
    {
        public int Id { get; set; }
        public string Response { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
