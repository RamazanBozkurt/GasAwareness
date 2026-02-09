using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Entities
{
    public class VideoAgeGroup : EntityBase
    {
        public Guid VideoId { get; set; }
        public virtual Video Video { get; set; }

        public Guid AgeGroupId { get; set; }
        public virtual AgeGroup AgeGroup { get; set; }
    }
}