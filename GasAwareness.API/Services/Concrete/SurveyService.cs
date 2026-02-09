using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GasAwareness.API.Entities;
using GasAwareness.API.Models.Survey.Requests;
using GasAwareness.API.Models.Survey.Responses;
using GasAwareness.API.Repositories.Abstract;
using GasAwareness.API.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace GasAwareness.API.Services.Concrete
{
    public class SurveyService : ISurveyService
    {
        private readonly ISurveyRepository _repository;
        private readonly IMapper _mapper;
        private readonly ISurveyOptionRepository _surveyOptionRepository;
        private readonly ISurveyAnswerRepository _surveyAnswerRepository;
        private readonly ISurveyResultRepository _surveyResultRepository;
        private readonly DataContext _context;
        public SurveyService(ISurveyRepository repository, IMapper mapper, ISurveyOptionRepository surveyOptionRepository, ISurveyAnswerRepository surveyAnswerRepository, ISurveyResultRepository surveyResultRepository, DataContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _surveyOptionRepository = surveyOptionRepository;
            _surveyAnswerRepository = surveyAnswerRepository;
            _surveyResultRepository = surveyResultRepository;
            _context = context;
        }

        public async Task<Guid> CreateSurveyAsync(CreateSurveyRequestDto request)
        {
            var survey = new Survey
            {
                Title = request.Title,
                Questions = new List<SurveyQuestion>()
            };

            foreach (var questionDto in request.Questions)
            {
                var question = new SurveyQuestion
                {
                    QuestionText = questionDto.QuestionText,
                    Order = questionDto.Order,
                    Options = new List<SurveyOption>()
                };

                foreach (var option in questionDto.Options)
                {
                    question.Options.Add(new SurveyOption
                    {
                        Id = Guid.NewGuid(),
                        IsCorrect = option.IsCorrect,
                        OptionText = option.OptionText,
                        Order = option.Order
                    });
                }

                survey.Questions.Add(question);
            }

            var createdSurvey = await _repository.CreateEntityAsync(survey);

            return createdSurvey.Id;
        }

        public async Task<SurveyResponseDto?> GetSurveyAsync(Guid id)
        {
            var survey = await _repository.GetSurveyAsync(id);

            if (survey == null) return null;

            var response = _mapper.Map<SurveyResponseDto>(survey);

            return response;
        }

        public async Task<List<SurveyMainResponseDto>> GetSurveyMainListAsync()
        {
            var surveys = await _repository.GetEntityListAsync(new string[] { "Questions" }, x => !x.IsDeleted && x.VideoId == null);

            if (!surveys.Any()) return new List<SurveyMainResponseDto>();

            return _mapper.Map<List<SurveyMainResponseDto>>(surveys);
        }

        public async Task<bool> IsSurveySolvedByUser(string userId, Guid surveyId)
        {
            return await _repository.IsSurveySolvedByUser(userId, surveyId);
        }

        public async Task<SurveyResultResponseDto?> SubmitSurveyAnswersAsync(string userId, SurveySubmitRequestDto request)
        {
            var surveyOptions = await _surveyOptionRepository.GetSurveyOptionsAsync(request.SurveyId);

            int correctCount = 0;
            var answersToSave = new List<SurveyAnswer>();

            foreach (var answer in request.Answers)
            {
                var selectedOption = surveyOptions.FirstOrDefault(o => o.Id == answer.OptionId);

                if (selectedOption != null && selectedOption.IsCorrect)
                {
                    correctCount++;
                }

                answersToSave.Add(new SurveyAnswer
                {
                    UserId = userId,
                    SurveyQuestionId = answer.QuestionId,
                    SurveyOptionId = answer.OptionId,
                    AnsweredAt = DateTime.UtcNow
                });
            }

            var answerResponse = await _surveyAnswerRepository.CreateEntityAsync(answersToSave);

            if (!answerResponse.Any()) return null;

            int totalQuestions = request.Answers.Count;
            double score = totalQuestions > 0 ? (double)correctCount / totalQuestions * 100 : 0;
            int wrongCount = totalQuestions - correctCount;

            SurveyResult surveyResult = new SurveyResult
            {
                UserId = userId,
                SurveyId = request.SurveyId,
                Score = score,
                CorrectCount = correctCount,
                WrongCount = wrongCount
            };

            var surveyResultResponse = await _surveyResultRepository.CreateEntityAsync(surveyResult);

            if (surveyResultResponse == null) return null;

            return new SurveyResultResponseDto
            {
                TotalQuestions = totalQuestions,
                CorrectAnswers = correctCount,
                WrongAnswers = wrongCount,
                Score = score
            };
        }

        public async Task<bool> UpdateAsync(Guid videoId, Guid surveyId)
        {
            var survey = await _repository.GetEntityAsync(null, x => x.Id == surveyId);
            survey?.VideoId = videoId;
            var response = await _repository.UpdateEntityAsync(survey);

            if (response == null) return false;

            return true;
        }

        public async Task<List<UserSurveyListDto>> GetAllUserSurveysAsync()
        {
            var userSurveys = await _surveyResultRepository.GetAllUserSurveysAsync();

            var response = _mapper.Map<List<UserSurveyListDto>>(userSurveys);

            return response;
        }

        public async Task<UserSurveyDetailDto?> GetUserSurveyDetailAsync(Guid resultId)
        {
            var result = await _context.SurveyResults
                .Include(r => r.User)
                .Include(r => r.Survey)
                .FirstOrDefaultAsync(r => r.Id == resultId);

            if (result == null) return null;

            var answers = await _context.SurveyAnswers
                .Where(a => a.UserId == result.UserId && a.Question.SurveyId == result.SurveyId)
                .Include(a => a.Question)
                .Include(a => a.Option)
                .OrderBy(x => x.Question.Order)
                .ThenBy(x => x.Option.Order)
                .Select(a => new AnswerDetailDto
                {
                    QuestionText = a.Question.QuestionText,
                    SelectedOptionText = a.Option.OptionText,
                    IsCorrect = a.Option.IsCorrect,
                }).ToListAsync();

            return new UserSurveyDetailDto
            {
                UserEmail = result.User.Email,
                SurveyTitle = result.Survey.Title,
                Score = result.Score,
                Answers = answers
            };
        }

        public async Task<List<SurveyMainResponseDto>> GetAllSurveysMainListAsync()
        {
            var surveys = await _repository.GetEntityListAsync(new string[] { "Questions" }, x => !x.IsDeleted);

            if (!surveys.Any()) return new List<SurveyMainResponseDto>();

            return _mapper.Map<List<SurveyMainResponseDto>>(surveys);
        }
    }
}