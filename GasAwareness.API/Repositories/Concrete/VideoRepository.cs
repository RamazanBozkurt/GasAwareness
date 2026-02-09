using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasAwareness.API.Entities;
using GasAwareness.API.Models.Video.Requests;
using GasAwareness.API.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace GasAwareness.API.Repositories.Concrete
{
    public class VideoRepository : GenericRepository<Video>, IVideoRepository
    {
        private readonly DataContext _context;
        public VideoRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserVideo?> GetUserVideoAsync(string userId, Guid videoId)
        {
            return await _context.UserVideos.FirstOrDefaultAsync(x => x.UserId == userId && x.VideoId == videoId && !x.IsDeleted);
        }

        public async Task<IQueryable<Video>> GetVideosAsync(GetVideoRequestDto request)
        {
            var query = _context.Videos.Where(x => !x.IsDeleted).OrderByDescending(x => x.CreatedAt).AsQueryable();

            if (request.CategoryId.HasValue)
            {
                query = query.Where(v => !v.IsDeleted && v.VideoCategories.Any(vc => vc.CategoryId == request.CategoryId)).OrderByDescending(x => x.CreatedAt);
            }

            if (request.AgeGroupId.HasValue)
            {
                query = query.Where(v => !v.IsDeleted && v.VideoAgeGroups.Any(va => va.AgeGroupId == request.AgeGroupId)).OrderByDescending(x => x.CreatedAt);
            }

            if (request.SubscriptionTypeId.HasValue)
            {
                query = query.Where(v => !v.IsDeleted && v.VideoSubscriptionTypes.Any(vs => vs.SubscriptionTypeId == request.SubscriptionTypeId)).OrderByDescending(x => x.CreatedAt);
            }

            return query;
        }
    }
}