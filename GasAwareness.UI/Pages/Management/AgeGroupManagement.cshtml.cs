using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GasAwareness.API.Models.AgeGroup.Requests;
using GasAwareness.API.Models.AgeGroup.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GasAwareness.UI.Pages.Management
{
    [Authorize(Policy = "RequireAdminAndEditor")]
    public class AgeGroupManagement : BasePageModel
    {
        [BindProperty]
        public CreateAgeGroupRequestDto Request { get; set; }

        public List<AgeGroupResponseDto> AgeGroups { get; set; } = new();

        private readonly IConfiguration _configuration;
        private HttpClient _client => CreateClient();

        public AgeGroupManagement(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            AgeGroups = await GetAgeGroupsAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            await CreateAgeGroupAsync(Request);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            if (id == Guid.Empty) return Page();

            await DeleteAgeGroupAsync(id);

            return RedirectToPage();
        }

        private async Task<bool> DeleteAgeGroupAsync(Guid id)
        {
            var url = $"{_configuration["APIBaseUrl"]}ageGroups?id={id}";
            var response = await _client.DeleteAsync(url);

            if (!response.IsSuccessStatusCode) return false;

            return true;
        }

        private async Task<bool> CreateAgeGroupAsync(CreateAgeGroupRequestDto request)
        {
            var url = _configuration["APIBaseUrl"] + "ageGroups";
            var content = new StringContent(
                JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(url, content);

            if (!response.IsSuccessStatusCode) return false;

            return true;
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
    }
}