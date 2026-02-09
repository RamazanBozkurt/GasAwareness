using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Models.Survey.Requests
{
    public class CreateSurveyRequestDto
    {
        public string Title { get; set; }
        public List<CreateQuestionRequestDto> Questions { get; set; } = new();
    }
}