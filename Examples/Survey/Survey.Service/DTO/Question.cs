using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Survey.Common;

namespace Survey.Service.DTO
{
    public class Question
    {
        public int Id { get; set; }
        public int Sequence { get; set; }
        public string Text { get; set; }
        public string AudioFileName { get; set; }
        public AnswerTypes AnswerType { get; set; }
        public List<PossibleAnswer> PossibleAnswers { get; set; }
        public List<SurveyResponse> SurveyResponses { get; set; }

    }
}
