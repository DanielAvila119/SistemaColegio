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
    public partial class frmJustificarInasistenciaDocente : Form
    {
        private int _idAsistencia;
        public frmJustificarInasistenciaDocente(int idAsistencia)
        {
            InitializeComponent();
            _idAsistencia = idAsistencia;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cboDocentes.SelectedValue == null || string.IsNullOrWhiteSpace(txtJustificacion.Text))
            {
                MessageBox.Show("Debe seleccionar un docente y escribir una justificación.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(Conexion.CN))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
            UPDATE ASISTENCIA_DOCENTE
            SET Justificacion = @Justificacion
            WHERE ID_Docente = @ID_Docente
            AND CAST(Fecha AS DATE) = CAST(GETDATE() AS DATE)", conn);

                cmd.Parameters.AddWithValue("@ID_Docente", cboDocentes.SelectedValue);
                cmd.Parameters.AddWithValue("@Justificacion", txtJustificacion.Text);

                int filas = cmd.ExecuteNonQuery();

                if (filas > 0)
                {
                    MessageBox.Show("Justificación guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se encontró una inasistencia para este docente en la fecha indicada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
