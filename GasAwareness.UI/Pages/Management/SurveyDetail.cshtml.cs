using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasAwareness.API.Models.Survey.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GasAwareness.UI.Pages.Management
{
    [Authorize(Policy = "RequireAdmin")]
    public class SurveyDetail : BasePageModel
    {
        public SurveyResponseDto Survey { get; set; }

        private HttpClient _client => CreateClient();
        private readonly IConfiguration _configuration;
        public SurveyDetail(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var url = $"{_configuration["APIBaseUrl"]}surveys?id={id}";

            var response = await _client.GetAsync(url);
            if (!response.IsSuccessStatusCode) return RedirectToPage("/Error");

            Survey = await response.Content.ReadFromJsonAsync<SurveyResponseDto>();

            return Page();
        }
    }
}