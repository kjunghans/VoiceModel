using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Service.DTO
{
    public class Survey
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Question> Questions { get; set; }

        public Survey()
        {
            Questions = new List<Question>();
        }

    }
}
