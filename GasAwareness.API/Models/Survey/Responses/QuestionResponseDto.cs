using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Models.Survey.Responses
{
    public class QuestionResponseDto
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; }
        public List<OptionResponseDto> Options { get; set; }
    }
}