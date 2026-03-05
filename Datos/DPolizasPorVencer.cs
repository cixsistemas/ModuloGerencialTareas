using Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class DPolizasPorVencer
    {
        // Recupera la cadena de conexión desde el archivo App.config o Web.config.
        // Es una buena práctica para no exponer credenciales en el código fuente.
        private string _connectionString = ConfigurationManager.AppSettings.Get("CadenaConexion");

        /// <summary>
        /// Ejecuta el procedimiento almacenado para obtener pólizas que vencen en 4 días.
        /// </summary>
        public List<EPolizasPorVencer> ListarPolizasPorVencer()
        {
            // Inicializamos la lista que contendrá los resultados.
            var lista = new List<EPolizasPorVencer>();

            try
            {
                // El bloque 'using' asegura que la conexión se cierre y se libere de la memoria 
                // automáticamente, incluso si ocurre un error (evita fugas de memoria).
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("dbo.BD_Pje_ObtenerPolizasPorVencer", conn))
                {
                    // Especificamos que vamos a ejecutar un Stored Procedure y no una consulta de texto.
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    // ExecuteReader con CloseConnection cierra la conexión automáticamente al terminar de leer.
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        // OPTIMIZACIÓN: Buscamos el índice (posición) de las columnas una sola vez antes de entrar al bucle.
                        // Esto es mucho más rápido que buscar la columna por nombre en cada fila.
                        int ordCodigo = SafeOrdinal(reader, "Codigo");
                        int ordPlaca = SafeOrdinal(reader, "Placa");
                        int ordFechaVencimiento = SafeOrdinal(reader, "FechaVencimiento");
                        int ordNroPoliza = SafeOrdinal(reader, "NroPoliza");
                        int ordTelefono = SafeOrdinal(reader, "Telefono");

                        // Recorremos los resultados fila por fila.
                        while (reader.Read())
                        {
                            var entidad = new EPolizasPorVencer
                            {
                                // Asignamos los valores usando métodos seguros para evitar errores con valores NULL.
                                Codigo = SafeGetString(reader, ordCodigo),
                                Placa = SafeGetString(reader, ordPlaca),

                                // CORRECCIÓN TÉCNICA: Se lee como DateTime. Si es nulo, asignamos el valor mínimo de fecha.
                                FechaVencimiento = reader.IsDBNull(ordFechaVencimiento) ? DateTime.MinValue : reader.GetDateTime(ordFechaVencimiento),

                                NroPoliza = SafeGetString(reader, ordNroPoliza),
                                Telefono = SafeGetString(reader, ordTelefono)
                            };
                            lista.Add(entidad);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Es vital capturar la excepción. En WinForms, podrías relanzarla con 'throw'
                // o registrarla en un log para saber por qué falló la conexión.
                throw new Exception("Error en la capa de Datos al consultar pólizas: " + ex.Message);
            }

            return lista;
        }

        /// <summary>
        /// Verifica que la columna exista en el resultado del SP antes de intentar leerla.
        /// </summary>
        private static int SafeOrdinal(SqlDataReader reader, string columnName)
        {
            try
            {
                return reader.GetOrdinal(columnName);
            }
            catch (IndexOutOfRangeException)
            {
                // Si cambias el nombre en el SP pero olvidas cambiarlo en C#, esto te dirá exactamente qué pasó.
                throw new InvalidOperationException($"La columna '{columnName}' no existe en el resultado del SP.");
            }
        }

        /// <summary>
        /// Convierte el valor de la base de datos a string, manejando valores nulos de forma segura.
        /// </summary>
        private static string SafeGetString(SqlDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? string.Empty : Convert.ToString(reader.GetValue(ordinal));
        }
    }
}