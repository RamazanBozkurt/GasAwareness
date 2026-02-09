using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasAwareness.API.Models.Video.Requests;
using GasAwareness.API.Models.Video.Responses;

namespace GasAwareness.API.Services.Video.Abstract
{
    public interface IVideoService
    {
        Task<Guid?> CreateVideoAsync(CreateVideoRequestDto request);
        Task<List<VideoResponseDto>> GetVideosAsync(GetVideoRequestDto request);
        Task<List<VideoResponseDto>> GetWatchedVideosAsync(string userId);
        Task<VideoDetailResponseDto?> GetVideoDetailAsync(string userId, Guid id);
        Task<bool> DeleteVideoAsync(Guid id);
        Task<bool> SetVideoWatchedStatusAsync(string userId, Guid id, bool isWatched);
    }
}