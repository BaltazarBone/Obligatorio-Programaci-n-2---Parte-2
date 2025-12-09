using System;

namespace ObligatorioParte2Razor.Dominio
{
    public enum MetodoPago
    {
        Efectivo,
        Debito,
        Credito
    }

    public class Pago
    {
        public static int contadorID = 1;
        public int ID { get; set; } 
        public int IdConsulta { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Monto { get; set; }
        public MetodoPago Metodo { get; set; }

        public Pago() { }

        public Pago(int idConsulta, decimal monto, MetodoPago metodo, DateTime fechaPago)
        {
            if (monto <= 0) throw new ArgumentException("El monto del pago debe ser mayor a cero.");
            if (fechaPago > DateTime.Now) throw new ArgumentException("La fecha de pago no puede ser futura.");

            ID = contadorID++;
            IdConsulta = idConsulta;
            Monto = monto;
            Metodo = metodo;
            FechaPago = fechaPago;
        }
        public override string ToString()
        {
            return $"Pago {ID} - Consulta: {IdConsulta}, Fecha: {FechaPago:dd/MM/yyyy}, Monto: ${Monto:F2}, Método: {Metodo}";
        }
    }
}
