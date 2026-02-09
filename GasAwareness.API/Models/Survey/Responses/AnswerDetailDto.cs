using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Models.Survey.Responses
{
    public class AnswerDetailDto
    {
        public string QuestionText { get; set; }
        public string SelectedOptionText { get; set; }
        public bool IsCorrect { get; set; }
        public string CorrectOption { get; set; }
    }
}