using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Entities
{
    public class Video : EntityBase
    {
        public Video()
        {
            VideoCategories = new List<VideoCategory>();
            VideoAgeGroups = new List<VideoAgeGroup>();
            VideoSubscriptionTypes = new List<VideoSubscriptionType>();
        }

        public string Title { get; set; }
        public int DurationSeconds { get; set; }
        public string Url { get; set; }

        public ICollection<VideoCategory> VideoCategories { get; set; }
        public ICollection<VideoSubscriptionType> VideoSubscriptionTypes { get; set; }
        public ICollection<VideoAgeGroup> VideoAgeGroups { get; set; }
        public ICollection<UserVideo> UserVideos { get; set; }
        public ICollection<Survey> Surveys { get; set; }
    }
}