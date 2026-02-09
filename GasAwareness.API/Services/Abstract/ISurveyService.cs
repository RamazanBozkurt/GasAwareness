using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasAwareness.API.Entities;
using GasAwareness.API.Models.Survey.Requests;
using GasAwareness.API.Models.Survey.Responses;

namespace GasAwareness.API.Services.Abstract
{
    public interface ISurveyService
    {
        Task<Guid> CreateSurveyAsync(CreateSurveyRequestDto request);
        Task<SurveyResponseDto?> GetSurveyAsync(Guid id);
        Task<SurveyResultResponseDto?> SubmitSurveyAnswersAsync(string userId, SurveySubmitRequestDto request);
        Task<bool> IsSurveySolvedByUser(string userId, Guid surveyId);
        Task<List<SurveyMainResponseDto>> GetSurveyMainListAsync();
        Task<List<SurveyMainResponseDto>> GetAllSurveysMainListAsync();
        Task<bool> UpdateAsync(Guid videoId, Guid surveyId);
        Task<List<UserSurveyListDto>> GetAllUserSurveysAsync();
        Task<UserSurveyDetailDto?> GetUserSurveyDetailAsync(Guid resultId);
    }
}