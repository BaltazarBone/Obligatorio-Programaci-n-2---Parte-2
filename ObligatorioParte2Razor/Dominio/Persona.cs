using System;

namespace ObligatorioParte2Razor.Dominio
{
    public abstract class Persona
    {
        public int ID { get; set; } 
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasenia { get; set; }

        // Constructor vacío requerido por EF
        protected Persona() { }

        protected Persona(string nombre, string apellido, string nombreUsuario, string contrasenia)
        {
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("El nombre no puede estar vacío.");
            if (string.IsNullOrWhiteSpace(apellido)) throw new ArgumentException("El apellido no puede estar vacío.");
            if (string.IsNullOrWhiteSpace(nombreUsuario)) throw new ArgumentException("El nombre de usuario no puede estar vacío.");
            if (string.IsNullOrWhiteSpace(contrasenia)) throw new ArgumentException("La contraseña no puede estar vacía.");

            Nombre = nombre;
            Apellido = apellido;
            NombreUsuario = nombreUsuario;
            Contrasenia = contrasenia;
        }
        public override string ToString()
        {
            return $"ID: {ID} - Nombre y Apellido: {Nombre} {Apellido}";
        }
    }
}
