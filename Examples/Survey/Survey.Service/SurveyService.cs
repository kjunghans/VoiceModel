using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Survey.Entities;
using Survey.Repository;
using AutoMapper;

namespace Survey.Service
{
    public class SurveyService
    {
        public SurveyService()
        {
            Mapper.CreateMap<Entities.Survey, DTO.Survey>();
            Mapper.CreateMap<Entities.User, DTO.User>();
            Mapper.CreateMap<Entities.Question, DTO.Question>();
            Mapper.CreateMap<Entities.PossibleAnswer, DTO.PossibleAnswer>();
            Mapper.CreateMap<Entities.Survey, DTO.Survey>();
            Mapper.CreateMap<Entities.SurveyResponse, DTO.SurveyResponse>();
            Mapper.CreateMap<DTO.Survey, Entities.Survey>();
            Mapper.CreateMap<DTO.Question, Entities.Question>();
            Mapper.CreateMap<DTO.PossibleAnswer, Entities.PossibleAnswer>();

        }

        public DTO.Survey GetSurvey(string surveyName)
        {
            UnitOfWork uow = new UnitOfWork();
            Entities.Survey survey = uow.SurveyRepository.Get(s => s.Name == surveyName).SingleOrDefault();
            return Mapper.Map<DTO.Survey>(survey);
        }

        public void InsertSurvey(DTO.Survey survey)
        {
            UnitOfWork uow = new UnitOfWork();
            Entities.Survey eSurvey = Mapper.Map<Entities.Survey>(survey);
            uow.SurveyRepository.Insert(eSurvey);
            uow.Save();

        }

        public int InsertUser(string name, string userId, string pin)
        {
            UnitOfWork uow = new UnitOfWork();
            User user = new User() { Name = name, Pin = pin, UserId = userId };
            uow.UserRepository.Insert(user);
            uow.Save();
            return user.Id;
        }

        public DTO.User GetUserByUserId(string userId)
        {
            UnitOfWork uow = new UnitOfWork();
            Entities.User user = uow.UserRepository.Get(u => u.UserId == userId).SingleOrDefault();
            return Mapper.Map<DTO.User>(user);
        }

        public void InsertResponse(string response, int questionId, int userId)
        {
            UnitOfWork uow = new UnitOfWork();
            SurveyResponse resp = new SurveyResponse()
            {
                Response = response,
                QuestionId = questionId,
                UserId = userId,
                TimeStamp = DateTime.Now
            };
            uow.SurveyResponseRepository.Insert(resp);
            uow.Save();
        }

        public List<DTO.SurveyResponse> GetResponsesForQuestion(int questionId)
        {
            UnitOfWork uow = new UnitOfWork();
            List<Entities.SurveyResponse> responses = uow.SurveyResponseRepository.Get(r => r.QuestionId == questionId).ToList();
            return Mapper.Map<List<DTO.SurveyResponse>>(responses);
        }
    }
}
