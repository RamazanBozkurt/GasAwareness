using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace GasAwareness.API.Entities
{
    public class User : IdentityUser
    {
        public DateTime CreatedAt { get; set; }

        public ICollection<UserVideo> UserVideos { get; set; }
        public ICollection<SurveyAnswer> SurveyAnswers { get; set; }
        public ICollection<SurveyResult> SurveyResults { get; set; }
    }
}