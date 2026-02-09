using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Entities
{
    public class SurveyAnswer : EntityBase
    {
        public DateTime AnsweredAt { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public Guid SurveyQuestionId { get; set; }
        public virtual SurveyQuestion Question { get; set; }

        public Guid SurveyOptionId { get; set; }
        public virtual SurveyOption Option { get; set; }
    }
}