using System;
using System.Linq;

namespace ObligatorioParte2Razor.Dominio
{
    public class Paciente : Persona
    {
        private static int contadorID = 1;

        public string NumeroDocumento { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Email { get; set; }
        public string ObraSocial { get; set; }

        // Constructor vacío para EF
        public Paciente() { }

        public Paciente(string nombre, string apellido, string nombreUsuario, string contrasenia, string numeroDocumento, string telefono, DateTime fechaNacimiento, string email, string obraSocial) : base(nombre, apellido, nombreUsuario, contrasenia)
        {
            if (string.IsNullOrWhiteSpace(numeroDocumento)) throw new ArgumentException("El número de documento no puede estar vacío.");
            if (string.IsNullOrWhiteSpace(telefono) || !telefono.All(char.IsDigit)) throw new ArgumentException("El teléfono solo puede contener números.");
            if (fechaNacimiento > DateTime.Today) throw new ArgumentException("La fecha de nacimiento no puede ser futura.");
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@")) throw new ArgumentException("Email inválido.");

            ID = contadorID++;
            NumeroDocumento = numeroDocumento;
            Telefono = telefono;
            FechaNacimiento = fechaNacimiento;
            Email = email;
            ObraSocial = obraSocial;
        }
        public override string ToString()
        {
            string obraSocialMostrar = string.IsNullOrWhiteSpace(ObraSocial) ? "Sin obra social" : ObraSocial;
            return $"{base.ToString()} (Email: {Email}, Telefono: {Telefono}, Numero de documento: {NumeroDocumento}, Fecha de nacimiento: {FechaNacimiento:dd/MM/yyyy}, Obra social: {obraSocialMostrar})";
        }
    }
}
