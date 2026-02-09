using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasAwareness.API.Entities;
using GasAwareness.API.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace GasAwareness.API.Repositories.Concrete
{
    public class SurveyResultRepository : GenericRepository<SurveyResult>, ISurveyResultRepository
    {
        public SurveyResultRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<SurveyResult>> GetAllUserSurveysAsync()
        {
            // return await _context.SurveyResults
            //     .Include(r => r.User)
            //     .Include(r => r.Survey)
            //     .OrderByDescending(r => r.CompletedAt)
            //     .Select(r => new UserSurveyListDto
            //     {
            //         ResultId = r.Id,
            //         UserEmail = r.User.Email,
            //         SurveyTitle = r.Survey.Title,
            //         CorrectCount = r.CorrectCount,
            //         WrongCount = r.WrongCount,
            //         Score = r.Score,
            //         CompletedAt = r.CompletedAt
            //     }).ToListAsync();

            return await _context.SurveyResults
                .Include(r => r.User)
                .Include(r => r.Survey)
                .OrderByDescending(r => r.CompletedAt).ToListAsync();
        }
    }
}