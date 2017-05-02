using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoiceModel;
using VoiceModel.CallFlow;
using Survey.Service;
using Survey.Service.DTO;
using Survey.Common;

namespace Survey.Controllers
{
    public class SurveyController : VoiceController
    {
        public override CallFlow BuildCallFlow()
        {
            CallFlow flow = new CallFlow();
            //Create Service/Domain Layer for accessing survey information
            SurveyService service = new SurveyService();
            //Get a survey named MySurvey
            Service.DTO.Survey survey = service.GetSurvey("MySurvey");
            //If we did not get a survey back there is an error. Tell the caller.
            if (survey == null)
            {
                flow.AddState(new VoiceState("noSurvey", new Exit("noSurvey", "Error finding survey in database. Goodbye.")),true);
                return flow;
            }

            //If we got here then we have a survey from the database.
            //Create a greeting and on the exit action from this state
            //get the user we seeded from the database.  In a real survey
            //application we would prompt the user for their id and pin 
            //to identify them.
            State greeting = new VoiceState("greeting", new Say("greeting", "Please take our short survey"))
                .AddOnExitAction(delegate(CallFlow cf, State state, Event e)
                {
                    SurveyService sservice = new SurveyService();
                    User user = sservice.GetUserByUserId("001");
                    int uid = 0;
                    if (user != null)
                        uid = user.Id;
                    cf.Ctx.SetGlobal<int>("userId", uid);
                });
            flow.AddState(greeting, true);
            State prevState = greeting;
            int questionNum = 0;
            int questionCount = survey.Questions.Count;
            //Iterate through the survey questions and add them to our callflow.
            foreach (Question question in survey.Questions)
            {
                questionNum++;
                string questionId = question.Id.ToString();
                bool lastQuestion = questionNum == questionCount;
                prevState.AddTransition("continue", questionId, null);
                Ask askQuestion = new Ask(questionId);
                askQuestion.initialPrompt.Add( new Prompt(question.Text));
                if (question.AnswerType == AnswerTypes.boolean)
                    askQuestion.grammar = new Grammar(new BuiltinGrammar(BuiltinGrammar.GrammarType.boolean));
                else
                    askQuestion.grammar = new Grammar("possibleAnswers", question.PossibleAnswers.Select(p => p.Answer).ToList());
                State questionState = ViewStateBuilder.Build(questionId, askQuestion);
                questionState.AddOnExitAction(delegate(CallFlow cf, State state, Event e)
                {
                    SurveyService sservice = new SurveyService();
                    string response = state.jsonArgs;
                    int userId = cf.Ctx.GetGlobalAs<int>("userId");
                    int qId = Int32.Parse( state.Id);
                    sservice.InsertResponse(response, qId, userId);

                });
                flow.AddState(questionState);
                prevState = questionState;
            }
            //Take the last question in the survey and add a transition to the application exit.
            prevState.AddTransition("continue", "goodbye", null);
            //Tell the caller goodbye and hangup
            flow.AddState(new VoiceState("goodbye", new Exit("goodbye", "Thank you for taking our survey. Goodbye.")));

            return flow;

        }

    }
}
