namespace Presentacion
{
    partial class FrmPrueba
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
			this.dgvLista = new System.Windows.Forms.DataGridView();
			this.BtnMostrar = new System.Windows.Forms.Button();
			this.BtnWhatsApp = new System.Windows.Forms.Button();
			this.lblEstado = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dgvLista)).BeginInit();
			this.SuspendLayout();
			// 
			// dgvLista
			// 
			this.dgvLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvLista.Location = new System.Drawing.Point(57, 172);
			this.dgvLista.Name = "dgvLista";
			this.dgvLista.RowHeadersWidth = 51;
			this.dgvLista.RowTemplate.Height = 24;
			this.dgvLista.Size = new System.Drawing.Size(880, 240);
			this.dgvLista.TabIndex = 0;
			// 
			// BtnMostrar
			// 
			this.BtnMostrar.Location = new System.Drawing.Point(745, 22);
			this.BtnMostrar.Name = "BtnMostrar";
			this.BtnMostrar.Size = new System.Drawing.Size(204, 56);
			this.BtnMostrar.TabIndex = 1;
			this.BtnMostrar.Text = "button1";
			this.BtnMostrar.UseVisualStyleBackColor = true;
			this.BtnMostrar.Click += new System.EventHandler(this.BtnMostrar_Click);
			// 
			// BtnWhatsApp
			// 
			this.BtnWhatsApp.Location = new System.Drawing.Point(745, 94);
			this.BtnWhatsApp.Name = "BtnWhatsApp";
			this.BtnWhatsApp.Size = new System.Drawing.Size(204, 56);
			this.BtnWhatsApp.TabIndex = 2;
			this.BtnWhatsApp.Text = "WHATSAPP";
			this.BtnWhatsApp.UseVisualStyleBackColor = true;
			this.BtnWhatsApp.Click += new System.EventHandler(this.BtnWhatsApp_Click);
			// 
			// lblEstado
			// 
			this.lblEstado.AutoSize = true;
			this.lblEstado.Location = new System.Drawing.Point(68, 436);
			this.lblEstado.Name = "lblEstado";
			this.lblEstado.Size = new System.Drawing.Size(46, 17);
			this.lblEstado.TabIndex = 3;
			this.lblEstado.Text = "label1";
			// 
			// FrmPrueba
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.ClientSize = new System.Drawing.Size(988, 506);
			this.Controls.Add(this.lblEstado);
			this.Controls.Add(this.BtnWhatsApp);
			this.Controls.Add(this.BtnMostrar);
			this.Controls.Add(this.dgvLista);
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "FrmPrueba";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.dgvLista)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLista;
        private System.Windows.Forms.Button BtnMostrar;
		private System.Windows.Forms.Button BtnWhatsApp;
		private System.Windows.Forms.Label lblEstado;
	}
}

