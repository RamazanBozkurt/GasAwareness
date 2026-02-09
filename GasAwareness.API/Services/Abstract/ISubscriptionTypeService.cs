using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasAwareness.API.Models.SubscriptionType.Requests;
using GasAwareness.API.Models.SubscriptionType.Responses;

namespace GasAwareness.API.Services.Abstract
{
    public interface ISubscriptionTypeService
    {
        Task<List<SubscriptionTypeResponseDto>> GetSubscriptionTypesAsync();
        Task<bool> CreateSubscriptionTypeAsync(CreateSubscriptionTypeRequestDto request);
        Task<bool> DeleteSubscriptionTypeAsync(Guid id);
    }
}