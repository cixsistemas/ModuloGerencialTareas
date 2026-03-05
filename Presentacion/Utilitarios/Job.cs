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

		public List<EPolizasPorVencer> listPolizas = new List<EPolizasPorVencer>();

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

			//var polizas = ListarPolizasPorVencer();

			//if (polizas != null && polizas.Count > 0)
			//{
			//	WhatsappHelper ws = new WhatsappHelper();

			//	foreach (var item in polizas)
			//	{
			//		// LLAMADA AL NUEVO MÉTODO APARTE
			//		string mensajeTexto = ConstructorMensajesWhatsApp.FormatearAlertaPoliza(item);

			//		// Procesar los teléfonos (pueden ser varios separados por coma)
			//		string[] destinos = item.Telefono.Split(new char[] { ',', ';' }, System.StringSplitOptions.RemoveEmptyEntries);

			//		foreach (var tel in destinos)
			//		{
			//			string numLimpio = tel.Trim();
			//			// Validar formato Perú (51)
			//			string numFinal = numLimpio.StartsWith("51") ? numLimpio : "51" + numLimpio;

			//			// Enviar de forma asíncrona pero esperando el resultado (Wait) para el Job
			//			System.Threading.Tasks.Task.Run(() => ws.EnviarAlertaPoliza(numFinal, mensajeTexto)).Wait();

			//			// Pausa de 3 segundos entre números para proteger tu cuenta de WhatsApp
			//			System.Threading.Thread.Sleep(3000);
			//		}
			//	}
			//}
		}

		// Método auxiliar para obtener la lista de clientes duplicados
		// Llama a la capa lógica (LClientesDuplicados), que a su vez consulta la BD
		private List<EClientesDuplicados> ListarClientesDuplicadosPorProgramacion()
		{
			LClientesDuplicados lp = new LClientesDuplicados();
			list = lp.ListarClientesDuplicadosPorProgramacion();
			return list;
		}

		//// Método auxiliar para obtener la lista de polizas por vencer
		//// Llama a la capa lógica (LPolizasPorVencer), que a su vez consulta la BD
		//private List<EPolizasPorVencer> ListarPolizasPorVencer()
		//{
		//	LPolizasPorVencer lp = new LPolizasPorVencer();
		//	listPolizas = lp.ListarPolizasPorVencer();
		//	return listPolizas;
		//}
	}
}
