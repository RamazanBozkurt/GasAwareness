using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GasAwareness.API.Models.Auth.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GasAwareness.UI.Pages
{
    public class Register : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        [BindProperty]
        public UserRegisterRequestDto RegisterDto { get; set; }

        public Register(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var client = _clientFactory.CreateClient();
            var loginUrl = _configuration["APIBaseUrl"] + "auth/register"; 
            var content = new StringContent(
                JsonSerializer.Serialize(RegisterDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(loginUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var isSuccess = response.IsSuccessStatusCode;

                if (isSuccess) return RedirectToPage("/Login");
            }

            ModelState.AddModelError(string.Empty, "Geçersiz e-posta veya şifre.");

            return Page();
        }
    }
}