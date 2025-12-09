using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ObligatorioParte2Razor.Dominio;

namespace ObligatorioParte2Razor.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LoginModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Paciente Input { get; set; } // Podemos usar la misma clase Paciente

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Buscar usuario en la base de datos
            var usuario = _context.Pacientes
                .FirstOrDefault(u => u.NombreUsuario == Input.NombreUsuario && u.Contrasenia == Input.Contrasenia);

            if (usuario == null)
            {
                ModelState.AddModelError("", "Usuario o contraseña incorrectos");
                return Page();
            }

            // Guardar usuario en sesión
            HttpContext.Session.SetInt32("UsuarioId", usuario.ID);
            HttpContext.Session.SetString("Rol", "Paciente");

            return RedirectToPage("/Index");
        }
    }
}
