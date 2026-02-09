using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using GasAwareness.API.Models.Survey.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GasAwareness.UI.Pages.Management
{
    public class UserSurveyDetail : BasePageModel
    {
        public UserSurveyDetailDto? Detail { get; set; }

        private readonly IConfiguration _configuration;
        private HttpClient _client => CreateClient();
        public UserSurveyDetail(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Detail = await GetUserSurveyDetailAsync(id);

            if (Detail == null) return NotFound();

            return Page();
        }

        private async Task<UserSurveyDetailDto?> GetUserSurveyDetailAsync(Guid id)
        {
            var url = _configuration["APIBaseUrl"] + $"surveys/userSurveys/detail?resultId={id}";
            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<UserSurveyDetailDto>(jsonString, options) ?? null;
            }

            return null;
        }
    }
}