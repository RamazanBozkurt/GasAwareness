using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasAwareness.API.Entities;
using GasAwareness.API.Repositories.Abstract;

namespace GasAwareness.API.Repositories.Concrete
{
    public class UserVideoRepository : GenericRepository<UserVideo>, IUserVideoRepository
    {
        public UserVideoRepository(DataContext context) : base(context)
        {
        }
    }
}