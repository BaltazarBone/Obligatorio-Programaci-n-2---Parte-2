using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ObligatorioParte2Razor.Dominio;

namespace ObligatorioParte2Razor.Pages.Admin
{
    public class PacientesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public PacientesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Paciente> Pacientes { get; set; }

        public async Task OnGetAsync()
        {
            // Listado de pacientes ordenados alfabéticamente por nombre
            Pacientes = await _context.Pacientes
                .OrderBy(p => p.Nombre)
                .ThenBy(p => p.Apellido)
                .ToListAsync();
        }
    }
}
