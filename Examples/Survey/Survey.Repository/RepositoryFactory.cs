using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Repository
{
    public class RepositoryFactory<TEntity> where TEntity : class
    {
        private GenericRepository<TEntity> _repository;
        private SurveyContext _context;

        public RepositoryFactory(SurveyContext context) 
        {
            _context = context;
        }

        public GenericRepository<TEntity> Instance
        {
            get
            {
                if (_repository == null)
                    _repository = new GenericRepository<TEntity>(_context);
                return _repository;
            }
        }
    }
}
