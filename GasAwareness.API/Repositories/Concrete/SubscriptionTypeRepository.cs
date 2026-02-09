using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasAwareness.API.Entities;
using GasAwareness.API.Repositories.Abstract;

namespace GasAwareness.API.Repositories.Concrete
{
    public class SubscriptionTypeRepository : GenericRepository<SubscriptionType>, ISubscriptionTypeRepository
    {
        public SubscriptionTypeRepository(DataContext context) : base(context)
        {
        }
    }
}