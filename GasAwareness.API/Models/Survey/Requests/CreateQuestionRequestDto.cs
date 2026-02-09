using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Models.Survey.Requests
{
    public class CreateQuestionRequestDto
    {
        public string QuestionText { get; set; }
        public int Order { get; set; }
        public List<CreateOptionRequestDto> Options { get; set; } = new();
    }
}