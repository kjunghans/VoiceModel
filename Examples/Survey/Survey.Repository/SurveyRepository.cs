using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Survey.Entities;

namespace Survey.Repository
{
    public class SurveyRepository: GenericRepository<Survey.Entities.Survey>
    {
        public SurveyRepository(SurveyContext context) : base(context) { }
    }
}
