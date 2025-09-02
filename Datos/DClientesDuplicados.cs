using Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
	public class DClientesDuplicados
	{
		// Lee la cadena desde <connectionStrings> del .config
		private string _connectionString = ConfigurationManager.AppSettings.Get("CadenaConexion");

		/// <summary>
		/// Retorna la lista de clientes duplicados por programación.
		/// </summary>
		public List<EClientesDuplicados> ListarClientesDuplicadosPorProgramacion()
		{
			var lista = new List<EClientesDuplicados>();

			try
			{
				// using garantiza cierre/Dispose aun con excepciones
				using (var conn = new SqlConnection(_connectionString))
				using (var cmd = new SqlCommand("dbo.BD_Pje_DetectarClientesDuplicadosPorProgramacion", conn))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					// Si tu SP requiere parámetros, agrégalos aquí:
					// cmd.Parameters.AddWithValue("@Param", valor);

					conn.Open();

					// Reader también en using
					using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
					{
						// Cachea los índices de columna para evitar errores de escritura y mejorar perf
						int ordDni = SafeOrdinal(reader, "DNI");
						int ordNombre = SafeOrdinal(reader, "Nombre");
						int ordRuta = SafeOrdinal(reader, "Ruta");
						int ordHoraViaje = SafeOrdinal(reader, "HoraViaje");
						int ordProgramacion = SafeOrdinal(reader, "Programacion");
						int ordAsientos = SafeOrdinal(reader, "Asientos");
						int ordUsuario = SafeOrdinal(reader, "Usuario"); // Si necesitas el usuario

						while (reader.Read())
						{
							var entidad = new EClientesDuplicados
							{
								DNI = SafeGetString(reader, ordDni),
								Nombre = SafeGetString(reader, ordNombre),
								Ruta = SafeGetString(reader, ordRuta),
								HoraViaje = SafeGetString(reader, ordHoraViaje),
								Programacion = SafeGetString(reader, ordProgramacion),
								Asientos = SafeGetString(reader, ordAsientos),
								Usuario = SafeGetString(reader, ordUsuario) // Si necesitas el usuario
							};
							lista.Add(entidad);
						}
					}
				}
			}
			catch (Exception)
			{
				// TODO: registra con tu logger (NLog, Serilog, log4net, etc.)
				// Log.Error(ex, "Error al listar clientes duplicados");
				// Decide: relanzar o devolver lista vacía. Aquí devolvemos lista vacía para no romper la UI.
			}

			return lista;
		}

		// Helpers para lectura segura
		private static int SafeOrdinal(SqlDataReader reader, string columnName)
		{
			try
			{
				return reader.GetOrdinal(columnName);
			}
			catch (IndexOutOfRangeException)
			{
				// Lanza error con pista clara si falta la columna
				throw new InvalidOperationException($"La columna '{columnName}' no existe en el resultado del SP.");
			}
		}

		private static string SafeGetString(SqlDataReader reader, int ordinal)
		{
			return reader.IsDBNull(ordinal) ? string.Empty : Convert.ToString(reader.GetValue(ordinal));
		}
	}
}
