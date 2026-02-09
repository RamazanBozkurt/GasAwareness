using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Entities
{
    public class Category : EntityBase
    {
        public string Name { get; set; }

        public ICollection<VideoCategory> VideoCategories { get; set; }
    }
}