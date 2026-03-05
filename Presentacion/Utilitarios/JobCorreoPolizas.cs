using Quartz;

namespace Presentacion.Utilitarios
{
	public class JobCorreoPolizas : IJob
    {
        private readonly Logica.LPolizasPorVencer _logica = new Logica.LPolizasPorVencer();
        private readonly MailHelper _mail = new MailHelper();

        public void Execute(IJobExecutionContext context)
        {
            var lista = _logica.ListarPolizasPorVencer();

            if (lista != null && lista.Count > 0)
            {
                // Contenedor principal para dar margen y fuente legible en celular
                string cuerpo = "<div style='font-family: Arial; padding: 20px; color: #333;'>";
                cuerpo += "<h2 style='color: #004a99;'>Notificación de Pólizas por Vencer</h2>";
                cuerpo += "<p>Se han detectado las siguientes unidades próximas a vencer:</p>";

                // Tabla con estilos inline
                string tabla = "<table style='width: 100%; border-collapse: collapse; min-width: 300px;'>" +
                               "<thead>" +
                               "  <tr style='background-color: #004a99; color: #ffffff; text-align: left;'>" +
                               "    <th style='padding: 12px; border: 1px solid #ddd;'>Codigo</th>" +
                               "    <th style='padding: 12px; border: 1px solid #ddd;'>Placa</th>" +
                               "    <th style='padding: 12px; border: 1px solid #ddd;'>Nro Póliza</th>" +
                               "    <th style='padding: 12px; border: 1px solid #ddd;'>Vencimiento</th>" +
                               "  </tr>" +
                               "</thead>" +
                               "<tbody>";

                foreach (var item in lista)
                {
                    tabla += $"<tr>" +
                             $"  <td style='padding: 10px; border: 1px solid #ddd;'>{item.Codigo}</td>" +
                             $"  <td style='padding: 10px; border: 1px solid #ddd;'>{item.Placa}</td>" +
                             $"  <td style='padding: 10px; border: 1px solid #ddd;'>{item.NroPoliza}</td>" +
                             $"  <td style='padding: 10px; border: 1px solid #ddd;'>{item.FechaVencimiento.ToShortDateString()}</td>" +
                             $"</tr>";
                }

                tabla += "</tbody></table>";
                cuerpo += tabla;
                cuerpo += "<p style='margin-top: 20px; font-size: 12px; color: #777;'>Este es un mensaje automático.</p>";
                cuerpo += "</div>";

                _mail.EnviarCorreoPolizas("ALERTA DE VENCIMIENTO DE PÓLIZAS", cuerpo);
            }
        }
    }
}