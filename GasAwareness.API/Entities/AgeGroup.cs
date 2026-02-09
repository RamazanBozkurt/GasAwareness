using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Entities
{
    public class AgeGroup : EntityBase
    {
        public string Name { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }

        public ICollection<VideoAgeGroup> VideoAgeGroups { get; set; }
    }
}