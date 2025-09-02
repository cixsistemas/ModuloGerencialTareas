using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using Logica;
using Presentacion.Utilitarios;

namespace Presentacion
{
    public partial class FrmPrueba : Form
    {
        public FrmPrueba()
        {
            InitializeComponent();
        }
        List<EBoletosWeb> _lista;
        Job _Job = new Job();
        CsBoletosWeb _CsBoletosWeb = new CsBoletosWeb();
        LBoletosWeb _LBoletosWeb = new LBoletosWeb();
        private void Listar()
        {
            dgvLista.DataSource = null;
            //EBoletosWeb entidad = new EBoletosWeb();
            try
            {
                LBoletosWeb lp = new LBoletosWeb();
                _lista = lp.ListaBoletosAnuladosWeb("1", "", "", "", "", "", "");

                if (_lista.Count == 0)
                {
                    //lblMensajeError.Text = "No hay registros para mostrar.";
                    //lblMensajeError.ForeColor = Color.Red;
                    //btnModificar.Enabled = false;
                    //btnEliminar.Enabled = false;
                }
                else
                {
                    //btnModificar.Enabled = true;
                    //btnEliminar.Enabled = true;

                    dgvLista.DataSource = _lista;
                    //dgvLista.Columns["Id"].Visible = false;
                    //dgvLista.Columns["IdUsuario"].Visible = false;

                    //RENOMBRAR NOMBRE DE COLUMNAS
                    //this.dgvLista.Columns["NombreGruConc"].HeaderText = "Grupo";
                    //this.dgvLista.Columns["UtilidadGruConc"].HeaderText = "Utilidad";
                    //================================================================

                    //lblMensajeError.Text = "Sistema tiene " + _lista.Count.ToString() + " registros.";
                    //lblMensajeError.ForeColor = Color.Blue;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
                //_CsReutlizar.MensajeError(entidad.MensajeBD() + (e) + ":(");
                throw;
            }

        }
        private void BtnMostrar_Click(object sender, EventArgs e)
        {
            Listar();
            //_CsBoletosWeb.ListaBoletosAnuladosWebJob();

            //_Job.ListaBoletosAnuladosWebJob();
            //VERIFICA SI HAY BOLETOS DUPLICADOS POR WEB
            //if (_CsBoletosWeb.ListaBoletosAnuladosWebJob().Count() > 0)
            //{
                //_LBoletosWeb.MatenimientoBoletosAnuladosWeb(_CsBoletosWeb.ListaBoletosAnuladosWebJob(), "N");
                Scheduler sc = new Scheduler();
                sc.Start1();
            //}

        }
    }
}
