using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GasAwareness.API.Entities;
using GasAwareness.API.Models.Video.Requests;
using GasAwareness.API.Models.Video.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GasAwareness.UI.Pages
{
    [Authorize(Policy = "RequireAllRoles")]
    public class VideoDetail : BasePageModel
    {
        public VideoDetailResponseDto? VideoDetailDto { get; set; }
        public List<VideoResponseDto> VideoModels { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public GetVideoRequestDto RequestDto { get; set; } = new(null, null, null);

        private readonly IConfiguration _configuration;
        private HttpClient _client => CreateClient();
        public VideoDetail(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            VideoDetailDto = await GetVideoDetailAsync(id);
            VideoModels = await GetVideosAsync(RequestDto, id);

            return Page();
        }

        private async Task<List<VideoResponseDto>> GetVideosAsync(GetVideoRequestDto request, Guid currentVideoId)
        {
            var url = $"{_configuration["APIBaseUrl"]}videos?categoryId={request.CategoryId}&ageGroupId={request.AgeGroupId}&subscriptionTypeId={request.AgeGroupId}";
            var response = await _client.GetAsync(url);

            CheckResponse(response);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var videos = JsonSerializer.Deserialize<List<VideoResponseDto>>(jsonString, options) ?? new List<VideoResponseDto>();

                if (videos.Any())
                {
                    var currentVideo = videos.FirstOrDefault(x => x.Id == currentVideoId);
                    videos.Remove(currentVideo);
                }

                return videos;
            }

            return new List<VideoResponseDto>();
        }

        public IActionResult OnPostRedirectToSurvey(Guid? surveyId)
        {
            return RedirectToPage("/SurveySolve", new { id = surveyId });
        }

        public async Task<IActionResult> OnPostSetVideoAsWatched(Guid? videoId)
        {
            var url = $"{_configuration["APIBaseUrl"]}videos/setWatchStatus?id={videoId}&isWatched=true";
            //var content = new StringContent(null, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, null);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSetVideoAsNotWatched(Guid? videoId)
        {
            var url = $"{_configuration["APIBaseUrl"]}videos/setWatchStatus?id={videoId}&isWatched=false";
            //var content = new StringContent(null, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, null);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage();
            }

            return RedirectToPage();
        }

        public IActionResult OnPostRedirectWithSelectedCategory(Guid categoryId)
        {
            return RedirectToPage("/Videos", new
            {
                CategoryId = categoryId
            });
        }

        public IActionResult OnPostRedirectWithSelectedAgeGroup(Guid ageGroupId)
        {
            return RedirectToPage("/Videos", new
            {
                AgeGroupId = ageGroupId
            });
        }

        public IActionResult OnPostRedirectWithSelectedSubscriptionType(Guid subscriptionTypeId)
        {
            return RedirectToPage("/Videos", new
            {
                SubscriptionTypeId = subscriptionTypeId
            });
        }

        private async Task<VideoDetailResponseDto?> GetVideoDetailAsync(Guid id)
        {
            var url = $"{_configuration["APIBaseUrl"]}videos/detail?id={id}";
            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<VideoDetailResponseDto>(jsonString, options) ?? null;
            }

            return null;
        }
    }
}