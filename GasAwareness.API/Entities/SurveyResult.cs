using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Entities
{
    public class SurveyResult : EntityBase
    {
        public double Score { get; set; }    
        public int CorrectCount { get; set; }
        public int WrongCount { get; set; }
        public DateTime CompletedAt { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; }
        public virtual User User { get; set; }
        
        public Guid SurveyId { get; set; }
        public virtual Survey Survey { get; set; }
    }
}