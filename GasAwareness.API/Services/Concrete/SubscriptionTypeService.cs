using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GasAwareness.API.Entities;
using GasAwareness.API.Models.SubscriptionType.Requests;
using GasAwareness.API.Models.SubscriptionType.Responses;
using GasAwareness.API.Repositories.Abstract;
using GasAwareness.API.Services.Abstract;

namespace GasAwareness.API.Services.Concrete
{
    public class SubscriptionTypeService : ISubscriptionTypeService
    {
        private readonly ISubscriptionTypeRepository _repository;
        private readonly IMapper _mapper;
        public SubscriptionTypeService(ISubscriptionTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> CreateSubscriptionTypeAsync(CreateSubscriptionTypeRequestDto request)
        {
            var entity = _mapper.Map<SubscriptionType>(request);

            var createdEntity = await _repository.CreateEntityAsync(entity);

            if (createdEntity == null) return false;

            return true;
        }

        public async Task<bool> DeleteSubscriptionTypeAsync(Guid id)
        {
            return await _repository.DeleteEntityAsync(id);
        }

        public async Task<List<SubscriptionTypeResponseDto>> GetSubscriptionTypesAsync()
        {
            return _mapper.Map<List<SubscriptionTypeResponseDto>>((await _repository.GetEntityListAsync(null, x => !x.IsDeleted)).OrderBy(x => x.Name));
        }
    }
}