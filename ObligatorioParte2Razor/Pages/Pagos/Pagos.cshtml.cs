using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ObligatorioParte2Razor.Dominio;

namespace ObligatorioParte2Razor.Pages.Admin
{
    public class PagosModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public PagosModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Consulta> Consultas { get; set; }
        public List<Paciente> Pacientes { get; set; }

        [BindProperty]
        public int SelectedConsultaId { get; set; }

        [BindProperty]
        public decimal Monto { get; set; }

        [BindProperty]
        public MetodoPago Metodo { get; set; } // usar el enum directamente

        public async Task OnGetAsync()
        {
            Pacientes = await _context.Pacientes.OrderBy(p => p.Nombre).ToListAsync();
            Consultas = await _context.Consultas
                .Where(c => c.Estado == EstadoConsulta.Agendada)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var consulta = await _context.Consultas.FindAsync(SelectedConsultaId);
            if (consulta == null)
            {
                ModelState.AddModelError("", "Consulta no encontrada");
                return Page();
            }

            var pago = new Pago
            {
                IdConsulta = SelectedConsultaId,
                Monto = Monto,
                FechaPago = DateTime.Now,
                Metodo = Metodo 
            };

            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();

            TempData["Exito"] = $"Pago registrado correctamente para la consulta {SelectedConsultaId}.";

            // emitir comprobante en consola
            Console.WriteLine($"Comprobante de pago: Consulta {SelectedConsultaId}, Monto {Monto:C}, Método {Metodo}, Fecha {DateTime.Now}");

            return RedirectToPage();
        }
    }
}
