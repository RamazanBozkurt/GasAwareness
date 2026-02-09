using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Entities
{
    public class SubscriptionType : EntityBase
    {
        public string Name { get; set; }

        public ICollection<VideoSubscriptionType> VideoSubscriptionTypes { get; set; }
    }
}