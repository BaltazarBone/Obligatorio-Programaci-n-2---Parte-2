using System;

namespace ObligatorioParte2Razor.Dominio
{
    public enum EstadoConsulta
    {
        Agendada,
        Realizada,
        Cancelada
    }

    public class Consulta
    {
        private static int contadorID = 1;
        public int ID { get; set; }
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public DateTime FechaHora { get; set; }
        public EstadoConsulta Estado { get; set; }

        // Propiedades de navegación
        public Paciente Paciente { get; set; }
        public Medico Medico { get; set; }

        public Consulta() { }

        public Consulta(int idPaciente, int idMedico, DateTime fechaHora)
        {
            if (fechaHora < DateTime.Now.AddHours(24))
                throw new ArgumentException("La consulta debe reservarse con al menos 24 horas de anticipación.");
            if (fechaHora.Minute % 30 != 0)
                throw new ArgumentException("Los turnos deben ser de 30 minutos exactos.");

            ID = contadorID++;
            IdPaciente = idPaciente;
            IdMedico = idMedico;
            FechaHora = fechaHora;
            Estado = EstadoConsulta.Agendada;
        }

        public void Cancelar()
        {
            if (Estado == EstadoConsulta.Realizada)
                throw new InvalidOperationException("No se puede cancelar una consulta ya realizada.");
            Estado = EstadoConsulta.Cancelada;
        }
        public void MarcarComoRealizada()
        {
            if (Estado == EstadoConsulta.Cancelada)
                throw new InvalidOperationException("No se puede marcar como realizada una consulta cancelada.");
            Estado = EstadoConsulta.Realizada;
        }
        public void Reprogramar(DateTime nuevaFechaHora)
        {
            if (Estado == EstadoConsulta.Cancelada)
                throw new InvalidOperationException("No se puede reprogramar una consulta cancelada.");
            if (nuevaFechaHora < DateTime.Now.AddHours(24))
                throw new ArgumentException("La consulta debe reservarse con al menos 24 horas de anticipación.");
            if (nuevaFechaHora.Minute % 30 != 0)
                throw new ArgumentException("Los turnos deben ser de 30 minutos exactos.");

            FechaHora = nuevaFechaHora;
            Estado = EstadoConsulta.Agendada;
        }

        public override string ToString()
        {
            return $"Consulta {ID} - Paciente: {IdPaciente}, Médico: {IdMedico}, Fecha y hora: {FechaHora:dd/MM/yyyy HH:mm}, Estado: {Estado}";
        }
    }
}
