using Presentacion.Utilitarios;  // Aquí están las clases utilitarias, como Scheduler y Jobs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion
{
	public partial class FrmEmail : Form
	{
	
		public FrmEmail()
		{
			InitializeComponent(); // Inicializa los componentes del formulario (botones, controles, etc.)
		}

		// Evento que se ejecuta al cargar el formulario
		private void FrmEmail_Load(object sender, EventArgs e)
		{
			// === UBICAR FORMULARIO EN ESQUINA SUPERIOR DERECHA ===
			Screen scr = Screen.FromPoint(Location);
			Location = new Point(scr.WorkingArea.Right - Width, scr.WorkingArea.Top);
			// Esto mueve la ventana a la esquina superior derecha de la pantalla

			// === OCULTAR FORMULARIO EN BARRA DE TAREAS ===
			this.ShowInTaskbar = false;
			// El formulario no aparecerá en la barra de tareas, quedará "oculto"

			// === EJECUTAR PROGRAMADOR AL CARGAR FORMULARIO ===
			Ejecutar(); // Llama al programador (Quartz Scheduler) apenas carga el form
		}

		// Evento que se ejecuta cuando el usuario intenta cerrar el formulario
		private void FrmEmail_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Impedir que el formulario se cierre pulsando la X o Alt + F4
			switch (e.CloseReason)
			{
				case CloseReason.UserClosing:
					e.Cancel = true; // Cancela el cierre, mantiene el form en ejecución
					break;
			}
		}

		// Botón "Ejecutar" (comentado porque ahora se llama Ejecutar() directo en Load)
		private void BtnEjecutar_Click(object sender, EventArgs e)
		{
			
		}

		// Método que arranca el Scheduler
		private void Ejecutar()
		{
			//DateTime date = dateTimePicker1.Value; // Posible futura personalización con DateTimePicker (comentado)

			Scheduler sc = new Scheduler(); // Crea instancia del programador Quartz
			sc.Start(); // Arranca el scheduler (ejecutará el Job programado)
		}
	}
}
