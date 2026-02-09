using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasAwareness.API.Models.Category.Requests;
using GasAwareness.API.Models.Category.Responses;

namespace GasAwareness.API.Services.Abstract
{
    public interface ICategoryService
    {
        Task<List<CategoryResponseDto>> GetCategoriesAsync();
        Task<bool> CreateCategoryAsync(CreateCategoryRequestDto request);
        Task<bool> DeleteCategoryAsync(Guid id);
    }
}