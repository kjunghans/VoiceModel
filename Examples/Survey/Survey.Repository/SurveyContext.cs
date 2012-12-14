using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Survey.Entities;
using Survey.Repository.ModelConfiguration;
 

namespace Survey.Repository
{
    public class SurveyContext : DbContext
    {
        public SurveyContext() : base("VoiceSurvey") { }

        public DbSet<PossibleAnswer> PossibleAnswers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Survey.Entities.Survey> Surveys { get; set; }
        public DbSet<SurveyResponse> SurveyResponses { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PossibleAnswerConfig());
        }

    }
}
