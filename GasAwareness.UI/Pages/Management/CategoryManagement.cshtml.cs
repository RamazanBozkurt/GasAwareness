using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GasAwareness.API.Models.Category.Requests;
using GasAwareness.API.Models.Category.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GasAwareness.UI.Pages.Management
{
    [Authorize(Policy = "RequireAdminAndEditor")]
    public class CategoryManagement : BasePageModel
    {
        [BindProperty]
        public CreateCategoryRequestDto Request { get; set; }

        public List<CategoryResponseDto> Categories { get; set; } = new();

        private readonly IConfiguration _configuration;
        private HttpClient _client => CreateClient();

        public CategoryManagement(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Categories = await GetCategoriesAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            await CreateCategoryAsync(Request);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            if (id == Guid.Empty) return Page();

            await DeleteCategoryAsync(id);

            return RedirectToPage();
        }

        private async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var url = $"{_configuration["APIBaseUrl"]}categories?id={id}";
            var response = await _client.DeleteAsync(url);

            if (!response.IsSuccessStatusCode) return false;

            return true;
        }

        private async Task<bool> CreateCategoryAsync(CreateCategoryRequestDto request)
        {
            var url = _configuration["APIBaseUrl"] + "categories";
            var content = new StringContent(
                JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(url, content);

            if (!response.IsSuccessStatusCode) return false;

            return true;
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
    }
}