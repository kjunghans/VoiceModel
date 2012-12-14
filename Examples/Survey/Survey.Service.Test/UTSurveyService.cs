using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Survey.Service;
using Survey.Service.DTO;
using Survey.Common;
using System.Collections.Generic;

namespace Survey.Service.Test
{
    [TestClass]
    public class UTSurveyService
    {
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


        }
    }
}
