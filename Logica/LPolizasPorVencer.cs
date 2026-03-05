using System.Collections.Generic;

namespace Logica
{
	/// <summary>
	/// Capa de Negocio para la gestión de alertas de pólizas.
	/// Aquí se deberían aplicar reglas de validación o filtros adicionales.
	/// </summary>
	public class LPolizasPorVencer
    {
        // Instancia privada de la capa de datos para acceder a la base de datos.
        // Se usa 'readonly' para asegurar que la referencia no cambie después de la inicialización.
        private readonly Datos.DPolizasPorVencer _datos = new Datos.DPolizasPorVencer();

        /// <summary>
        /// Obtiene la lista de pólizas que cumplen la condición de vencimiento desde la capa de Datos.
        /// </summary>
        /// <returns>Una lista de objetos EPolizasPorVencer.</returns>
        public List<Entidades.EPolizasPorVencer> ListarPolizasPorVencer()
        {
            // En esta capa podrías agregar lógica adicional en el futuro, por ejemplo:
            // - Filtrar placas específicas.
            // - Formatear textos antes de enviarlos a la UI.
            // - Validar si el usuario tiene permisos para ver esta lista.

            return _datos.ListarPolizasPorVencer();
        }
    }
}