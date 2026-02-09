using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Models.AgeGroup.Responses
{
    public class AgeGroupResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}