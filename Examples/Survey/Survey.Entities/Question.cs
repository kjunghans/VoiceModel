using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Survey.Common;

namespace Survey.Entities
{

    public class Question
    {
        public int Id { get; set; }
        public int Sequence { get; set; }
        public string Text { get; set; }
        public string AudioFileName { get; set; }
        public AnswerTypes AnswerType { get; set; }

        public int SurveyId { get; set; }
        public virtual Survey Survey { get; set; }

        public virtual ICollection<PossibleAnswer> PossibleAnswers { get; set; }
        public virtual ICollection<SurveyResponse> SurveyResponses { get; set; }
    }
}
