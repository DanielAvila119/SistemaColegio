using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaDatos;
using CapaModelo;

namespace Sistema
{
    public partial class frmConductaAlumnos : Form
    {
        private CD_ConductaAlumno datos = new CD_ConductaAlumno();
        private string modo = ""; // "NUEVO" o "EDITAR"
        private int idConductaSeleccionada = -1;
        private int idUsuarioActual = 1; // Asigna aquí el usuario logueado
        private int idConductaSeleccionado = 0;
        public frmConductaAlumnos()
        {
            InitializeComponent();
        }

        private void frmConductaAlumnos_Load(object sender, EventArgs e)
        {
            // Ajustar automáticamente las columnas al contenido y al ancho del DataGridView
            dgvConductas.RowTemplate.Height = 40;
            dgvConductas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvConductas.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvConductas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvConductas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            cboFiltro.Items.Clear();
            cboFiltro.Items.Add("Nombre del alumno");
            cboFiltro.Items.Add("Curso/Sección");
            cboFiltro.Items.Add("Tipo de conducta");
            cboFiltro.Items.Add("Fecha de conducta");
            cboFiltro.SelectedIndex = 0;

            txtFilter.Enabled = true;
            dtpFechaFiltro.Enabled = false;

            //CargarConductas(); // carga todos los datos al iniciar

            // Poblar combobox UNA sola vez
            PopulateTipo();
            ListarConductas();
            EstadoInicial();
        }

        private void cboFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Enabled = false;
            dtpFechaFiltro.Enabled = false;

            if (cboFiltro.SelectedItem.ToString() == "Nombre del alumno" ||
                cboFiltro.SelectedItem.ToString() == "Curso/Sección")
            {
                txtFilter.Enabled = true;
                txtFilter.Clear();
            }
            else if (cboFiltro.SelectedItem.ToString() == "Fecha de conducta")
            {
                dtpFechaFiltro.Enabled = true;
            }
            else if (cboFiltro.SelectedItem.ToString() == "Tipo de conducta")
            {
                // Aplica filtro automático
                FiltrarConductasPorTipo("Mérito"); // por defecto si quieres
            }
        }
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (cboFiltro.SelectedItem.ToString() == "Nombre del alumno")
            {
                FiltrarConductas("NombreAlumno", txtFilter.Text);
            }
            else if (cboFiltro.SelectedItem.ToString() == "Curso/Sección")
            {
                FiltrarConductas("CursoSeccion", txtFilter.Text);
            }
        }
        private void dtpFechaFiltro_ValueChanged(object sender, EventArgs e)
        {
            if (cboFiltro.SelectedItem.ToString() == "Fecha de conducta")
            {
                FiltrarConductasPorFecha(dtpFechaFiltro.Value);
            }
        }


        private void PopulateTipo()
        {
            // Evita duplicados: agrega solo si no tiene elementos
            if (cboTipo.Items.Count == 0)
            {
                cboTipo.Items.Add("Mérito");
                cboTipo.Items.Add("Falta");
            }
        }
        private void ListarConductas()
        {
            var lista = datos.Listar();
            dgvConductas.DataSource = lista;
            lblTotalRegistros.Text = $"Total de registros: {lista.Count}";
            dgvConductas.ClearSelection();
            // Desactivar botones si no hay selección
            btnEditar.Enabled = lista.Count > 0 ? false : false;
            btnEliminar.Enabled = lista.Count > 0 ? false : false;
        }
        private void EstadoInicial()
        {
            modo = "";
            idConductaSeleccionada = 0;

            HabilitarCampos(false);
            LimpiarCampos();

            // Botones: Nuevo y Editar en modo inicial
            btnNuevo.Text = "Nuevo";
            btnNuevo.BackColor = SystemColors.Highlight;
            btnEditar.ForeColor = Color.White;
            btnEliminar.ForeColor = Color.White;

            btnEditar.Text = "Editar";
            btnEditar.BackColor = SystemColors.Highlight;
            btnEditar.ForeColor = Color.White;
            btnEliminar.ForeColor = Color.White;

            // Eliminar será botón de eliminar por defecto
            btnEliminar.Text = "Eliminar";
            btnEliminar.BackColor = Color.Red;
            btnEditar.ForeColor = Color.White;
            btnEliminar.ForeColor = Color.White;

            btnNuevo.Enabled = true;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btncancelar.Enabled = false; // opcional, puedes ocultarlo
        }

        private void HabilitarCampos(bool estado)
        {
            txtNombreAlumno.Enabled = estado;
            txtCursoSeccionAlumno.Enabled = estado;
            dtpFechaConducta.Enabled = estado;
            cboTipo.Enabled = estado;
            txtDescripcion.Enabled = estado;
        }

        private void LimpiarCampos()
        {
            txtNombreAlumno.Clear();
            txtCursoSeccionAlumno.Clear();
            cboTipo.SelectedIndex = -1;
            txtDescripcion.Clear();
            dtpFechaConducta.Value = DateTime.Now;
        }

        /*private void btnBuscarAlumno_Click(object sender, EventArgs e)
        {
            using (frmBuscarAlumno buscar = new frmBuscarAlumno())
            {
                if (buscar.ShowDialog() == DialogResult.OK)
                {
                    // Solo actualizar si el usuario seleccionó uno diferente
                    if (idAlumnoSeleccionado != buscar.IdAlumnoSeleccionado)
                    {
                        idAlumnoSeleccionado = buscar.IdAlumnoSeleccionado;
                        txtCursoSeccionAlumno.Text = idAlumnoSeleccionado.ToString();
                        txtNombreAlumno.Text = buscar.NombreAlumnoSeleccionado;
                    }
                }
            }
        }*/

        // Botón NUEVO (actúa como "Nuevo" -> habilita, y luego "Guardar" al volver a pulsar)
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (btnNuevo.Text == "Nuevo")
            {
                modo = "NUEVO";
                LimpiarCampos();
                HabilitarCampos(true);

                btnNuevo.Text = "Guardar";
                btnNuevo.BackColor = SystemColors.Highlight;
                btnEditar.ForeColor = Color.White;
                btnEliminar.ForeColor = Color.White;

                // Convertimos ELIMINAR en CANCELAR temporalmente
                btnEliminar.Text = "Cancelar";
                btnEliminar.BackColor = Color.Red;
                btnEditar.ForeColor = Color.White;
                btnEliminar.ForeColor = Color.White;
                btnEliminar.Enabled = true;

                btnEditar.Enabled = false;
                btncancelar.Enabled = true;
            }
            else // Guardar nuevo
            {
                if (!ValidarCampos()) return;

                var obj = new ConductaAlumno
                {
                    NombreAlumno = txtNombreAlumno.Text.Trim(),
                    CursoSeccion = txtCursoSeccionAlumno.Text.Trim(),
                    FechaConducta = dtpFechaConducta.Value.Date,
                    Tipo = cboTipo.Text,
                    Descripcion = txtDescripcion.Text.Trim(),
                    IdUsuario = idUsuarioActual
                };

                bool ok = datos.Registrar(obj);
                if (ok)
                    MessageBox.Show("Registro guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Error al guardar el registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Volver al estado inicial
                ListarConductas();
                EstadoInicial();
            }

        }
        // Botón ELIMINAR (si estamos en modoNuevo actúa como Cancelar, si no => eliminar registro seleccionado)
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idConductaSeleccionado == -1)
            {
                MessageBox.Show("Seleccione un registro antes de eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult respuesta = MessageBox.Show("¿Está seguro de eliminar este registro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                bool eliminado = datos.Eliminar(idConductaSeleccionada);

                if (eliminado)
                {
                    MessageBox.Show("Registro eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //CargarConductas(); // Recargar la lista sin el registro
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Si estamos en modo (NUEVO o EDITAR) -> CANCELAR
            if (modo == "NUEVO" || modo == "EDITAR")
            {
                // Confirmación opcional antes de cancelar (no necesario)
                // Restablecer todo
                EstadoInicial();
                return;
            }

            // Si no estamos en modo, ELIMINAR registro seleccionado
            if (dgvConductas.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un registro para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var obj = dgvConductas.CurrentRow.DataBoundItem as ConductaAlumno;
            if (obj == null)
            {
                MessageBox.Show("No se pudo identificar el registro seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("¿Eliminar este registro?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool ok = datos.Eliminar(obj.IdConducta);
                if (ok)
                    MessageBox.Show("Registro eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Error al eliminar el registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                ListarConductas();
                EstadoInicial();
            }
        }
        // -------------------- VALIDACIÓN DE CAMPOS --------------------
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombreAlumno.Text))
            {
                MessageBox.Show("Ingrese el nombre y apellido del alumno.", "Falta dato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreAlumno.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCursoSeccionAlumno.Text))
            {
                MessageBox.Show("Ingrese el curso y sección del alumno.", "Falta dato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCursoSeccionAlumno.Focus();
                return false;
            }

            if (cboTipo.SelectedIndex < 0 || string.IsNullOrWhiteSpace(cboTipo.Text))
            {
                MessageBox.Show("Seleccione el tipo de conducta (Mérito o Falta).", "Falta dato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTipo.DroppedDown = true;
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Ingrese la descripción de la conducta.", "Falta dato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescripcion.Focus();
                return false;
            }

            // (opcional) valida fecha si es necesario
            // if (dtpFechaConducta.Value > DateTime.Today) { ... }

            return true;
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dgvConductas.CurrentRow != null)
            {
                idConductaSeleccionada = Convert.ToInt32(dgvConductas.CurrentRow.Cells["IdConducta"].Value);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Seleccione un registro.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        // Evento para cuando el usuario selecciona una fila del datagrid (habilita editar/eliminar)
        private void dgvConductas_SelectionChanged(object sender, EventArgs e)
        {
            //Cambiarle nombre a las columnas del DataGridView
            if (dgvConductas.Columns.Contains("NombreAlumno"))
            {
                dgvConductas.Columns["NombreAlumno"].HeaderText = "Nombre del alumno";
            }

            if (dgvConductas.Columns.Contains("CursoSeccion"))
            {
                dgvConductas.Columns["CursoSeccion"].HeaderText = "Grado y Sección";
            }

            if (dgvConductas.Columns.Contains("FechaConducta"))
            {
                dgvConductas.Columns["FechaConducta"].HeaderText = "Fecha de Conducta";
            }

            if (dgvConductas.Columns.Contains("Descripcion"))
            {
                dgvConductas.Columns["Descripcion"].HeaderText = "Descripción";
            }

            if (dgvConductas.Columns.Contains("FechaRegistro"))
            {
                dgvConductas.Columns["FechaRegistro"].HeaderText = "Fecha de registro";
            }
            // Ocultar ID siempre
            if (dgvConductas.Columns.Contains("IdConducta"))
                dgvConductas.Columns["IdConducta"].Visible = false;
            if (dgvConductas.Columns.Contains("IdUsuario"))
                dgvConductas.Columns["IdUsuario"].Visible = false;
            if (dgvConductas.Columns.Contains("Activo"))
                dgvConductas.Columns["Activo"].Visible = false;
            bool haySeleccion = dgvConductas.SelectedRows.Count > 0 || dgvConductas.CurrentRow != null;
            btnEditar.Enabled = haySeleccion;
            btnEliminar.Enabled = haySeleccion;

            // Opcional: al seleccionar un registro, precargar en los textbox (sin entrar en modo editar)
            if (dgvConductas.CurrentRow != null && modo == "")
            {
                var obj = dgvConductas.CurrentRow.DataBoundItem as ConductaAlumno;
                if (obj != null)
                {
                    txtNombreAlumno.Text = obj.NombreAlumno;
                    txtCursoSeccionAlumno.Text = obj.CursoSeccion;
                    dtpFechaConducta.Value = obj.FechaConducta;
                    cboTipo.Text = obj.Tipo;
                    txtDescripcion.Text = obj.Descripcion;
                    idConductaSeleccionada = obj.IdConducta;
                }
            }
        }
        private void dgvConducta_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        // Botón EDITAR (habilita campos para editar registro seleccionado)
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (btnEditar.Text == "Editar")
            {
                if (idConductaSeleccionado == 0)
                {
                    MessageBox.Show("Debe seleccionar un registro antes de editar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Cambiar a modo edición
                btnEditar.Text = "Guardar";
                btnEditar.BackColor = Color.Green;
                btnNuevo.Enabled = false;
                btnEliminar.Enabled = false;

                // Habilitar campos
                txtNombreAlumno.Enabled = true;
                txtCursoSeccionAlumno.Enabled = true;
                dtpFechaConducta.Enabled = true;
                cboTipo.Enabled = true;
                txtDescripcion.Enabled = true;
            }
            else // Guardar cambios
            {
                // Validación de campos vacíos
                if (string.IsNullOrWhiteSpace(txtNombreAlumno.Text) ||
                    string.IsNullOrWhiteSpace(txtCursoSeccionAlumno.Text) ||
                    cboTipo.SelectedIndex == -1 ||
                    string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    MessageBox.Show("Debe completar todos los campos antes de guardar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Llamada a la capa de datos para actualizar
                datos.Editar(new ConductaAlumno
                {
                    IdConducta = idConductaSeleccionado,
                    NombreAlumno = txtNombreAlumno.Text.Trim(),
                    CursoSeccion = txtCursoSeccionAlumno.Text.Trim(),
                    FechaConducta = dtpFechaConducta.Value,
                    Tipo = cboTipo.SelectedItem.ToString(),
                    Descripcion = txtDescripcion.Text.Trim(),
                    //IdUsuario = usuario.IdUsuario // o como obtengas el usuario logueado
                });

                MessageBox.Show("Registro actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refrescar el DataGridView
                //CargarConductas();

                // Volver a estado inicial
                btnEditar.Text = "Editar";
                btnEditar.BackColor = Color.DodgerBlue;
                btnNuevo.Enabled = true;
                btnEliminar.Enabled = true;
            }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            /*string filtro = cboFiltro.SelectedItem.ToString();
            string valor = txtFilter.Text.Trim();

            // Si "Todos" => recarga todo
            if (filtro == "Todos" || string.IsNullOrWhiteSpace(valor))
            {
                //CargarConductas();
                return;
            }

            // Filtrado simple: mérito/falta o por nombre de alumno
            DataTable dt = datosConducta.ListarConductas(); // obtener todo y filtrar en memoria (fácil)
            DataView dv = dt.DefaultView;

            if (filtro == "Mérito" || filtro == "Falta")
                dv.RowFilter = $"Tipo LIKE '%{filtro}%'";
            else if (filtro == "Alumno")
                dv.RowFilter = $"Alumno LIKE '%{valor}%'";

            if (dv.Count == 0)
                MessageBox.Show("No existen registros para el valor ingresado.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);

            dgvConductas.DataSource = dv.ToTable();*/
        }
        private void btncancelar_Click(object sender, EventArgs e)
        {
            EstadoInicial();
        }
        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            //CargarConductas();
            txtFilter.Clear();
            cboFiltro.SelectedIndex = 0;
        }
        private void FiltrarConductas(string campo, string valor)
        {
            dgvConductas.DataSource = datos.FiltrarConductas(campo, valor);
            lblTotalRegistros.Text = $"Total de registros: {dgvConductas.Rows.Count}";
        }

        private void FiltrarConductasPorTipo(string tipo)
        {
            dgvConductas.DataSource = datos.FiltrarConductasPorTipo(tipo);
            lblTotalRegistros.Text = $"Total de registros: {dgvConductas.Rows.Count}";
        }

        private void FiltrarConductasPorFecha(DateTime fecha)
        {
            dgvConductas.DataSource = datos.FiltrarConductasPorFecha(fecha);
            lblTotalRegistros.Text = $"Total de registros: {dgvConductas.Rows.Count}";
        }

        private void dgvConductas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex >= 0) // Para evitar encabezados
            {
                DataGridViewRow fila = dgvConductas.Rows[e.RowIndex];

                idConductaSeleccionada = Convert.ToInt32(fila.Cells["IdConducta"].Value);

                txtNombreAlumno.Text = fila.Cells["NombreAlumno"].Value.ToString();
                txtCursoSeccionAlumno.Text = fila.Cells["CursoSeccion"].Value.ToString();
                dtpFechaConducta.Value = Convert.ToDateTime(fila.Cells["FechaConducta"].Value);
                cboTipo.SelectedItem = fila.Cells["Tipo"].Value.ToString();
                txtDescripcion.Text = fila.Cells["Descripcion"].Value.ToString();
            }

            if (e.RowIndex >= 0) // Evita errores al hacer click en encabezados
            {
                DataGridViewRow row = dgvConductas.Rows[e.RowIndex];

                // Capturar el ID de la conducta
                if (row.Cells["IdConducta"].Value != null)
                {
                    idConductaSeleccionado = Convert.ToInt32(row.Cells["IdConducta"].Value);
                }

                // Llenar los textbox
                txtNombreAlumno.Text = row.Cells["NombreAlumno"].Value?.ToString();
                txtCursoSeccionAlumno.Text = row.Cells["CursoSeccion"].Value?.ToString();
                dtpFechaConducta.Value = Convert.ToDateTime(row.Cells["FechaConducta"].Value);
                cboTipo.SelectedItem = row.Cells["Tipo"].Value?.ToString();
                txtDescripcion.Text = row.Cells["Descripcion"].Value?.ToString();

                // Habilitar botón Editar
                btnEditar.Enabled = true;
                btnNuevo.Enabled = false;
            }
        }
    }
}
