using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasAwareness.API.Entities;
using GasAwareness.API.Repositories.Abstract;

namespace GasAwareness.API.Repositories.Abstract
{
    public interface ISurveyRepository : IGenericRepository<Survey>
    {
        Task<Survey?> GetSurveyAsync(Guid id);
        Task<bool> IsSurveySolvedByUser(string userId, Guid surveyId);
    }
}