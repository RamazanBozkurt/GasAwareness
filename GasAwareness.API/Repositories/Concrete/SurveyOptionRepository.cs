using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasAwareness.API.Entities;
using GasAwareness.API.Repositories.Abstract;

namespace GasAwareness.API.Repositories.Concrete
{
    public class SurveyOptionRepository : GenericRepository<SurveyOption>, ISurveyOptionRepository
    {
        public SurveyOptionRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<SurveyOption>> GetSurveyOptionsAsync(Guid surveyId)
        {
            //var surveyOptions = _context.SurveyOptions.Where(o => o.Question.SurveyId == surveyId).ToList(); 

            return _context.SurveyOptions
                .Where(o => _context.SurveyQuestions.Where(q => q.SurveyId == surveyId).Select(q => q.Id).Contains(o.QuestionId))
                .ToList();
        }
    }
}