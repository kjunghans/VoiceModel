using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Survey.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Survey.Repository.ModelConfiguration
{
    public class SurveyConfig : EntityTypeConfiguration<Survey.Entities.Survey>
    {
        internal SurveyConfig()
        {
            this.HasKey(s => s.Id);
            
        }
    }
}
