using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasAwareness.API.Entities;
using GasAwareness.API.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace GasAwareness.API.Repositories.Concrete
{
    public class SurveyRepository : GenericRepository<Survey>, ISurveyRepository
    {
        public SurveyRepository(DataContext context) : base(context)
        {
        }

        public async Task<Survey?> GetSurveyAsync(Guid id)
        {
            var survey = await _context.Surveys
                .Include(x => x.Questions.OrderBy(q => q.Order))
                .ThenInclude(x => x.Options.OrderBy(o => o.Order))
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            return survey;
        }

        public async Task<bool> IsSurveySolvedByUser(string userId, Guid surveyId)
        {
            return await _context.SurveyAnswers.AnyAsync(x => x.UserId == userId && x.Question.SurveyId == surveyId);
        }
    }
}