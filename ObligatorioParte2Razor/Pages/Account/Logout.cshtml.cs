using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ObligatorioParte2Razor.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Limpiar toda la sesión
            HttpContext.Session.Clear();

            // Redirigir al Index después de cerrar sesión
            return RedirectToPage("/Index");
        }
    }
}
