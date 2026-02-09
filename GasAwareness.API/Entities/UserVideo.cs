using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Entities
{
    public class UserVideo : EntityBase
    {
        public bool IsWatched { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public Guid VideoId { get; set; }
        public virtual Video Video { get; set; }
    }
}