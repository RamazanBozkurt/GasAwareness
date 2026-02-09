using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Models.Survey.Requests
{
    public class CreateOptionRequestDto
    {
        public bool IsCorrect { get; set; }
        public string OptionText { get; set; }
        public int Order { get; set; }
    }
}