using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasAwareness.API.Models.Survey.Requests;
using GasAwareness.API.Models.Survey.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GasAwareness.UI.Pages
{
    public class SurveySolve : BasePageModel
    {
        [BindProperty]
        public SurveyResponseDto Survey { get; set; }

        [BindProperty]
        public List<UserSelectedAnswerRequestDto> UserAnswers { get; set; } = new();

        public SurveyResultResponseDto Result { get; set; }

        private readonly IConfiguration _configuration;
        private HttpClient _client => CreateClient();

        public SurveySolve(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
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

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            //if (!ModelState.IsValid) return Page();

            var submitRequest = new SurveySubmitRequestDto
            {
                SurveyId = id,
                Answers = UserAnswers.Select(x => new QuestionAnswerRequestDto
                {
                    QuestionId = x.QuestionId,
                    OptionId = x.OptionId
                }).ToList()
            };

            var url = $"{_configuration["APIBaseUrl"]}surveys/submit";
            var response = await _client.PostAsJsonAsync(url, submitRequest);

            if (response.IsSuccessStatusCode)
            {
                Result = await response.Content.ReadFromJsonAsync<SurveyResultResponseDto>();

                Survey = await _client.GetFromJsonAsync<SurveyResponseDto>($"{_configuration["APIBaseUrl"]}surveys?id={id}");

                return Page();
            }

            ModelState.AddModelError("", "Anket gönderilirken bir hata oluştu.");

            return Page();
        }
    }
}