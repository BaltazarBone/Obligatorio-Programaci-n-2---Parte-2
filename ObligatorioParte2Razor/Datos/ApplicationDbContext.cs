using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ObligatorioParte2Razor.Dominio;

namespace ObligatorioParte2Razor.Dominio
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Administrativo> Administrativos { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Pago> Pagos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones básicas
            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.HasKey(p => p.ID);
                entity.Property(p => p.Email).IsRequired().HasMaxLength(200);
                entity.Property(p => p.NumeroDocumento).IsRequired().HasMaxLength(20);
            });

            modelBuilder.Entity<Medico>(entity =>
            {
                entity.HasKey(m => m.ID);
                entity.Property(m => m.Matricula).IsRequired().HasMaxLength(50);
                entity.Property(m => m.Especialidad).IsRequired().HasMaxLength(100);
                // DiasAtencion y HorariosDisponibles: serializaremos como JSON (si usas SQL Server 2022, soportarás NVARCHAR)
                entity.Property(m => m.DiasAtencion).HasConversion(
                    v => string.Join(";", v),
                    v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
                );
                entity.Property(m => m.HorariosDisponibles).HasConversion(
                    v => string.Join(";", v.Select(ts => ts.ToString(@"hh\:mm"))),
                    v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).Select(s => TimeSpan.Parse(s)).ToList()
                );
            });

            modelBuilder.Entity<Administrativo>(entity =>
            {
                entity.HasKey(a => a.ID);
                entity.Property(a => a.NumeroDocumento).IsRequired().HasMaxLength(20);
            });

            modelBuilder.Entity<Consulta>(entity =>
            {
                entity.HasKey(c => c.ID);
                entity.Property(c => c.FechaHora).IsRequired();
                // relaciones por FK si se quiere
                entity.HasOne<Paciente>().WithMany().HasForeignKey("IdPaciente").OnDelete(DeleteBehavior.Restrict);
                entity.HasOne<Medico>().WithMany().HasForeignKey("IdMedico").OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.HasKey(p => p.ID);
                entity.Property(p => p.Monto).HasColumnType("decimal(10,2)");
                entity.HasOne<Consulta>().WithMany().HasForeignKey("IdConsulta").OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
