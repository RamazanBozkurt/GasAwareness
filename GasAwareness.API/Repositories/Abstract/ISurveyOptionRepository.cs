using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasAwareness.API.Entities;

namespace GasAwareness.API.Repositories.Abstract
{
    public interface ISurveyOptionRepository : IGenericRepository<SurveyOption>
    {
        Task<List<SurveyOption>> GetSurveyOptionsAsync(Guid surveyId);
    }
}