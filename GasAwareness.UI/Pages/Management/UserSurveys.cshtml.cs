using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using GasAwareness.API.Models.Survey.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GasAwareness.UI.Pages.Management
{
    [Authorize(Policy = "RequireAdmin")]
    public class UserSurveys : BasePageModel
    {
        public List<UserSurveyListDto> UserSurveyList { get; set; } = new();

        private readonly IConfiguration _configuration;
        private HttpClient _client => CreateClient();
        public UserSurveys(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _configuration = configuration;
        }

        public async Task OnGetAsync()
        {
            UserSurveyList = await GetUserSurveysAsync();
        }

        private async Task<List<UserSurveyListDto>> GetUserSurveysAsync()
        {
            var url = _configuration["APIBaseUrl"] + "surveys/userSurveys";
            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<List<UserSurveyListDto>>(jsonString, options) ?? new List<UserSurveyListDto>();
            }

            return new List<UserSurveyListDto>();
        }
    }
}