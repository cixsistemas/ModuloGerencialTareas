using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Utilitarios
{
    /// <summary>
    /// Clase encargada de gestionar la comunicación con el microservicio de WhatsApp (Node.js).
    /// </summary>
    public class WhatsappHelper
    {
        // Define la dirección del servidor local donde corre la API de WhatsApp.
        private readonly string _urlNode = "http://localhost:3000/enviar-alerta";

        /// <summary>
        /// Envía una petición POST al servidor Node.js para disparar un mensaje de WhatsApp.
        /// </summary>
        /// <param name="telefono">Número de destino (debe incluir código de país, ej. 51...)</param>
        /// <param name="mensaje">Texto del mensaje a enviar.</param>
        /// <returns>True si el servidor respondió con éxito, False en caso contrario.</returns>
        public async Task<bool> EnviarAlertaPoliza(string telefono, string mensaje)
        {
            try
            {
                // HttpClient se usa para realizar peticiones web (HTTP). 
                // Se envuelve en un 'using' para asegurar que se liberen los recursos de red al terminar.
                using (var client = new HttpClient())
                {
                    // Creamos un objeto anónimo con la estructura que espera el server.js
                    var data = new { numero = telefono, mensaje = mensaje };

                    // Convertimos el objeto de C# a una cadena de texto en formato JSON
                    var json = JsonConvert.SerializeObject(data);

                    // Preparamos el contenido del cuerpo del mensaje (UTF8 y tipo application/json)
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    // Realizamos la llamada asíncrona al servidor Node.js
                    var response = await client.PostAsync(_urlNode, content);

                    // Verificamos si la respuesta es exitosa (código 200 OK)
                    if (!response.IsSuccessStatusCode)
                    {
                        // Si falló (ej: error 400 o 500), leemos el motivo que devuelve el servidor
                        string errorDetalle = await response.Content.ReadAsStringAsync();

                        // Registro de error en archivo físico para auditoría posterior
                        System.IO.File.AppendAllText("log_errores.txt",
                            $"{DateTime.Now}: Error enviando a {telefono}. Detalle: {errorDetalle}\n");

                        return false;
                    }

                    // Si llegamos aquí, el mensaje fue aceptado por el microservicio
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Este bloque captura errores críticos, como que el servidor Node.js esté apagado
                System.IO.File.AppendAllText("log_errores.txt",
                    $"{DateTime.Now}: No se pudo conectar con el servidor Node.js. ¿Está encendido? {ex.Message}\n");

                return false;
            }
        }
    }
}