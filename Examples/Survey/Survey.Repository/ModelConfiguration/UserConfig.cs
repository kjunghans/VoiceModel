using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Survey.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Survey.Repository.ModelConfiguration
{
    public class UserConfig : EntityTypeConfiguration<SurveyResponse>
    {
        internal UserConfig()
        {
            this.HasKey(u => u.Id);
        }
    }
}
