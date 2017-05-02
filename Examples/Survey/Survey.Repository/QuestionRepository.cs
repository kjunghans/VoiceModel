using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Survey.Entities;

namespace Survey.Repository
{
    public class QuestionRepository: GenericRepository<Question>
    {
        public QuestionRepository(SurveyContext context) : base(context) { }
    }
}
