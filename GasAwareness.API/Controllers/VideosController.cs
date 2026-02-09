using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasAwareness.API.Helpers;
using GasAwareness.API.Models.Video.Requests;
using GasAwareness.API.Services.Video;
using GasAwareness.API.Services.Video.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GasAwareness.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideosController : ControllerBase
    {
        private readonly IVideoService _videoService;
        public VideosController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        /// <summary>
        /// Create a new video
        /// </summary>
        /// <param name="request">Video Create Request</param>
        [HttpPost]
        [Authorize(Policy = "RequireAdminAndEditor")]
        public async Task<IActionResult> CreateVideoAsync(CreateVideoRequestDto request)
        {
            var createdId = await _videoService.CreateVideoAsync(request);

            if(createdId == null) return BadRequest();

            return Ok(createdId);
        }

        /// <summary>
        /// Get videos by filter (if filter options are null then get all videos)
        /// </summary>
        /// <param name="categoryId">Category Id</param>
        /// <param name="ageGroupId">Age Group Id</param>
        /// <param name="subscriptionTypeId">Subscription Type Id</param>
        [HttpGet]
        [Authorize(Policy = "RequireAllRoles")]
        public async Task<IActionResult> GetVideosAsync([FromQuery] Guid? categoryId, [FromQuery] Guid? ageGroupId, [FromQuery] Guid? subscriptionTypeId)
        {
            var response = await _videoService.GetVideosAsync(new GetVideoRequestDto(categoryId, ageGroupId, subscriptionTypeId));

            return Ok(response);
        }

        /// <summary>
        /// Get users watched videos
        /// </summary>
        /// <param name="categoryId">Category Id</param>
        /// <param name="ageGroupId">Age Group Id</param>
        /// <param name="subscriptionTypeId">Subscription Type Id</param>
        [HttpGet("watched")]
        [Authorize(Policy = "RequireAllRoles")]
        public async Task<IActionResult> GetWatchedVideosAsync()
        {
            var response = await _videoService.GetWatchedVideosAsync(User.UserId());

            return Ok(response);
        }

        /// <summary>
        /// Get video details by id
        /// </summary>
        /// <param name="id">Video Id</param>
        [HttpGet("detail")]
        [Authorize(Policy = "RequireAllRoles")]
        public async Task<IActionResult> GetVideoDetailAsync([FromQuery] Guid id)
        {
            var response = await _videoService.GetVideoDetailAsync(User.UserId(), id);

            if (response == null) return NotFound();

            return Ok(response);
        }

        /// <summary>
        /// Delete video by id
        /// </summary>
        /// <param name="id">Video Id</param>
        [HttpDelete]
        [Authorize(Policy = "RequireAdminAndEditor")]
        public async Task<IActionResult> DeleteVideoAsync([FromQuery] Guid id)
        {
            var response = await _videoService.DeleteVideoAsync(id);

            if (!response) return BadRequest();

            return Ok();
        }

        /// <summary>
        /// Set video watched status
        /// </summary>
        /// <param name="id">Video Id</param>
        /// <param name="isWatched">Is Watched Status (True or False)</param>
        [HttpPost("setWatchStatus")]
        [Authorize(Policy = "RequireAllRoles")]
        public async Task<IActionResult> SetVideoWatchedStatusAsync([FromQuery] Guid id, [FromQuery] bool isWatched)
        {
            var response = await _videoService.SetVideoWatchedStatusAsync(User.UserId(), id, isWatched);

            if (!response) return BadRequest();

            return Ok();
        }
    }
}