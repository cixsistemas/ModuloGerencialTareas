using Presentacion.Utilitarios;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class FrmEmail : Form
    {
        public FrmEmail()
        {
            InitializeComponent();
            //dateTimePicker1.Format = DateTimePickerFormat.Custom;
            //dateTimePicker1.CustomFormat = "dd/MM/yyyy hh:mm:ss";
            //dateTimePicker1.Format = DateTimePickerFormat.Custom;
            //dateTimePicker1.CustomFormat = "MM/dd/yyyy hh:mm:ss";
            //Ejecutar();
        }
        CsBoletosWeb _CsBoletosWeb = new CsBoletosWeb();
        private void FrmEmail_Load(object sender, EventArgs e)
        {
            //UBICAR FORMULARIO EN ESQUINA SUPERIOR DERECHA
            Screen scr = Screen.FromPoint(Location);
            Location = new Point(scr.WorkingArea.Right - Width, scr.WorkingArea.Top);
            //=================================================

            //OCULTAR FORMULARIO EN BARRA DE TAREAS
            this.ShowInTaskbar = false;
            //=================================================
            //BtnEjecutar_Click(sender, e);

            //EJECUTAR PROGRAMADOR AL CARGAR FORMULARIO
            Ejecutar();
            //button1_Click(sender, e);
            //=================================================
            //MessageBox.Show(dateTimePicker1.Value.ToString());
        }

        private void FrmEmail_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Impedir que el formulario se cierre pulsando X o Alt + F4
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                    e.Cancel = true;
                    break;
            }
        }

        private void BtnEjecutar_Click(object sender, EventArgs e)
        {
            //_CsBoletosWeb.ListaBoletosAnuladosWebJob();
            ////_Job.ListaBoletosAnuladosWebJob();
            ////VERIFICA SI HAY BOLETOS DUPLICADOS POR WEB
            //if (_CsBoletosWeb.ListaBoletosAnuladosWebJob().Count() > 0)
            //{
            //    Scheduler sc = new Scheduler();
            //    sc.Start();
            //}
        }

        private void Ejecutar()
        {
            //DateTime date = dateTimePicker1.Value;
            
            Scheduler sc = new Scheduler();
            sc.Start();
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    DateTime date = dateTimePicker1.Value;
        //    Scheduler sc = new Scheduler();
        //    sc.Start();
        //    MessageBox.Show(dateTimePicker1.Value.ToString());
        //}
    }
}
