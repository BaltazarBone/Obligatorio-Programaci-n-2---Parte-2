using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ObligatorioParte2Razor.Dominio;
using System.Linq;

namespace ObligatorioParte2Razor.Pages.Admin
{
    public class ConsultasFrecuentesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ConsultasFrecuentesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<EspecialidadCount> ConsultasPorEspecialidad { get; set; }

        public async Task OnGetAsync()
        {
            ConsultasPorEspecialidad = await _context.Consultas
                .Include(c => c.IdMedico) 
                .Join(_context.Medicos,
                      c => c.IdMedico,
                      m => m.ID,
                      (c, m) => new { m.Especialidad })
                .GroupBy(x => x.Especialidad)
                .Select(g => new EspecialidadCount
                {
                    Especialidad = g.Key,
                    Cantidad = g.Count()
                })
                .OrderByDescending(x => x.Cantidad)
                .ToListAsync();
        }
    }

    public class EspecialidadCount
    {
        public string Especialidad { get; set; }
        public int Cantidad { get; set; }
    }
}
