using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema
{
    public partial class frmTrasladoAlumnos : Form
    {
        public frmTrasladoAlumnos()
        {
            InitializeComponent();
            CargarTraslados(); // Carga los registros al iniciar el formulario
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(txtNombreAlumno.Text) ||
                string.IsNullOrWhiteSpace(txtGradoSeccion.Text) ||
                string.IsNullOrWhiteSpace(txtInstituto.Text) ||
                string.IsNullOrWhiteSpace(txtMotivo.Text) ||
                string.IsNullOrWhiteSpace(txtNumeroPartida.Text) ||
                dtpFecha.Value == null ||
                (!rbnIngreso.Checked && !rbnEgreso.Checked))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crear el objeto TrasladoAlumno con los datos del formulario
            TrasladoAlumno objTraslado = new TrasladoAlumno()
            {
                NombreAlumno = txtNombreAlumno.Text,
                GradoSeccion = txtGradoSeccion.Text,
                NumeroPartida = txtNumeroPartida.Text,
                TipoTraslado = rbnIngreso.Checked ? "Ingreso" : "Egreso",
                InstitutoOrigenDestino = txtInstituto.Text,
                FechaTraslado = dtpFecha.Value,
                Motivo = txtMotivo.Text,
                Observaciones = txtObservaciones.Text,
                Activo = true
            };

            // Llamar a la capa de datos para registrar el traslado
            bool resultado = CD_TrasladoAlumnos.RegistrarTraslado(objTraslado);

            if (resultado)
            {
                MessageBox.Show("Traslado registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarControles();
                CargarTraslados();
            }
            else
            {
                MessageBox.Show("Error al registrar el traslado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarTraslados()
        {
            // Cargar los registros en el DataGridView
            dgvTraslados.DataSource = CD_TrasladoAlumnos.ListarTraslados();

            // Ocultar columnas que no deben ser visibles
            if (dgvTraslados.Columns.Contains("IdTraslado")) dgvTraslados.Columns["IdTraslado"].Visible = false;
            if (dgvTraslados.Columns.Contains("Activo")) dgvTraslados.Columns["Activo"].Visible = false;

            // Se asegura de que existan las columnas antes de cambiar su nombre
            if (dgvTraslados.Columns.Contains("NombreAlumno")) dgvTraslados.Columns["NombreAlumno"].HeaderText = "Nombre del alumno";
            if (dgvTraslados.Columns.Contains("GradoSeccion")) dgvTraslados.Columns["GradoSeccion"].HeaderText = "Grado y sección";
            if (dgvTraslados.Columns.Contains("NumeroPartida")) dgvTraslados.Columns["NumeroPartida"].HeaderText = "Número de partida";
            if (dgvTraslados.Columns.Contains("TipoTraslado")) dgvTraslados.Columns["TipoTraslado"].HeaderText = "Tipo de traslado";
            if (dgvTraslados.Columns.Contains("InstitutoOrigenDestino")) dgvTraslados.Columns["InstitutoOrigenDestino"].HeaderText = "Instituto de origen/destino";
            if (dgvTraslados.Columns.Contains("FechaTraslado")) dgvTraslados.Columns["FechaTraslado"].HeaderText = "Fecha de traslado";
            if (dgvTraslados.Columns.Contains("Motivo")) dgvTraslados.Columns["Motivo"].HeaderText = "Motivo";
            if (dgvTraslados.Columns.Contains("Observaciones")) dgvTraslados.Columns["Observaciones"].HeaderText = "Observaciones";
            if (dgvTraslados.Columns.Contains("FechaRegistro")) dgvTraslados.Columns["FechaRegistro"].HeaderText = "Fecha de registro";

            // --- Actualizar el label del total de registros ---
            lblTotalRegistros.Text = "Total de Registros: " + dgvTraslados.RowCount.ToString();
        }

        private void LimpiarControles()
        {
            txtNombreAlumno.Clear();
            txtGradoSeccion.Clear();
            txtNumeroPartida.Clear();
            txtInstituto.Clear();
            txtMotivo.Clear();
            txtObservaciones.Clear();
            rbnIngreso.Checked = false;
            rbnEgreso.Checked = false;
            dtpFecha.Value = DateTime.Now;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarControles();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Validar si hay una fila seleccionada
            if (dgvTraslados.SelectedRows.Count > 0)
            {
                // Obtener el ID del registro de la fila seleccionada
                DataGridViewRow filaSeleccionada = dgvTraslados.SelectedRows[0];
                int idTraslado = Convert.ToInt32(filaSeleccionada.Cells["IdTraslado"].Value);

                // Pedir confirmación al usuario
                DialogResult resultadoDialogo = MessageBox.Show(
                    "¿Está seguro de que desea eliminar este registro?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (resultadoDialogo == DialogResult.Yes)
                {
                    // Llamar a la capa de datos para eliminar el registro
                    bool resultadoEliminacion = CD_TrasladoAlumnos.EliminarTraslado(idTraslado);
                    if (resultadoEliminacion)
                    {
                        MessageBox.Show("Registro eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarTraslados(); // Refrescar la tabla
                    }
                    else
                    {
                        MessageBox.Show("Ocurrió un error al eliminar el registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
