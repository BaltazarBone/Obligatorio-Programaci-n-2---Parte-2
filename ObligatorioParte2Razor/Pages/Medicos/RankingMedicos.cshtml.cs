using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ObligatorioParte2Razor.Dominio;
using System.Linq;

namespace ObligatorioParte2Razor.Pages.Admin
{
    public class RankingMedicosModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RankingMedicosModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<MedicoRanking> Ranking { get; set; }

        public async Task OnGetAsync()
        {
            Ranking = await _context.Consultas
                .Join(_context.Medicos,
                      c => c.IdMedico,
                      m => m.ID,
                      (c, m) => new { m.Nombre, m.Apellido, m.Especialidad })
                .GroupBy(x => new { x.Nombre, x.Apellido, x.Especialidad })
                .Select(g => new MedicoRanking
                {
                    MedicoNombre = $"{g.Key.Nombre} {g.Key.Apellido}",
                    Especialidad = g.Key.Especialidad,
                    Cantidad = g.Count()
                })
                .OrderByDescending(x => x.Cantidad)
                .ToListAsync();
        }
    }

    public class MedicoRanking
    {
        public string MedicoNombre { get; set; }
        public string Especialidad { get; set; }
        public int Cantidad { get; set; }
    }
}
