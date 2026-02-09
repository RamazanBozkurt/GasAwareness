using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Models.Survey.Requests
{
    public class SurveySubmitRequestDto
    {
        public Guid SurveyId { get; set; }
        public List<QuestionAnswerRequestDto> Answers { get; set; } = new();
    }
}