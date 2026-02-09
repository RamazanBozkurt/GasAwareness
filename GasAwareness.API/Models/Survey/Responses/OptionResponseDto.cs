using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Models.Survey.Responses
{
    public class OptionResponseDto
    {
        public Guid Id { get; set; }
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
    }
}