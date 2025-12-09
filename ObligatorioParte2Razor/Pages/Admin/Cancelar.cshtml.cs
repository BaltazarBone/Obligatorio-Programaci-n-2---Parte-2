using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ObligatorioParte2Razor.Dominio;

namespace ObligatorioParte2Razor.Pages.Admin
{
    public class CancelarModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CancelarModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ConsultaView> Consultas { get; set; } = new();

        [BindProperty]
        public int SelectedConsultaId { get; set; }

        public async Task OnGetAsync()
        {
            // Solo mostrar consultas agendadas
            Consultas = await _context.Consultas
                .Where(c => c.Estado == EstadoConsulta.Agendada)
                .Include(c => c.Medico)
                .Include(c => c.Paciente)
                .Select(c => new ConsultaView
                {
                    ID = c.ID,
                    Descripcion = $"ID {c.ID} - {c.Paciente.Nombre} con Dr/a {c.Medico.Nombre} {c.Medico.Apellido} ({c.FechaHora:dd/MM/yyyy HH:mm})"
                })
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var consulta = await _context.Consultas.FindAsync(SelectedConsultaId);
            if (consulta == null)
            {
                ModelState.AddModelError("", "Consulta no encontrada.");
                await OnGetAsync();
                return Page();
            }

            try
            {
                consulta.Cancelar();
                await _context.SaveChangesAsync();
                TempData["Exito"] = "Consulta cancelada correctamente.";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            await OnGetAsync();
            return Page();
        }

        public class ConsultaView
        {
            public int ID { get; set; }
            public string Descripcion { get; set; }
        }
    }
}
