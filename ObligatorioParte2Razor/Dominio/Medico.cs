using System;
using System.Collections.Generic;
using System.Linq;

namespace ObligatorioParte2Razor.Dominio
{
    public class Medico : Persona
    {
        private static int contadorID = 1;
        public string Especialidad { get; set; }
        public string Matricula { get; set; }
        public List<string> DiasAtencion { get; set; } = new List<string>();
        public List<TimeSpan> HorariosDisponibles { get; set; } = new List<TimeSpan>();

        public Medico() { }

        public Medico(string nombre, string apellido, string nombreUsuario, string contrasenia, string especialidad, string matricula, List<string> diasAtencion, List<TimeSpan> horariosDisponibles) : base(nombre, apellido, nombreUsuario, contrasenia)
        {
            if (string.IsNullOrWhiteSpace(especialidad)) throw new ArgumentException("La especialidad no puede estar vacía.");
            if (string.IsNullOrWhiteSpace(matricula)) throw new ArgumentException("La matrícula no puede estar vacía.");
            if (diasAtencion == null || diasAtencion.Count == 0) throw new ArgumentException("Debe ingresar al menos un día de atención.");
            if (horariosDisponibles == null || horariosDisponibles.Count == 0) throw new ArgumentException("Debe ingresar al menos un horario disponible.");

            ID = contadorID++;
            Especialidad = especialidad;
            Matricula = matricula;
            DiasAtencion = diasAtencion;
            HorariosDisponibles = horariosDisponibles;
        }
        public override string ToString()
        {
            string diasDisponibles = string.Join("/ ", DiasAtencion);
            string horariosDisponibles = string.Join("/ ", HorariosDisponibles.Select(h => h.ToString(@"hh\:mm")));
            return $"{base.ToString()} (Especialidad: {Especialidad}, Matrícula: {Matricula}, Días de atención: {diasDisponibles}, Horarios disponibles: {horariosDisponibles})";
        }
    }
}
