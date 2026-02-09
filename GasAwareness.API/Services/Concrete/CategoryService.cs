using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GasAwareness.API.Entities;
using GasAwareness.API.Models.Category.Requests;
using GasAwareness.API.Models.Category.Responses;
using GasAwareness.API.Repositories.Abstract;
using GasAwareness.API.Services.Abstract;

namespace GasAwareness.API.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> CreateCategoryAsync(CreateCategoryRequestDto request)
        {
            var entity = _mapper.Map<Category>(request);

            var createdEntity = await _repository.CreateEntityAsync(entity);

            if (createdEntity == null) return false;

            return true;
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            return await _repository.DeleteEntityAsync(id);
        }

        public async Task<List<CategoryResponseDto>> GetCategoriesAsync()
        {
            return _mapper.Map<List<CategoryResponseDto>>((await _repository.GetEntityListAsync(null, x => !x.IsDeleted)).OrderBy(x => x.Name));
        }
    }
}