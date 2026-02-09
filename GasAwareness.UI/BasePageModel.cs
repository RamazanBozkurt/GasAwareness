using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GasAwareness.UI.Pages
{
    public class BasePageModel : PageModel
    {
        protected readonly IHttpClientFactory _clientFactory;
        public BasePageModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        protected HttpClient CreateClient()
        {
            var client = _clientFactory.CreateClient();
            var token = User.FindFirst("JWTToken")?.Value;

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }

        protected void CheckResponse(HttpResponseMessage response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) { Response.Redirect("/Login"); }
        }
    }
}