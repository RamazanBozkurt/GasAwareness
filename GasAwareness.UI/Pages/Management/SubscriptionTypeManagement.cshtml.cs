using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GasAwareness.API.Models.SubscriptionType.Requests;
using GasAwareness.API.Models.SubscriptionType.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GasAwareness.UI.Pages.Management
{
    [Authorize(Policy = "RequireAdminAndEditor")]
    public class SubscriptionTypeManagement : BasePageModel
    {
        [BindProperty]
        public CreateSubscriptionTypeRequestDto Request { get; set; }

        public List<SubscriptionTypeResponseDto> SubscriptionTypes { get; set; } = new();

        private readonly IConfiguration _configuration;
        private HttpClient _client => CreateClient();

        public SubscriptionTypeManagement(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            SubscriptionTypes = await GetSubscriptionTypesAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await CreateSubscriptionTypeAsync(Request);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            if (id == Guid.Empty) return Page();

            await DeleteSubscriptionTypeAsync(id);

            return RedirectToPage();
        }

        private async Task<bool> DeleteSubscriptionTypeAsync(Guid id)
        {
            var url = $"{_configuration["APIBaseUrl"]}subscriptionTypes?id={id}";
            var response = await _client.DeleteAsync(url);

            if (!response.IsSuccessStatusCode) return false;

            return true;
        }

        private async Task<bool> CreateSubscriptionTypeAsync(CreateSubscriptionTypeRequestDto request)
        {
            var url = _configuration["APIBaseUrl"] + "subscriptionTypes";
            var content = new StringContent(
                JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(url, content);

            if (!response.IsSuccessStatusCode) return false;

            return true;
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