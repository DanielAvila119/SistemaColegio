namespace Sistema
{
    partial class frmConductaAlumnos
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
            this.txtCursoSeccionAlumno = new System.Windows.Forms.TextBox();
            this.txtNombreAlumno = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboTipo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaConducta = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btncancelar = new System.Windows.Forms.Button();
            this.btnguardar = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dtpFechaFiltro = new System.Windows.Forms.DateTimePicker();
            this.btnMostrarTodos = new System.Windows.Forms.Button();
            this.lblTotalRegistros = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.cboFiltro = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dgvConductas = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConductas)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCursoSeccionAlumno
            // 
            this.txtCursoSeccionAlumno.Location = new System.Drawing.Point(427, 24);
            this.txtCursoSeccionAlumno.Name = "txtCursoSeccionAlumno";
            this.txtCursoSeccionAlumno.Size = new System.Drawing.Size(144, 20);
            this.txtCursoSeccionAlumno.TabIndex = 15;
            // 
            // txtNombreAlumno
            // 
            this.txtNombreAlumno.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreAlumno.Location = new System.Drawing.Point(113, 24);
            this.txtNombreAlumno.Name = "txtNombreAlumno";
            this.txtNombreAlumno.Size = new System.Drawing.Size(203, 20);
            this.txtNombreAlumno.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Nombre del alumno:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtNombreAlumno);
            this.groupBox1.Controls.Add(this.txtCursoSeccionAlumno);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(732, 58);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(334, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Grado y Sección:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDescripcion);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cboTipo);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.dtpFechaConducta);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 76);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(732, 85);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalles";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(524, 27);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(202, 49);
            this.txtDescripcion.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(452, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Descripción:";
            // 
            // cboTipo
            // 
            this.cboTipo.FormattingEnabled = true;
            this.cboTipo.Location = new System.Drawing.Point(322, 24);
            this.cboTipo.Name = "cboTipo";
            this.cboTipo.Size = new System.Drawing.Size(107, 21);
            this.cboTipo.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(285, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Tipo:";
            // 
            // dtpFechaConducta
            // 
            this.dtpFechaConducta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpFechaConducta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaConducta.Location = new System.Drawing.Point(127, 25);
            this.dtpFechaConducta.Name = "dtpFechaConducta";
            this.dtpFechaConducta.Size = new System.Drawing.Size(132, 20);
            this.dtpFechaConducta.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fecha de la Conducta:";
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.Red;
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(750, 106);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(88, 23);
            this.btnEliminar.TabIndex = 49;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEditar.ForeColor = System.Drawing.Color.White;
            this.btnEditar.Location = new System.Drawing.Point(750, 78);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(88, 23);
            this.btnEditar.TabIndex = 48;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Location = new System.Drawing.Point(750, 51);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(88, 23);
            this.btnNuevo.TabIndex = 46;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btncancelar
            // 
            this.btncancelar.BackColor = System.Drawing.Color.Red;
            this.btncancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btncancelar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btncancelar.ForeColor = System.Drawing.Color.White;
            this.btncancelar.Location = new System.Drawing.Point(750, 78);
            this.btncancelar.Name = "btncancelar";
            this.btncancelar.Size = new System.Drawing.Size(88, 23);
            this.btncancelar.TabIndex = 50;
            this.btncancelar.Text = "Cancelar";
            this.btncancelar.UseVisualStyleBackColor = false;
            this.btncancelar.Visible = false;
            // 
            // btnguardar
            // 
            this.btnguardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnguardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnguardar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnguardar.ForeColor = System.Drawing.Color.White;
            this.btnguardar.Location = new System.Drawing.Point(750, 51);
            this.btnguardar.Name = "btnguardar";
            this.btnguardar.Size = new System.Drawing.Size(88, 23);
            this.btnguardar.TabIndex = 47;
            this.btnguardar.Text = "Guardar";
            this.btnguardar.UseVisualStyleBackColor = false;
            this.btnguardar.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dtpFechaFiltro);
            this.groupBox3.Controls.Add(this.btnMostrarTodos);
            this.groupBox3.Controls.Add(this.lblTotalRegistros);
            this.groupBox3.Controls.Add(this.txtFilter);
            this.groupBox3.Controls.Add(this.cboFiltro);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.dgvConductas);
            this.groupBox3.Location = new System.Drawing.Point(12, 166);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(826, 275);
            this.groupBox3.TabIndex = 51;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Lista de Conductas Registradas";
            // 
            // dtpFechaFiltro
            // 
            this.dtpFechaFiltro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpFechaFiltro.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFiltro.Location = new System.Drawing.Point(457, 18);
            this.dtpFechaFiltro.Name = "dtpFechaFiltro";
            this.dtpFechaFiltro.Size = new System.Drawing.Size(132, 20);
            this.dtpFechaFiltro.TabIndex = 19;
            this.dtpFechaFiltro.ValueChanged += new System.EventHandler(this.dtpFechaFiltro_ValueChanged);
            // 
            // btnMostrarTodos
            // 
            this.btnMostrarTodos.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnMostrarTodos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMostrarTodos.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMostrarTodos.ForeColor = System.Drawing.Color.White;
            this.btnMostrarTodos.Location = new System.Drawing.Point(671, 16);
            this.btnMostrarTodos.Name = "btnMostrarTodos";
            this.btnMostrarTodos.Size = new System.Drawing.Size(149, 23);
            this.btnMostrarTodos.TabIndex = 53;
            this.btnMostrarTodos.Text = "Mostrar todos los registros";
            this.btnMostrarTodos.UseVisualStyleBackColor = false;
            this.btnMostrarTodos.Click += new System.EventHandler(this.btnMostrarTodos_Click);
            // 
            // lblTotalRegistros
            // 
            this.lblTotalRegistros.AutoSize = true;
            this.lblTotalRegistros.BackColor = System.Drawing.Color.White;
            this.lblTotalRegistros.ForeColor = System.Drawing.Color.Black;
            this.lblTotalRegistros.Location = new System.Drawing.Point(675, 253);
            this.lblTotalRegistros.Name = "lblTotalRegistros";
            this.lblTotalRegistros.Size = new System.Drawing.Size(13, 13);
            this.lblTotalRegistros.TabIndex = 23;
            this.lblTotalRegistros.Text = "0";
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(289, 18);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(153, 20);
            this.txtFilter.TabIndex = 21;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // cboFiltro
            // 
            this.cboFiltro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cboFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFiltro.FormattingEnabled = true;
            this.cboFiltro.Location = new System.Drawing.Point(149, 18);
            this.cboFiltro.Name = "cboFiltro";
            this.cboFiltro.Size = new System.Drawing.Size(121, 21);
            this.cboFiltro.TabIndex = 20;
            this.cboFiltro.SelectedIndexChanged += new System.EventHandler(this.cboFiltro_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(87, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Filtrar  por:";
            // 
            // dgvConductas
            // 
            this.dgvConductas.AllowUserToAddRows = false;
            this.dgvConductas.AllowUserToDeleteRows = false;
            this.dgvConductas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConductas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvConductas.Location = new System.Drawing.Point(6, 48);
            this.dgvConductas.MultiSelect = false;
            this.dgvConductas.Name = "dgvConductas";
            this.dgvConductas.ReadOnly = true;
            this.dgvConductas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvConductas.Size = new System.Drawing.Size(814, 198);
            this.dgvConductas.TabIndex = 18;
            this.dgvConductas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvConductas_CellClick);
            this.dgvConductas.SelectionChanged += new System.EventHandler(this.dgvConductas_SelectionChanged);
            // 
            // frmConductaAlumnos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 453);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.btncancelar);
            this.Controls.Add(this.btnguardar);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmConductaAlumnos";
            this.Text = "Conducta de Alumnos";
            this.Load += new System.EventHandler(this.frmConductaAlumnos_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConductas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtCursoSeccionAlumno;
        private System.Windows.Forms.TextBox txtNombreAlumno;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboTipo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaConducta;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btncancelar;
        private System.Windows.Forms.Button btnguardar;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblTotalRegistros;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.ComboBox cboFiltro;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dgvConductas;
        private System.Windows.Forms.Button btnMostrarTodos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaFiltro;
    }
}