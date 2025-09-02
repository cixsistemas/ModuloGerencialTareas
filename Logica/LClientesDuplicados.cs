using System.Collections.Generic;

namespace Logica
{
	public class LClientesDuplicados
	{
		private readonly Datos.DClientesDuplicados _datos = new Datos.DClientesDuplicados();
		/// <summary>
		/// Retorna la lista de clientes duplicados por programación.
		/// </summary>
		public List<Entidades.EClientesDuplicados> ListarClientesDuplicadosPorProgramacion()
		{
			return _datos.ListarClientesDuplicadosPorProgramacion();
		}
	}
}
