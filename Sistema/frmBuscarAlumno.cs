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
    public partial class frmBuscarAlumno : Form
    {

        SqlConnection conexion = new SqlConnection("Data Source=.;Initial Catalog=BDCOLEGIO;Integrated Security=True;TrustServerCertificate=True");

        public int IdAlumnoSeleccionado { get; private set; }
        public string NombreAlumnoSeleccionado { get; private set; }

        public frmBuscarAlumno()
        {
            InitializeComponent();
        }

        private void frmBuscarAlumno_Load(object sender, EventArgs e)
        {
            CargarTodosLosAlumnos();

            // Llenar ComboBox con nombres de columnas
            cmbFiltro.Items.Add("Nombres");
            cmbFiltro.Items.Add("Apellidos");
            cmbFiltro.Items.Add("Sexo");
            //cmbFiltro.Items.Add("Codigo");
            cmbFiltro.SelectedIndex = 0; // Por defecto
        }

        private void CargarTodosLosAlumnos()
        {
            try
            {
                conexion.Open();
                string query = @"SELECT IdAlumno, Nombres, Apellidos, Sexo FROM ALUMNO";
                SqlDataAdapter da = new SqlDataAdapter(query, conexion);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvAlumnos.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBusqueda.Text))
            {
                MessageBox.Show("Ingrese un valor para buscar.");
                return;
            }

            try
            {
                conexion.Open();
                string columna = cmbFiltro.SelectedItem.ToString();
                string query = $@"SELECT IdAlumno, Nombres, Apellidos, Sexo FROM ALUMNO 
                                  WHERE {columna} LIKE @valor";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@valor", "%" + txtBusqueda.Text + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvAlumnos.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron registros. Verifique el valor ingresado para el filtro seleccionado e intente de nuevo.",
                                    "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la búsqueda: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            CargarTodosLosAlumnos();
            txtBusqueda.Clear();
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dgvAlumnos.SelectedRows.Count > 0)
            {
                IdAlumnoSeleccionado = Convert.ToInt32(dgvAlumnos.SelectedRows[0].Cells["IdAlumno"].Value);
                NombreAlumnoSeleccionado = dgvAlumnos.SelectedRows[0].Cells["Nombres"].Value.ToString() + " " +
                                            dgvAlumnos.SelectedRows[0].Cells["Apellidos"].Value.ToString();
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Seleccione un alumno de la lista.");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; // No se seleccionó nada
        }
    }
}
