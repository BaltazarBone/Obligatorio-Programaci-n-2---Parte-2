using System;

namespace ObligatorioParte2Razor.Dominio
{
    public class Administrativo : Persona
    {
        private static int contadorID = 1;
        public string NumeroDocumento { get; set; }

        public Administrativo() { }

        public Administrativo(string nombre, string apellido, string nombreUsuario, string contrasenia, string numeroDocumento) : base(nombre, apellido, nombreUsuario, contrasenia)
        {
            if (string.IsNullOrWhiteSpace(numeroDocumento)) throw new ArgumentException("El número de documento no puede estar vacío.");
            ID = contadorID++;
            NumeroDocumento = numeroDocumento;
        }

        public override string ToString()
        {
            return $"{base.ToString()} (Número de documento: {NumeroDocumento})";
        }
    }
}
