using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasAwareness.API.Entities;
using GasAwareness.API.Repositories.Abstract;

namespace GasAwareness.API.Repositories.Concrete
{
    public class AgeGroupRepository : GenericRepository<AgeGroup>, IAgeGroupRepository
    {
        private readonly DataContext _context;
        public AgeGroupRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}