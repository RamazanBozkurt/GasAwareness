using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Entities
{
    public class Survey : EntityBase
    {
        public string Title { get; set; }

        public Guid? VideoId { get; set; }
        public virtual Video Video { get; set; }

        public ICollection<SurveyQuestion> Questions { get; set; } = new List<SurveyQuestion>();
        public ICollection<SurveyResult> SurveyResults { get; set; } = new List<SurveyResult>();
    }
}