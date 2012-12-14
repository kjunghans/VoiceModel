using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Survey.Entities;

namespace Survey.Repository
{
    public class UnitOfWork: IDisposable
    {
        SurveyContext _context = new SurveyContext();
        PossibleAnswerRepository _possibleAnswerRepository;
        QuestionRepository _questionRepository;
        SurveyRepository _surveyRepository;
        SurveyResponseRepository _surveyResponseRepository;
        UserRepository _userRepository;


        public UnitOfWork()
        {
            _possibleAnswerRepository = new PossibleAnswerRepository(_context);
            _questionRepository = new QuestionRepository(_context);
            _surveyRepository = new SurveyRepository(_context);
            _surveyResponseRepository = new SurveyResponseRepository(_context);
            _userRepository = new UserRepository(_context);
        }

        public PossibleAnswerRepository PossibleAnswerRepository
        {
            get { return _possibleAnswerRepository; }
        }

        public QuestionRepository QuestionRepository
        {
            get { return _questionRepository; }
        }

        public SurveyRepository SurveyRepository
        {
            get { return _surveyRepository; }
        }

        public SurveyResponseRepository SurveyResponseRepository
        {
            get { return _surveyResponseRepository; }
        }

        public UserRepository UserRepository
        {
            get { return _userRepository; }
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public SurveyContext Context
        {

            get
            {
                return _context;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
