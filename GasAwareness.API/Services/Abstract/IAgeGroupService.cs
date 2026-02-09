using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasAwareness.API.Models.AgeGroup.Requests;
using GasAwareness.API.Models.AgeGroup.Responses;

namespace GasAwareness.API.Services.Abstract
{
    public interface IAgeGroupService
    {
        Task<List<AgeGroupResponseDto>> GetAgeGroupsAsync();
        Task<bool> CreateAgeGroupAsync(CreateAgeGroupRequestDto request);
        Task<bool> DeleteAgeGroupAsync(Guid id);
    }
}