using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GasAwareness.UI.Pages.Management
{
    [Authorize(Policy = "RequireAdminAndEditor")]
    public class Management : PageModel
    {
        

        public Management()
        {
        }

        public void OnGet()
        {
            RedirectToPage("/Management/VideoManagement");
        }
    }
}