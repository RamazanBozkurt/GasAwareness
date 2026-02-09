using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasAwareness.API.Entities;
using GasAwareness.API.Models.Video.Requests;

namespace GasAwareness.API.Repositories.Abstract
{
    public interface IVideoRepository : IGenericRepository<Video>
    {
        Task<IQueryable<Video>> GetVideosAsync(GetVideoRequestDto request);
        Task<UserVideo?> GetUserVideoAsync(string userId, Guid videoId); 
    }
}