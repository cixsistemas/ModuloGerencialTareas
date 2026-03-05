using Quartz;
using System;
using System.Threading.Tasks;

namespace Presentacion.Utilitarios
{
    /// <summary>
    /// Trabajo (Job) programado mediante Quartz.NET para procesar el envío masivo de alertas.
    /// Implementa IJob para permitir que el Scheduler lo ejecute automáticamente.
    /// </summary>
    public class JobPolizas : IJob
    {
        // Instancia del helper para la comunicación con el servidor Node.js
        private readonly WhatsappHelper _whatsapp = new WhatsappHelper();

        // Acceso a la capa de lógica para obtener los datos de SQL Server
        private readonly Logica.LPolizasPorVencer _logica = new Logica.LPolizasPorVencer();

        /// <summary>
        /// Método principal que se dispara según la programación del Scheduler.
        /// </summary>
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                // 1. Obtener la lista de registros que cumplen la condición de vencimiento (DATEDIFF = 3)
                var lista = _logica.ListarPolizasPorVencer();

                // 1. Leer teléfonos de administradores desde el App.config
                string adminConfig = System.Configuration.ConfigurationManager.AppSettings["TelefonosEnvioPoliza"];
                string[] listaAdmins = adminConfig?.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                // Verificamos que la lista no sea nula y contenga registros para evitar procesos innecesarios
                if (lista != null && lista.Count > 0)
                {
                    foreach (var poliza in lista)
                    {
                        // 2. Construimos el cuerpo del mensaje formateado (con negritas y saltos de línea)
                        string mensaje = ConstructorMensajesWhatsApp.FormatearAlertaPoliza(poliza);

                        //// --- BLOQUE A: ENVÍO A DUEÑOS (Viene de Base de Datos) ---
                        //string[] telsDueños = poliza.Telefono.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                        //ProcesarYEnviar(telsDueños, mensaje);

                        // --- BLOQUE B: ENVÍO A ADMINISTRADORES (Viene de App.config) ---
                        if (listaAdmins != null)
                        {
                            // Agregamos una nota al mensaje para el admin
                            string mensajeAdmin = mensaje;
                            ProcesarYEnviar(listaAdmins, mensajeAdmin);
                        }                     
                    }
                }
            }
            catch (Exception ex)
            {
                // Captura de errores críticos durante la ejecución del ciclo (ej. caída de BD o falta de internet)
                Console.WriteLine("Error en JobPolizas: " + ex.Message);

                // Nota: En un entorno de servidor, aquí se recomienda usar System.Diagnostics.Debug o un logger de archivo.
            }
        }

        // Método auxiliar para evitar repetir código
        private void ProcesarYEnviar(string[] telefonos, string mensaje)
        {
            foreach (var tel in telefonos)
            {
                string numLimpio = tel.Trim();
                string numFinal = numLimpio.StartsWith("51") ? numLimpio : "51" + numLimpio;

                Task.Run(() => _whatsapp.EnviarAlertaPoliza(numFinal, mensaje)).Wait();
                System.Threading.Thread.Sleep(3000); // Respetamos el delay
            }
        }

    }
}