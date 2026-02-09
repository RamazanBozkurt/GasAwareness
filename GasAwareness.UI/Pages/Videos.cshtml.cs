using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using GasAwareness.API.Models.AgeGroup.Responses;
using GasAwareness.API.Models.Category.Responses;
using GasAwareness.API.Models.SubscriptionType.Responses;
using GasAwareness.API.Models.Video.Requests;
using GasAwareness.API.Models.Video.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GasAwareness.UI.Pages
{
    [Authorize(Policy = "RequireAllRoles")]
    public class Videos : BasePageModel
    {
        [BindProperty(SupportsGet = true)]
        public GetVideoRequestDto RequestDto { get; set; } = new(null, null, null);

        public List<CategoryResponseDto> Categories { get; set; } = new();
        public List<AgeGroupResponseDto> AgeGroups { get; set; } = new();
        public List<SubscriptionTypeResponseDto> SubscriptionTypes { get; set; } = new();
        public List<VideoResponseDto> VideoModels { get; set; } = new();

        public List<VideoResponseDto> WatchedVideos { get; set; } = new();

        private readonly IConfiguration _configuration;
        private HttpClient _client => CreateClient();

        public Videos(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Categories = await GetCategoriesAsync();
            AgeGroups = await GetAgeGroupsAsync();
            SubscriptionTypes = await GetSubscriptionTypesAsync();
            VideoModels = await GetVideosAsync(RequestDto);
            WatchedVideos = await GetWatchedVideosAsync(RequestDto);

            return Page();
        }

        private async Task<List<VideoResponseDto>> GetVideosAsync(GetVideoRequestDto request)
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

                return JsonSerializer.Deserialize<List<VideoResponseDto>>(jsonString, options) ?? new List<VideoResponseDto>();
            }

            return new List<VideoResponseDto>();
        }

        private async Task<List<VideoResponseDto>> GetWatchedVideosAsync(GetVideoRequestDto request)
        {
            var url = $"{_configuration["APIBaseUrl"]}videos/watched";
            var response = await _client.GetAsync(url);

            CheckResponse(response);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<List<VideoResponseDto>>(jsonString, options) ?? new List<VideoResponseDto>();
            }

            return new List<VideoResponseDto>();
        }

        private async Task<List<CategoryResponseDto>> GetCategoriesAsync()
        {
            var url = _configuration["APIBaseUrl"] + "categories";
            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<List<CategoryResponseDto>>(jsonString, options) ?? new List<CategoryResponseDto>();
            }

            return new List<CategoryResponseDto>();
        }

        private async Task<List<AgeGroupResponseDto>> GetAgeGroupsAsync()
        {
            var url = _configuration["APIBaseUrl"] + "ageGroups";
            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<List<AgeGroupResponseDto>>(jsonString, options) ?? new List<AgeGroupResponseDto>();
            }

            return new List<AgeGroupResponseDto>();
        }

        private async Task<List<SubscriptionTypeResponseDto>> GetSubscriptionTypesAsync()
        {
            var url = _configuration["APIBaseUrl"] + "subscriptionTypes";
            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<List<SubscriptionTypeResponseDto>>(jsonString, options) ?? new List<SubscriptionTypeResponseDto>();
            }

            return new List<SubscriptionTypeResponseDto>();
        }
    }
}