using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Models.Survey.Requests
{
    public class QuestionAnswerRequestDto
    {
        public Guid QuestionId { get; set; }
        public Guid OptionId { get; set; }
    }
}