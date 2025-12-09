using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ObligatorioParte2Razor.Dominio;

namespace ObligatorioParte2Razor.Pages.Admin
{
    public class AgendarModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AgendarModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Medico> Medicos { get; set; } = new();
        public List<Paciente> Pacientes { get; set; } = new();

        [BindProperty]
        public int SelectedMedicoId { get; set; }

        [BindProperty]
        public int SelectedPacienteId { get; set; }

        [BindProperty]
        public DateTime FechaHora { get; set; }

        public async Task OnGetAsync()
        {
            Medicos = await _context.Medicos.ToListAsync();
            Pacientes = await _context.Pacientes.OrderBy(p => p.Nombre).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Medicos = await _context.Medicos.ToListAsync();
            Pacientes = await _context.Pacientes.OrderBy(p => p.Nombre).ToListAsync();

            if (!ModelState.IsValid) return Page();

            var medico = await _context.Medicos.FindAsync(SelectedMedicoId);
            var paciente = await _context.Pacientes.FindAsync(SelectedPacienteId);
            if (medico == null || paciente == null)
            {
                ModelState.AddModelError("", "Médico o paciente no encontrado");
                return Page();
            }

            if (FechaHora < DateTime.Now.AddHours(24))
            {
                ModelState.AddModelError("", "Los turnos deben reservarse con al menos 24 horas de anticipación");
                return Page();
            }

            if (FechaHora.Minute % 30 != 0)
            {
                ModelState.AddModelError("", "Los turnos deben ser de 30 minutos exactos");
                return Page();
            }

            bool turnoExiste = _context.Consultas.Any(c => c.IdMedico == SelectedMedicoId && c.FechaHora == FechaHora);
            if (turnoExiste)
            {
                ModelState.AddModelError("", "El médico ya tiene un turno a esa hora");
                return Page();
            }

            var consulta = new Consulta
            {
                IdMedico = SelectedMedicoId,
                IdPaciente = SelectedPacienteId,
                FechaHora = FechaHora,
                Estado = EstadoConsulta.Agendada
            };

            _context.Consultas.Add(consulta);
            await _context.SaveChangesAsync();

            TempData["Exito"] = "Consulta agendada correctamente";
            return RedirectToPage();
        }
    }
}
