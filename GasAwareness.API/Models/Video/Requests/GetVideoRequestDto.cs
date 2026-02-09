using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Models.Video.Requests
{
    public record GetVideoRequestDto (Guid? CategoryId, Guid? AgeGroupId, Guid? SubscriptionTypeId)
    {
    }
}