using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ObligatorioParte2Razor.Dominio;

namespace ObligatorioParte2Razor.Pages.Admin
{
    public class DisponibilidadModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DisponibilidadModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<string> Especialidades { get; set; } = new();
        public List<Medico> Medicos { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string SelectedEspecialidad { get; set; }

        public async Task OnGetAsync()
        {
            Especialidades = await _context.Medicos
                .Select(m => m.Especialidad)
                .Distinct()
                .OrderBy(e => e)
                .ToListAsync();

            Medicos = string.IsNullOrEmpty(SelectedEspecialidad)
                ? new List<Medico>()
                : await _context.Medicos
                    .Where(m => m.Especialidad == SelectedEspecialidad)
                    .ToListAsync();
        }
    }
}
