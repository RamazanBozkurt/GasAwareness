using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Models.Video.Responses
{
    public class VideoResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int DurationSeconds { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}