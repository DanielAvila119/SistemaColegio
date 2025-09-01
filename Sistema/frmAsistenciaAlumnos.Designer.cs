namespace Sistema
{
    partial class frmAsistenciaAlumnos
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.btnReporteExcel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerarReporte = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lblTotalRegistros = new System.Windows.Forms.Label();
            this.dgvAsistencia = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsistencia)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.btnGuardar);
            this.groupBox2.Controls.Add(this.lblTotalRegistros);
            this.groupBox2.Controls.Add(this.dgvAsistencia);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(597, 376);
            this.groupBox2.TabIndex = 61;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ver/Registrar Asistencias";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpFecha);
            this.groupBox1.Controls.Add(this.btnReporteExcel);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnGenerarReporte);
            this.groupBox1.Location = new System.Drawing.Point(6, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(441, 45);
            this.groupBox1.TabIndex = 53;
            this.groupBox1.TabStop = false;
            // 
            // dtpFecha
            // 
            this.dtpFecha.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(66, 14);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(132, 20);
            this.dtpFecha.TabIndex = 15;
            this.dtpFecha.ValueChanged += new System.EventHandler(this.dtpFecha_ValueChanged);
            // 
            // btnReporteExcel
            // 
            this.btnReporteExcel.BackColor = System.Drawing.Color.ForestGreen;
            this.btnReporteExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReporteExcel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReporteExcel.ForeColor = System.Drawing.Color.White;
            this.btnReporteExcel.Location = new System.Drawing.Point(327, 14);
            this.btnReporteExcel.Name = "btnReporteExcel";
            this.btnReporteExcel.Size = new System.Drawing.Size(103, 23);
            this.btnReporteExcel.TabIndex = 52;
            this.btnReporteExcel.Text = "Exportar a Excel";
            this.btnReporteExcel.UseVisualStyleBackColor = false;
            this.btnReporteExcel.Click += new System.EventHandler(this.btnReporteExcel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "FECHA:";
            // 
            // btnGenerarReporte
            // 
            this.btnGenerarReporte.BackColor = System.Drawing.Color.Gray;
            this.btnGenerarReporte.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerarReporte.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGenerarReporte.ForeColor = System.Drawing.Color.White;
            this.btnGenerarReporte.Location = new System.Drawing.Point(218, 14);
            this.btnGenerarReporte.Name = "btnGenerarReporte";
            this.btnGenerarReporte.Size = new System.Drawing.Size(103, 23);
            this.btnGenerarReporte.TabIndex = 50;
            this.btnGenerarReporte.Text = "Exportar a PDF";
            this.btnGenerarReporte.UseVisualStyleBackColor = false;
            this.btnGenerarReporte.Click += new System.EventHandler(this.btnGenerarReporte_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(503, 34);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(84, 23);
            this.btnGuardar.TabIndex = 47;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // lblTotalRegistros
            // 
            this.lblTotalRegistros.AutoSize = true;
            this.lblTotalRegistros.Location = new System.Drawing.Point(459, 355);
            this.lblTotalRegistros.Name = "lblTotalRegistros";
            this.lblTotalRegistros.Size = new System.Drawing.Size(35, 13);
            this.lblTotalRegistros.TabIndex = 51;
            this.lblTotalRegistros.Text = "label2";
            // 
            // dgvAsistencia
            // 
            this.dgvAsistencia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAsistencia.Location = new System.Drawing.Point(6, 70);
            this.dgvAsistencia.Name = "dgvAsistencia";
            this.dgvAsistencia.Size = new System.Drawing.Size(581, 278);
            this.dgvAsistencia.TabIndex = 50;
            this.dgvAsistencia.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvAsistencia_CurrentCellDirtyStateChanged);
            // 
            // frmAsistenciaAlumnos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(619, 398);
            this.Controls.Add(this.groupBox2);
            this.Name = "frmAsistenciaAlumnos";
            this.Text = "Asistencia de Alumnos";
            this.Load += new System.EventHandler(this.frmAsistenciaAlumnos_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsistencia)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Button btnReporteExcel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGenerarReporte;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblTotalRegistros;
        private System.Windows.Forms.DataGridView dgvAsistencia;
    }
}