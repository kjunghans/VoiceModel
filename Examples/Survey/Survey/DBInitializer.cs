using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Survey.Common;
using Survey.Entities;
using Survey.Repository;

namespace Survey
{
    public class DBInitializer : DropCreateDatabaseAlways<SurveyContext>
    {
        protected override void Seed(SurveyContext context)
        {
            UnitOfWork uow = new UnitOfWork();
           string surveyName = "MySurvey";
            Entities.Survey survey = new Entities.Survey()
            {
                Name = surveyName,
                Questions = new List<Question>()
            };
            survey.Questions.Add(new Question()
            {
                AnswerType = AnswerTypes.boolean,
                Sequence = 1,
                Text = "Do you like this question?"
            });
            List<PossibleAnswer> answers = new List<PossibleAnswer>();
            answers.Add(new PossibleAnswer()
            {
                Answer = "oranges",
                Sequence = 1
            });
            answers.Add(new PossibleAnswer()
            {
                Answer = "apples",
                Sequence = 2
            });

            survey.Questions.Add(new Question()
            {
                AnswerType = AnswerTypes.list,
                Sequence = 2,
                Text = "Which do you prefer, oranges or apples?",
                PossibleAnswers = answers
            });

            uow.SurveyRepository.Insert(survey);
            uow.Save();

            User user = new User()
            {
                Name = "Joe Tester",
                UserId = "001",
                Pin = "002"
            };
            uow.UserRepository.Insert(user);
            uow.Save();

        }
    }
}