using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Survey.Entities;

namespace Survey.Repository
{
    public class SurveyResponseRepository: GenericRepository<SurveyResponse>
    {
        public SurveyResponseRepository(SurveyContext context) : base(context) { }
    }
}
