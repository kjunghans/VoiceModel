using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Survey.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Survey.Repository.ModelConfiguration
{
    public class SurveyResponseConfig : EntityTypeConfiguration<SurveyResponse>
    {
        internal SurveyResponseConfig()
        {
            this.HasKey(s => s.Id);

            this.HasRequired(s => s.Question)
             .WithMany(q => q.SurveyResponses)
             .HasForeignKey(s => s.QuestionId);

            this.HasRequired(s => s.User)
             .WithMany(q => q.SurveyResponses)
             .HasForeignKey(s => s.UserId);

        }
    }
}
