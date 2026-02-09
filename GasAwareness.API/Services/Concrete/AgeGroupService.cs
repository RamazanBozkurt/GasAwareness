using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GasAwareness.API.Entities;
using GasAwareness.API.Models.AgeGroup.Requests;
using GasAwareness.API.Models.AgeGroup.Responses;
using GasAwareness.API.Repositories.Abstract;
using GasAwareness.API.Services.Abstract;

namespace GasAwareness.API.Services.Concrete
{
    public class AgeGroupService : IAgeGroupService
    {
        private readonly IAgeGroupRepository _repository;
        private readonly IMapper _mapper;
        public AgeGroupService(IAgeGroupRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> CreateAgeGroupAsync(CreateAgeGroupRequestDto request)
        {
            var entity = _mapper.Map<AgeGroup>(request);

            var createdEntity = await _repository.CreateEntityAsync(entity);

            if (createdEntity == null) return false;

            return true;
        }

        public async Task<bool> DeleteAgeGroupAsync(Guid id)
        {
            return await _repository.DeleteEntityAsync(id);
        }

        public async Task<List<AgeGroupResponseDto>> GetAgeGroupsAsync()
        {
            return _mapper.Map<List<AgeGroupResponseDto>>((await _repository.GetEntityListAsync(null, x => !x.IsDeleted)).OrderBy(x => x.MinAge).ThenBy(x => x.MaxAge));
        }
    }
}