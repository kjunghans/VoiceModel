using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Survey.Service;
using Survey.Service.DTO;
using Survey.Common;
using System.Collections.Generic;
using Survey.Repository;
using System.Data.Entity;

namespace Survey.Service.Test
{
    [TestClass]
    public class UTSurveyService
    {
        UnitOfWork uow;

        [TestInitialize]
        public void TestInitialize()
        {
            Database.SetInitializer<SurveyContext>(new DBInitializer());
            SurveyContext context = new SurveyContext();
            //context.Database.CreateIfNotExists();
            //Need to invoke context to have the DbInitializer do its stuff
            context.Surveys.Find(-1);
            uow = new UnitOfWork();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            SurveyContext context = new SurveyContext();
            context.Database.ExecuteSqlCommand("ALTER DATABASE VoiceSurvey SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
            context.Database.Delete();
            uow.Dispose();
        }
        [TestMethod]
        public void TestSurveyCRUD()
        {
            SurveyService service = new SurveyService();
            string surveyName = "MySurvey";
            DTO.Survey survey = new DTO.Survey()
                {
                    Name = surveyName
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
                Answer = "1",
                Sequence = 1
            });
            answers.Add(new PossibleAnswer()
            {
                Answer = "2",
                Sequence = 2
            });

            survey.Questions.Add(new Question()
            {
                AnswerType = AnswerTypes.list,
                Sequence = 2,
                Text = "If you like oranges press one. If you like apples press two.",
                PossibleAnswers = answers
            });

            service.InsertSurvey(survey);
            DTO.Survey expectedSurvey = service.GetSurvey(surveyName);
            Assert.IsNotNull(expectedSurvey, "Could not find survey with a name of " + surveyName);
            Assert.AreEqual(expectedSurvey.Name, survey.Name, "Survey names do not match");

            string userId = "1234";
            int uid = service.InsertUser( "joe tester", userId, "0001");
            User user = service.GetUserByUserId(userId);
            Assert.IsNotNull(user, "Could not find user with ID of " + userId);

            service.InsertResponse("No", expectedSurvey.Questions[0].Id, user.Id);
            user = service.GetUserByUserId(userId);
            Assert.IsNotNull(user, "Could not find user with ID of " + userId + " after adding response to question.");
            Assert.AreEqual(1, user.SurveyResponses.Count, "Expected 1 survey response but there are " + user.SurveyResponses.Count.ToString());


        }
    }
}
