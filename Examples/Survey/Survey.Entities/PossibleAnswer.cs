using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Entities
{
    public class PossibleAnswer
    {
        public int Id { get; set; }
        public int Sequence { get; set; }
        public string Answer { get; set; }
        public string AudioFileName { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
