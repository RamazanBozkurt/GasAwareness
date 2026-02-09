using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GasAwareness.API.Models.AgeGroup.Responses;
using GasAwareness.API.Models.Category.Responses;
using GasAwareness.API.Models.SubscriptionType.Responses;
using GasAwareness.API.Models.Survey.Responses;
using GasAwareness.API.Models.Video.Requests;
using GasAwareness.API.Models.Video.Responses;
using MediaToolkit;
using MediaToolkit.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GasAwareness.UI.Pages.Management
{
    [Authorize(Policy = "RequireAdminAndEditor")]
    public class VideoManagement : BasePageModel
    {
        [BindProperty]
        public CreateVideoRequestDto RequestDto { get; set; }

        [BindProperty]
        public IFormFile VideoFile { get; set; }

        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public List<CategoryResponseDto> Categories { get; set; } = new();
        public List<AgeGroupResponseDto> AgeGroups { get; set; } = new();
        public List<SubscriptionTypeResponseDto> SubscriptionTypes { get; set; } = new();
        public List<VideoResponseDto> VideoModels { get; set; } = new();
        public List<SurveyMainResponseDto> Surveys { get; set; }

        private readonly IConfiguration _configuration;
        private HttpClient _client => CreateClient();

        public VideoManagement(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            //var client = _clientFactory.CreateClient();

            Categories = await GetCategoriesAsync();
            AgeGroups = await GetAgeGroupsAsync();
            SubscriptionTypes = await GetSubscriptionTypesAsync();
            Surveys = await GetSurveysAsync();

            VideoModels = await GetVideosAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var fileUploadResponseDto = await UploadFileAsync();

            RequestDto.Id = fileUploadResponseDto!.Id;
            RequestDto.Path = fileUploadResponseDto.Path;

            var apiResponse = await SaveVideoAsync(RequestDto);


            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            if (id == Guid.Empty) return Page();

            await DeleteVideoAsync(id);

            return RedirectToPage();
        }

        private async Task<bool> DeleteVideoAsync(Guid id)
        {
            var url = $"{_configuration["APIBaseUrl"]}videos?id={id}";
            var response = await _client.DeleteAsync(url);

            if (!response.IsSuccessStatusCode) return false;

            return true;
        }

        private async Task<List<VideoResponseDto>> GetVideosAsync()
        {
            var url = $"{_configuration["APIBaseUrl"]}videos?categoryId=&ageGroupId=&subscriptionTypeId=";
            var response = await _client.GetAsync(url);

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

        private async Task<bool> SaveVideoAsync(CreateVideoRequestDto request)
        {
            var url = _configuration["APIBaseUrl"] + "videos";
            var content = new StringContent(
                JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(url, content);

            if (!response.IsSuccessStatusCode) return false;

            return true;
        }

        private async Task<FileUploadResponseDto?> UploadFileAsync()
        {
            if (VideoFile == null || VideoFile.Length == 0) return null;

            //if (ImageFile == null || ImageFile.Length == 0) 

            var extension = Path.GetExtension(VideoFile.FileName).ToLower();

            string contentId = Guid.NewGuid().ToString();

            var localePath = Path.Combine("files", "contents", contentId);

            var folderName = Path.Combine("wwwroot", localePath);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (!Directory.Exists(pathToSave))
                Directory.CreateDirectory(pathToSave);

            var fileName = contentId + extension;
            var fullPath = Path.Combine(pathToSave, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await VideoFile.CopyToAsync(stream);
            }

            var dbPath = Path.Combine(localePath, fileName);

            return new FileUploadResponseDto
            {
                Id = new Guid(contentId),
                Path = "/" + dbPath,
                FullPath = fullPath
            };
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

        private async Task<List<SurveyMainResponseDto>> GetSurveysAsync()
        {
            var url = _configuration["APIBaseUrl"] + "surveys/main";
            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<List<SurveyMainResponseDto>>(jsonString, options) ?? new List<SurveyMainResponseDto>();
            }

            return new List<SurveyMainResponseDto>();
        }
    }

    // Todo: Move to models folder 

    public class FileUploadResponseDto
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public string FullPath { get; set; }
    }
}