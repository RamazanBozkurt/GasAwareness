using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Models.Survey.Responses
{
    public class UserSurveyDetailDto
    {
        public string UserEmail { get; set; }
        public string SurveyTitle { get; set; }
        public double Score { get; set; }
        public List<AnswerDetailDto> Answers { get; set; }
    }
}