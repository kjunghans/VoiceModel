﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Pin { get; set; }

        public virtual ICollection<SurveyResponse> SurveyResponses { get; set; }
    }
}
