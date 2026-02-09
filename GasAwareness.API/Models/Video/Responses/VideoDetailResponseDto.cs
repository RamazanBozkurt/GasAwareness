using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasAwareness.API.Models.AgeGroup.Responses;
using GasAwareness.API.Models.Category.Responses;
using GasAwareness.API.Models.SubscriptionType.Responses;

namespace GasAwareness.API.Models.Video.Responses
{
    public class VideoDetailResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsWatched { get; set; }
        public Guid? SurveyId { get; set; }
        public bool IsSurveySolved { get; set; }
        public List<CategoryResponseDto> Categories { get; set; }
        public List<AgeGroupResponseDto> AgeGroups { get; set; }
        public List<SubscriptionTypeResponseDto> SubscriptionTypes { get; set; }
    }
}