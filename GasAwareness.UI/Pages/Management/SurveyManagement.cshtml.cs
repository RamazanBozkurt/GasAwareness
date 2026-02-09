using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GasAwareness.API.Models.Survey.Requests;
using GasAwareness.API.Models.Survey.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GasAwareness.UI.Pages.Management
{
    [Authorize(Policy = "RequireAdmin")]
    public class SurveyManagement : BasePageModel
    {
        public List<SurveyMainResponseDto> Surveys { get; set; } = new();

        private readonly IConfiguration _configuration;
        private HttpClient _client => CreateClient();

        public SurveyManagement(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public CreateSurveyRequestDto SurveyRequest { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            Surveys = await GetSurveysAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (SurveyRequest.Questions == null || !SurveyRequest.Questions.Any())
            {
                ModelState.AddModelError("", "En az bir soru eklemelisiniz.");
                return Page();
            }

            try
            {
                var url = $"{_configuration["APIBaseUrl"]}surveys";
                var jsonContent = new StringContent(JsonSerializer.Serialize(SurveyRequest), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(url, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage();
                }
                else
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"API Error: {errorMsg}");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Sistem Hatası: {ex.Message}");
            }

            return Page();
        }

        public IActionResult OnPostRedirectToSurveyDetail(Guid surveyId)
        {
            return RedirectToPage("/Management/SurveyDetail", new { id = surveyId });
        }

        private async Task<List<SurveyMainResponseDto>> GetSurveysAsync()
        {
            var url = _configuration["APIBaseUrl"] + "surveys/main/all";
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
}