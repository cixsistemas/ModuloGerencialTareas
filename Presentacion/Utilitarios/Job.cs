using Entidades;            // Referencia a las entidades del sistema (EClientesDuplicados, etc.)
using Logica;               // Capa de lógica de negocio (LClientesDuplicados)
using Quartz;               // Librería Quartz.NET para la programación de tareas
using System.Collections.Generic;
using System.Linq;

namespace Presentacion.Utilitarios
{
	// Clase que implementa un Job de Quartz
	// Los jobs son las tareas que Quartz ejecuta de manera programada
	public class Job : IJob
	{
		// Instancia del helper que construye y envía correos
		MailHelper MailHelper = new MailHelper();

		// Lista en memoria para guardar clientes duplicados
		public List<EClientesDuplicados> list = new List<EClientesDuplicados>();

		// Método obligatorio de la interfaz IJob
		// Aquí se define qué hará la tarea cuando Quartz la ejecute
		public void Execute(IJobExecutionContext context)
		{
			// Verifica si hay clientes duplicados en la base de datos
			// Si hay al menos 1, envía un correo con los detalles
			if (ListarClientesDuplicadosPorProgramacion().Count() > 0)
			{
				MailHelper.EnviarClientesDuplicados("Clientes Duplicados", ListarClientesDuplicadosPorProgramacion());
			}
		}

		// Método auxiliar para obtener la lista de clientes duplicados
		// Llama a la capa lógica (LClientesDuplicados), que a su vez consulta la BD
		private List<EClientesDuplicados> ListarClientesDuplicadosPorProgramacion()
		{
			LClientesDuplicados lp = new LClientesDuplicados();
			list = lp.ListarClientesDuplicadosPorProgramacion();
			return list;
		}
	}
}


//List<EBoletosWeb> lista = new List<EBoletosWeb>();
//_CsBoletosWeb.ListaBoletosAnuladosWebJob();
////VERIFICA SI HAY BOLETOS DUPLICADOS POR WEB
//if (_CsBoletosWeb.ListaBoletosAnuladosWebJob().Count() > 0)
//{
//	_CsEnvioCorreo.EnviarMensaje("Boletos duplicados (Venta Web)", _CsBoletosWeb.ListaBoletosAnuladosWebJob());
//}
