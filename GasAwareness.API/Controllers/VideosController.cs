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

        [HttpPost]
        [Authorize(Policy = "RequireAdminAndEditor")]
        public async Task<IActionResult> CreateVideoAsync(CreateVideoRequestDto request)
        {
            var createdId = await _videoService.CreateVideoAsync(request);

            if(createdId == null) return BadRequest();

            return Ok(createdId);
        }

        [HttpGet]
        [Authorize(Policy = "RequireAllRoles")]
        public async Task<IActionResult> GetVideosAsync([FromQuery] Guid? categoryId, [FromQuery] Guid? ageGroupId, [FromQuery] Guid? subscriptionTypeId)
        {
            var response = await _videoService.GetVideosAsync(new GetVideoRequestDto(categoryId, ageGroupId, subscriptionTypeId));

            return Ok(response);
        }

        [HttpGet("detail")]
        [Authorize(Policy = "RequireAllRoles")]
        public async Task<IActionResult> GetVideoDetailAsync([FromQuery] Guid id)
        {
            var response = await _videoService.GetVideoDetailAsync(User.UserId(), id);

            if (response == null) return NotFound();

            return Ok(response);
        }

        [HttpDelete]
        [Authorize(Policy = "RequireAdminAndEditor")]
        public async Task<IActionResult> DeleteVideoAsync([FromQuery] Guid id)
        {
            var response = await _videoService.DeleteVideoAsync(id);

            if (!response) return BadRequest();

            return Ok();
        }

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