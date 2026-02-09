using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GasAwareness.UI.Pages;

public class IndexModel : PageModel
{
    public IActionResult OnGet()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToPage("/Login");   
        }
        else
        {
            return RedirectToPage("/Videos");   
        }
    }
}
