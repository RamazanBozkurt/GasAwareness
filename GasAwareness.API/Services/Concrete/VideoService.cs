using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GasAwareness.API.Entities;
using GasAwareness.API.Models.Video.Requests;
using GasAwareness.API.Models.Video.Responses;
using GasAwareness.API.Repositories;
using GasAwareness.API.Repositories.Abstract;
using GasAwareness.API.Services.Abstract;
using GasAwareness.API.Services.Video.Abstract;

namespace GasAwareness.API.Services.Video.Concrete
{
    public class VideoService : IVideoService
    {
        private readonly IVideoRepository _repository;
        private readonly IUserVideoRepository _userVideoRepository;
        private readonly IMapper _mapper;
        private readonly ISurveyService _surveyService;

        public VideoService(IVideoRepository repository, IMapper mapper, IUserVideoRepository userVideoRepository, ISurveyService surveyService)
        {
            _repository = repository;
            _mapper = mapper;
            _userVideoRepository = userVideoRepository;
            _surveyService = surveyService;
        }

        public async Task<Guid?> CreateVideoAsync(CreateVideoRequestDto request)
        {
            var entity = _mapper.Map<Entities.Video>(request);

            entity.VideoCategories.Add(new Entities.VideoCategory
            {
                CategoryId = request.CategoryId
            });

            entity.VideoAgeGroups.Add(new Entities.VideoAgeGroup
            {
                AgeGroupId = request.AgeGroupId
            });

            entity.VideoSubscriptionTypes.Add(new Entities.VideoSubscriptionType
            {
                SubscriptionTypeId = request.SubscriptionTypeId
            });

            var createdVideo = await _repository.CreateEntityAsync(entity);
            var surveyResponse = await _surveyService.UpdateAsync(createdVideo.Id, request.SurveyId);

            if (!surveyResponse) return null;

            return createdVideo?.Id;
        }

        public async Task<bool> DeleteVideoAsync(Guid id)
        {
            return await _repository.DeleteEntityAsync(id);
        }

        public async Task<VideoDetailResponseDto?> GetVideoDetailAsync(string userId, Guid id)
        {
            var video = await _repository.GetEntityAsync(new string[] { "VideoCategories.Category", "VideoSubscriptionTypes.SubscriptionType", "VideoAgeGroups.AgeGroup", "UserVideos", "Surveys" }, x => !x.IsDeleted && x.Id == id);

            if (video == null) return null;

            var response = _mapper.Map<VideoDetailResponseDto>(video);
            response.IsWatched = video.UserVideos.Any(x => x.UserId == userId && x.VideoId == id && x.IsWatched);
            response.IsSurveySolved = response.SurveyId == null ? false : await _surveyService.IsSurveySolvedByUser(userId, response.SurveyId.Value);

            return response;
        }

        public async Task<List<VideoResponseDto>> GetVideosAsync(GetVideoRequestDto request)
        {
            var videos = (await _repository.GetVideosAsync(request)).OrderByDescending(x => x.CreatedAt);

            if (!videos.Any()) return new List<VideoResponseDto>();

            return _mapper.Map<List<VideoResponseDto>>(videos);
        }

        public async Task<bool> SetVideoWatchedStatusAsync(string userId, Guid id, bool isWatched)
        {
            var userVideo = await _repository.GetUserVideoAsync(userId, id);

            if (!isWatched)
            {
                if (userVideo == null) return true;

                userVideo.IsWatched = false;
                var updateResult = await _userVideoRepository.UpdateEntityAsync(userVideo);

                return updateResult != null;
            }

            if (userVideo == null)
            {
                var createResult = await _userVideoRepository.CreateEntityAsync(new UserVideo
                {
                    UserId = userId.ToString(),
                    VideoId = id,
                    IsWatched = true
                });

                return createResult != null;
            }

            if (userVideo.IsWatched) return true;

            userVideo.IsWatched = true;
            var result = await _userVideoRepository.UpdateEntityAsync(userVideo);

            return result != null;
        }
    }
}