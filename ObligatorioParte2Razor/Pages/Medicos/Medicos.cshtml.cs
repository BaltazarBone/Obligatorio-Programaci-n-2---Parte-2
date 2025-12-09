using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ObligatorioParte2Razor.Dominio;

namespace ObligatorioParte2Razor.Pages.Admin
{
    public class MedicosModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public MedicosModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Medico> Medicos { get; set; }

        public async Task OnGetAsync()
        {
            // Listado de médicos ordenados por especialidad
            Medicos = await _context.Medicos
                .OrderBy(m => m.Especialidad)
                .ThenBy(m => m.Nombre)
                .ToListAsync();
        }
    }
}
