using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Models.Video.Requests
{
    public class CreateVideoRequestDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public int DurationSeconds { get; set; }
        public Guid CategoryId { get; set; }
        public Guid AgeGroupId { get; set; }
        public Guid SubscriptionTypeId { get; set; }
        public Guid SurveyId { get; set; }
    }
}