using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Entities
{
    public class VideoCategory : EntityBase
    {
        public Guid VideoId { get; set; }
        public virtual Video Video { get; set; }

        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}