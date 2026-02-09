using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Models.Survey.Responses
{
    public class UserSurveyListDto
    {
        public Guid ResultId { get; set; }
        public string UserEmail { get; set; }
        public string SurveyTitle { get; set; }
        public int CorrectCount { get; set; }
        public int WrongCount { get; set; }
        public double Score { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}