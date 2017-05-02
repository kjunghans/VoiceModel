using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Survey.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Survey.Repository.ModelConfiguration
{
    class PossibleAnswerConfig : EntityTypeConfiguration<PossibleAnswer>
    {
       internal PossibleAnswerConfig()
       {
           this.HasKey(p => p.Id);
           this.HasRequired(p => p.Question)
            .WithMany(q => q.PossibleAnswers)
            .HasForeignKey(p => p.QuestionId);

       }
     }
}
