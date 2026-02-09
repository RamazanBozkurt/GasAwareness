using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Models.Survey.Responses
{
    public class SurveyResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<QuestionResponseDto> Questions { get; set; }
    }
}