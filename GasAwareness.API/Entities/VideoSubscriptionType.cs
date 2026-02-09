using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Entities
{
    public class VideoSubscriptionType : EntityBase
    {
        public Guid VideoId { get; set; }
        public virtual Video Video { get; set; }

        public Guid SubscriptionTypeId { get; set; }
        public virtual SubscriptionType SubscriptionType { get; set; }
    }
}