namespace Presentacion.Utilitarios
{
	public static class ConstructorMensajesWhatsApp
	{
		public static string FormatearAlertaPoliza(Entidades.EPolizasPorVencer poliza)
		{
			// Usamos interpolación de cadenas para que sea más legible
			string mensaje = $"*RECORDATORIO DE VENCIMIENTO* 🚐\n\n" +
							 $"Estimado(a), le informamos los detalles de su seguro:\n\n" +
							 $"• *Codigo:* {poliza.Codigo}\n" +
							 $"• *Placa:* {poliza.Placa}\n" +
							 $"• *Nro. Póliza:* {poliza.NroPoliza}\n" +
							 $"• *Vence el:* {poliza.FechaVencimiento.ToString("dd/MM/yyyy")}\n\n" +
							 $"_Por favor, realice la renovación antes de la fecha indicada para evitar inconvenientes._";

			return mensaje;
		}
	}
}
