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
    public partial class frmMarcarAsistenciaDocentes : Form
    {
        private int _idAsistencia;
        private CD_AsistenciaDocente oDatos = new CD_AsistenciaDocente();
        public frmMarcarAsistenciaDocentes(int idAsistencia)
        {
            InitializeComponent();
            _idAsistencia = idAsistencia;
        }
        private void frmMarcarAsistencia_Load(object sender, EventArgs e)
        {
            // Cargar datos de docentes, horarios, etc.
            CargarDatosAsistencia();

            // Agregar columna de checkbox si no existe
            if (!dgvMarcarAsistenciaDoc.Columns.Contains("Asistio"))
            {
                DataGridViewCheckBoxColumn chkAsistio = new DataGridViewCheckBoxColumn();
                chkAsistio.HeaderText = "Asistió";
                chkAsistio.Name = "Asistio";
                chkAsistio.Width = 60;
                dgvMarcarAsistenciaDoc.Columns.Add(chkAsistio);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.CN))
            {
                cn.Open();

                foreach (DataGridViewRow row in dgvMarcarAsistenciaDoc.Rows)
                {
                    if (row.IsNewRow) continue;

                    int idDocente = Convert.ToInt32(row.Cells["IdDocente"].Value);
                    bool asistio = Convert.ToBoolean(row.Cells["Asistio"].Value ?? false);

                    string query = "INSERT INTO ASISTENCIA_DOCENTE (IdDocente, Fecha, Asistio) VALUES (@IdDocente, @Fecha, @Asistio)";

                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        cmd.Parameters.AddWithValue("@IdDocente", idDocente);
                        cmd.Parameters.AddWithValue("@Fecha", DateTime.Now.Date);
                        cmd.Parameters.AddWithValue("@Asistio", asistio);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Asistencia guardada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void CargarDatosAsistencia()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.CN))
                {
                    conn.Open();

                    string query = @"
                SELECT a.IdAlumno,
                       a.NombreCompleto,
                       ISNULL(asi.Asistio, 0) AS Asistio
                FROM ALUMNO a
                LEFT JOIN ASISTENCIA_DOCENTE asi 
                    ON asi.IdDocente = a.IdDocente
                    AND asi.Fecha = @Fecha";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Fecha", dtpFecha.Value.Date); // DateTimePicker para la fecha

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvMarcarAsistenciaDoc.Columns.Clear(); // Limpiar columnas previas
                    dgvMarcarAsistenciaDoc.DataSource = dt;

                    // Ajustar columnas
                    dgvMarcarAsistenciaDoc.Columns["IdDocente"].ReadOnly = true;
                    dgvMarcarAsistenciaDoc.Columns["NombreCompleto"].ReadOnly = true;

                    // Convertir columna Asistio a checkbox
                    DataGridViewCheckBoxColumn chkAsistio = new DataGridViewCheckBoxColumn();
                    chkAsistio.Name = "chkAsistio";
                    chkAsistio.HeaderText = "Asistió";
                    chkAsistio.DataPropertyName = "Asistio";
                    dgvMarcarAsistenciaDoc.Columns.Remove("Asistio"); // quitar la columna int
                    dgvMarcarAsistenciaDoc.Columns.Add(chkAsistio);

                    // Ajustar ancho
                    dgvMarcarAsistenciaDoc.Columns["chkAsistio"].Width = 60;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }

    }
}
