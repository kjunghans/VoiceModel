using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Survey.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Survey.Repository.ModelConfiguration
{
    public class QuestionConfig : EntityTypeConfiguration<Question>
    {
        internal QuestionConfig()
        {
            this.HasKey(q => q.Id);
            this.HasRequired(q => q.Survey)
             .WithMany(s => s.Questions)
             .HasForeignKey(q => q.SurveyId);
        }
    }
}
