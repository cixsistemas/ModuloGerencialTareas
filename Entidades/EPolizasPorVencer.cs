using System;

namespace Entidades
{
    /// <summary>
    /// Representa un objeto de negocio que contiene la información mínima
    /// necesaria para notificar el vencimiento de una póliza.
    /// </summary>
    public class EPolizasPorVencer
    {
        // El código interno del bus o registro (útil para auditoría)
        public string Codigo { get; set; }

        // La placa del bus, dato clave para que el dueño identifique su unidad
        public string Placa { get; set; }

        // Usamos DateTime para poder formatear la fecha fácilmente en C# 
        // (ej: .ToShortDateString() o .ToString("dd/MM/yyyy"))
        public DateTime FechaVencimiento { get; set; }

        // El número de contrato o póliza para referencia legal en el mensaje
        public string NroPoliza { get; set; }

        public string Telefono { get; set; }
    }
}