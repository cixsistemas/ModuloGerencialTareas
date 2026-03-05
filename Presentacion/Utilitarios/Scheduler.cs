using Quartz;         // Librería principal de Quartz.NET (manejo de Jobs y Triggers)
using Quartz.Impl;    // Implementación por defecto de Quartz (StdSchedulerFactory)

namespace Presentacion.Utilitarios
{
	public class Scheduler
	{
		// Método Start: inicia un scheduler que ejecuta el Job cada 59 minutos
		public void Start()
		{
			// Obtiene el scheduler por defecto de Quartz
			IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

			// Arranca el scheduler (queda en ejecución en segundo plano)
			scheduler.Start();

			// Crea un Job de tipo Job (clase que implementa IJob en tu proyecto)
			IJobDetail job = JobBuilder.Create<Job>().Build();

			// Crea un trigger con las siguientes características:
			ITrigger trigger1 = TriggerBuilder.Create()
			   //.WithIdentity("IDGJob", "IDG")       // Identidad opcional para el Job
			   //.StartAt(date)                      // Para iniciar en una fecha específica
			   //.WithCronSchedule("0 1 * * * ?")    // Ejemplo de cron, pero estaba fallando
			   .StartNow()                           // Se lanza inmediatamente al iniciar
			   .WithPriority(1)                      // Prioridad del trigger
			   .WithDailyTimeIntervalSchedule(s =>
					s.WithIntervalInMinutes(59)      // Ejecuta cada 59 minutos
					 .OnEveryDay()                   // Todos los días
				)
			   .Build();

			// Asocia el Job con el trigger dentro del scheduler
			scheduler.ScheduleJob(job, trigger1);
		}

		// Método Start1: otra forma de configurar el scheduler, más flexible
		public void Start1()
		{
			// Crea un Job con identidad personalizada y marcado como durable
			IJobDetail job = JobBuilder.Create<Job>()
								.StoreDurably()                // Permite existir sin trigger inicial
								.WithIdentity("J_Email", "J_Mailing") // Nombre y grupo del Job
								.Build();

			// Obtiene el scheduler
			IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

			// Agrega el Job al scheduler, aunque todavía no tenga triggers
			scheduler.AddJob(job, true /* reemplazar si ya existe */ );

			// Crea un trigger que:
			ITrigger trigger1 = TriggerBuilder.Create()
								.WithIdentity("MailTrigger1", "T_Mail1") // Nombre y grupo del trigger
								.StartNow()                              // Se lanza inmediatamente
								.WithSimpleSchedule(x => x
									.WithMisfireHandlingInstructionIgnoreMisfires() // Ignora errores de ejecución atrasados
									.WithIntervalInHours(1)                         // Ejecuta cada 1 hora
									.RepeatForever())                               // Se repite indefinidamente
																					//.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(08, 00)) // Ejemplo: todos los días a las 8am
																					//.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(12, 49)) // Ejemplo: todos los días a las 12:49pm
								.ForJob(job)                                       // Asigna el trigger al Job creado
								.Build();

			// Agenda el trigger en el scheduler
			scheduler.ScheduleJob(trigger1);

			// Arranca el scheduler
			scheduler.Start();
		}


		/// <summary>
		/// Configura y arranca el proceso automático de alertas por WhatsApp.
		/// </summary>
		public void StartNotificacionesWhatsApp()
		{
			// Obtiene una instancia del motor de Quartz (Scheduler)
			IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

			// Si el motor no ha arrancado (por ejemplo, al abrir la app), lo inicia
			if (!scheduler.IsStarted) scheduler.Start();

			// Definición del Job: Le indica a Quartz que debe ejecutar la clase 'JobPolizas'
			IJobDetail job = JobBuilder.Create<JobPolizas>()
								.WithIdentity("JobWhatsApp", "GrupoAlertas") // Nombre único para identificar el proceso
								.Build();

			// Trigger con Cron Schedule: Permite una programación muy precisa.
			// Formato: "segundos minutos horas díaMes mes díaSemana"
			ITrigger trigger = TriggerBuilder.Create()
								.WithIdentity("TriggerWhatsApp", "GrupoAlertas")
								.WithCronSchedule("0 0 9 * * ?") // Se dispara EXACTAMENTE a las 09:00:00 AM todos los días
								.Build();

			// Registra el Job y el Trigger en el motor de Quartz
			scheduler.ScheduleJob(job, trigger);
		}

		/// <summary>
		/// Configura y arranca el proceso automático de reportes por Correo Electrónico.
		/// </summary>
		public void StartCorreoCada4Horas()
		{
			// Obtiene o reutiliza el scheduler por defecto
			IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
			if (!scheduler.IsStarted) scheduler.Start();

			// Definición del Job: Apunta a la clase 'JobCorreoPolizas' encargada de armar el HTML del mail
			IJobDetail job = JobBuilder.Create<JobCorreoPolizas>()
								.WithIdentity("JobCorreo", "GrupoMailing")
								.Build();

			// Trigger de Intervalo Simple: Ideal para tareas recurrentes frecuentes.
			ITrigger trigger = TriggerBuilder.Create()
								.WithIdentity("TriggerCorreo", "GrupoMailing")
								.StartNow() // Se ejecuta por PRIMERA VEZ apenas se abre la aplicación
								.WithSimpleSchedule(x => x
									.WithIntervalInHours(4)     // Define la frecuencia de repetición (4 horas)
									.RepeatForever())           // No tiene fecha de finalización
								.Build();

			// Registra el proceso de correo en el motor
			scheduler.ScheduleJob(job, trigger);
		}

	}
}
