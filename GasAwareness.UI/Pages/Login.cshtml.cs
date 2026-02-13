using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GasAwareness.API.Models.Auth.Requests;
using GasAwareness.API.Models.Auth.Responses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GasAwareness.UI.Pages
{
    public class Login : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        [BindProperty]
        public UserLoginRequestDto LoginDto { get; set; }

        public Login(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var client = _clientFactory.CreateClient();
            var loginUrl = _configuration["APIBaseUrl"] + "auth/login";

            Console.WriteLine("REQUEST SENDED TO: " + loginUrl);

            var content = new StringContent(
                JsonSerializer.Serialize(LoginDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(loginUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<UserLoginResponseDto>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, LoginDto.Email),
                    new Claim("JWTToken", result.Token)
                };

                foreach (var role in result.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToPage("/Videos");
            }

            ModelState.AddModelError(string.Empty, "Geçersiz e-posta veya şifre.");

            return Page();
        }
    }
}