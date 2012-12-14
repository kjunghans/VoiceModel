using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Entities
{
    public class SurveyResponse
    {
        public int Id { get; set; }
        public string Response { get; set; }
        public DateTime TimeStamp { get; set; }

        public int QuestionId { get; set; }
        public int UserId { get; set; }

        public virtual Question Question { get; set; }
        public virtual User User { get; set; }
    }
}
