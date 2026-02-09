using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Entities
{
    public class SurveyQuestion : EntityBase
    {
        public string QuestionText { get; set; }
        public int Order { get; set; }

        public Guid SurveyId { get; set; }
        public virtual Survey Survey { get; set; }

        public ICollection<SurveyAnswer> Answers { get; set; }
        public ICollection<SurveyOption> Options { get; set; }
    }
}