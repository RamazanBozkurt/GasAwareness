using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Entities
{
    public class SurveyOption : EntityBase
    {
        public bool IsCorrect { get; set; }
        public string OptionText { get; set; } 
        public int Order { get; set; }

        public Guid QuestionId { get; set; } 
        public virtual SurveyQuestion Question { get; set; }

        public virtual ICollection<SurveyAnswer> Answers { get; set; } = new List<SurveyAnswer>();
    }
}