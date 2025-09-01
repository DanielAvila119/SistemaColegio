namespace Sistema
{
    partial class frmMarcarAsistenciaDocentes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvMarcarAsistenciaDoc = new System.Windows.Forms.DataGridView();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkAsistio = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarcarAsistenciaDoc)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMarcarAsistenciaDoc
            // 
            this.dgvMarcarAsistenciaDoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMarcarAsistenciaDoc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column1,
            this.chkAsistio});
            this.dgvMarcarAsistenciaDoc.Location = new System.Drawing.Point(12, 46);
            this.dgvMarcarAsistenciaDoc.Name = "dgvMarcarAsistenciaDoc";
            this.dgvMarcarAsistenciaDoc.Size = new System.Drawing.Size(461, 303);
            this.dgvMarcarAsistenciaDoc.TabIndex = 0;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(253, 355);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(77, 23);
            this.btnGuardar.TabIndex = 51;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(157, 355);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(80, 23);
            this.btnCancelar.TabIndex = 52;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "IdDocente";
            this.Column3.Name = "Column3";
            this.Column3.Visible = false;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Docente";
            this.Column1.Name = "Column1";
            // 
            // chkAsistio
            // 
            this.chkAsistio.HeaderText = "Asistió";
            this.chkAsistio.Name = "chkAsistio";
            this.chkAsistio.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.chkAsistio.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dtpFecha
            // 
            this.dtpFecha.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(105, 12);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(132, 20);
            this.dtpFecha.TabIndex = 53;
            // 
            // frmMarcarAsistenciaDocentes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 392);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.dgvMarcarAsistenciaDoc);
            this.Name = "frmMarcarAsistenciaDocentes";
            this.Text = "Marcar Asistencia de Docentes";
            this.Load += new System.EventHandler(this.frmMarcarAsistencia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarcarAsistenciaDoc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMarcarAsistenciaDoc;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkAsistio;
        private System.Windows.Forms.DateTimePicker dtpFecha;
    }
}