using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Survey.Entities;

namespace Survey.Repository
{
    public class UserRepository: GenericRepository<User>
    {
        public UserRepository(SurveyContext context) : base(context) { }
    }
}
