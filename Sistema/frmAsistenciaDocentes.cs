using CapaDatos;
using CapaModelo;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema
{
    public partial class frmAsistenciaDocentes : Form
    {
        private bool asistenciaHoyExiste = false;
        public frmAsistenciaDocentes()
        {
            InitializeComponent();
            // Asegura que el check se “confirme” al hacer clic
            dgvAsistencia.CurrentCellDirtyStateChanged += dgvAsistencia_CurrentCellDirtyStateChanged;
            dgvAsistencia.CellValueChanged += dgvAsistencia_CellValueChanged; // opcional para UX
        }
        private void frmAsistenciaDocente_Load(object sender, EventArgs e)
        {
            // Ajustar automáticamente las columnas al contenido y al ancho del DataGridView
            dgvAsistencia.RowTemplate.Height = 40;
            dgvAsistencia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAsistencia.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvAsistencia.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvAsistencia.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvAsistencia.AllowUserToAddRows = false;

            //Cambiarle nombre a las columnas del DataGridView
            if (dgvAsistencia.Columns.Contains("DOCENTE"))
            {
                dgvAsistencia.Columns["DOCENTE"].HeaderText = "Docente";
            }
            if (dgvAsistencia.Columns.Contains("Asistio"))
            {
                dgvAsistencia.Columns["Asistio"].HeaderText = "Asistió";
            }

            if (dgvAsistencia.Columns.Contains("Justificacion"))
            {
                dgvAsistencia.Columns["Justificacion"].HeaderText = "Justificación";
            }
            // Hacer que la columna "Asistió" sea más angosta
            if (dgvAsistencia.Columns.Contains("Asistió"))
            {
                dgvAsistencia.Columns["Asistió"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvAsistencia.Columns["Asistió"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // Forzar que las demás columnas llenen el espacio restante
            foreach (DataGridViewColumn col in dgvAsistencia.Columns)
            {
                if (col.Name != "Asistió")
                {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            dtpFecha.Value = DateTime.Now; // asegura que esté en la fecha de hoy
            CargarAsistenciaPorFecha(dtpFecha.Value);
            CargarDatosAsistencia(); // unificado
        }

        // ========= CARGA UNIFICADA =========
        private void CargarDatosAsistencia()
        {

            using (var con = new SqlConnection(Conexion.CN))
            {
                con.Open();

                // 1) ¿Ya hay asistencia hoy?
                var cmdCount = new SqlCommand(
                    "SELECT COUNT(*) FROM ASISTENCIA_DOCENTE WHERE CAST(Fecha AS date) = CAST(GETDATE() AS date)", con);
                int count = (int)cmdCount.ExecuteScalar();
                asistenciaHoyExiste = (count > 0);

                if (asistenciaHoyExiste)
                {
                    // 2) Cargar asistencia de hoy (solo ver)
                    var cmd = new SqlCommand(@"
                        SELECT a.IdDocente,
                               d.Nombres + ' ' + d.Apellidos AS Docente,
                               a.Asistio,
                               a.Justificacion
                        FROM ASISTENCIA_DOCENTE a
                        INNER JOIN DOCENTE d ON d.IdDocente = a.IdDocente
                        WHERE CAST(a.Fecha AS date) = CAST(GETDATE() AS date)
                        ORDER BY Docente;", con);

                    var dt = new DataTable();
                    new SqlDataAdapter(cmd).Fill(dt);

                    dgvAsistencia.AutoGenerateColumns = true; // o false si usas columnas manuales
                    dgvAsistencia.DataSource = dt;

                    ConfigurarColumnasBinding(); // asigna DataPropertyName si usas columnas diseñadas
                    PrepararGridSoloLectura();
                    ActualizarTotal(dt.Rows.Count);
                }
                else
                {
                    // 3) No hay asistencia aún → preparar para registrar hoy
                    var cmd = new SqlCommand(@"
                        SELECT IdDocente,
                               Nombres + ' ' + Apellidos AS Docente,
                               CAST(0 AS bit) AS Asistio,
                               '' AS Justificacion
                        FROM DOCENTE
                        ORDER BY Docente;", con);

                    var dt = new DataTable();
                    new SqlDataAdapter(cmd).Fill(dt);

                    dgvAsistencia.AutoGenerateColumns = true; // o false si usas columnas diseñadas
                    dgvAsistencia.DataSource = dt;

                    ConfigurarColumnasBinding();
                    PrepararGridEditable();
                    ActualizarTotal(dt.Rows.Count);
                }
            }

            // Ocultar ID siempre
            if (dgvAsistencia.Columns.Contains("IdDocente"))
                dgvAsistencia.Columns["IdDocente"].Visible = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (asistenciaHoyExiste)
            {
                MessageBox.Show("La asistencia de hoy ya fue registrada.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Confirma el check si el usuario no salió de la celda
            dgvAsistencia.EndEdit();

            // Validar: si Asistio == false → Justificación obligatoria
            foreach (DataGridViewRow r in dgvAsistencia.Rows)
            {
                if (r.IsNewRow) continue;

                bool asistio = false;
                if (r.Cells["Asistio"].Value != null && r.Cells["Asistio"].Value != DBNull.Value)
                    asistio = Convert.ToBoolean(r.Cells["Asistio"].Value);

                string just = r.Cells["Justificacion"].Value?.ToString().Trim() ?? string.Empty;

                if (!asistio && string.IsNullOrEmpty(just))
                {
                    string docente = r.Cells["Docente"].Value?.ToString() ?? "(sin nombre)";
                    MessageBox.Show($"El docente \"{docente}\" no asistió. Debe ingresar una justificación.",
                        "Falta justificación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgvAsistencia.CurrentCell = r.Cells["Justificacion"];
                    dgvAsistencia.BeginEdit(true);
                    return;
                }
            }

            // Guardar con transacción
            using (var con = new SqlConnection(Conexion.CN))
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                {
                    try
                    {
                        var sql = @"INSERT INTO ASISTENCIA_DOCENTE (IdDocente, Fecha, Asistio, Justificacion)
                                    VALUES (@IdDocente, CAST(GETDATE() AS date), @Asistio, @Justificacion)";

                        foreach (DataGridViewRow r in dgvAsistencia.Rows)
                        {
                            if (r.IsNewRow) continue;

                            int idDocente = Convert.ToInt32(r.Cells["IdDocente"].Value);
                            bool asistio = r.Cells["Asistio"].Value != null && r.Cells["Asistio"].Value != DBNull.Value
                                           ? Convert.ToBoolean(r.Cells["Asistio"].Value)
                                           : false;
                            string just = r.Cells["Justificacion"].Value?.ToString().Trim() ?? string.Empty;

                            using (var cmd = new SqlCommand(sql, con, tx))
                            {
                                cmd.Parameters.AddWithValue("@IdDocente", idDocente);
                                cmd.Parameters.AddWithValue("@Asistio", asistio);
                                cmd.Parameters.AddWithValue("@Justificacion", just);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        tx.Commit();
                        asistenciaHoyExiste = true;
                        MessageBox.Show("Asistencia registrada correctamente.", "OK",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Recarga en modo solo lectura para reflejar lo guardado
                        CargarDatosAsistencia();
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        MessageBox.Show("Error al guardar la asistencia: " + ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }
        // ========= UTILIDADES UI =========
        private void ConfigurarColumnasBinding()
        {
            if (dgvAsistencia.Columns.Contains("DOCENTE"))
            {
                dgvAsistencia.Columns["DOCENTE"].HeaderText = "Docente";
            }
            if (dgvAsistencia.Columns.Contains("Asistio"))
            {
                dgvAsistencia.Columns["Asistio"].HeaderText = "Asistió";
            }

            if (dgvAsistencia.Columns.Contains("Justificacion"))
            {
                dgvAsistencia.Columns["Justificacion"].HeaderText = "Justificación";
            }
            try
            {
                if (dgvAsistencia.Columns.Contains("Docente"))
                    dgvAsistencia.Columns["Docente"].DataPropertyName = "Docente";

                if (dgvAsistencia.Columns.Contains("Asistio"))
                    dgvAsistencia.Columns["Asistio"].DataPropertyName = "Asistio";

                if (dgvAsistencia.Columns.Contains("Justificacion"))
                    dgvAsistencia.Columns["Justificacion"].DataPropertyName = "Justificacion";

                if (dgvAsistencia.Columns.Contains("IdDocente"))
                    dgvAsistencia.Columns["IdDocente"].DataPropertyName = "IdDocente";
            }
            catch { /* ignorar si no aplica */ }
        }

        private void PrepararGridEditable()
        {
            if (dgvAsistencia.Columns.Contains("DOCENTE"))
            {
                dgvAsistencia.Columns["DOCENTE"].HeaderText = "Docente";
            }
            if (dgvAsistencia.Columns.Contains("Asistio"))
            {
                dgvAsistencia.Columns["Asistio"].HeaderText = "Asistió";
            }

            if (dgvAsistencia.Columns.Contains("Justificacion"))
            {
                dgvAsistencia.Columns["Justificacion"].HeaderText = "Justificación";
            }
            dgvAsistencia.ReadOnly = false;

            // Columnas específicas de solo lectura/edición
            if (dgvAsistencia.Columns.Contains("Docente"))
                dgvAsistencia.Columns["Docente"].ReadOnly = true;

            if (dgvAsistencia.Columns.Contains("Asistio"))
                dgvAsistencia.Columns["Asistio"].ReadOnly = false;

            if (dgvAsistencia.Columns.Contains("Justificacion"))
                dgvAsistencia.Columns["Justificacion"].ReadOnly = false;

            btnGuardar.Enabled = true;
        }

        private void PrepararGridSoloLectura()
        {
            dgvAsistencia.ReadOnly = true;
            btnGuardar.Enabled = false;
        }

        private void ActualizarTotal(int total)
        {
            if (lblTotalRegistros != null)
                lblTotalRegistros.Text = $"Total registros: {total}";
        }

        // Confirma el cambio del checkbox inmediatamente
        private void dgvAsistencia_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvAsistencia.IsCurrentCellDirty)
                dgvAsistencia.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        // UX: si marca Asistio = true, limpia la justificación; si Asistio = false y estaba vacía, no obliga hasta guardar
        private void dgvAsistencia_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvAsistencia.Columns[e.ColumnIndex].Name == "Asistio")
            {
                bool asistio = false;
                var val = dgvAsistencia.Rows[e.RowIndex].Cells["Asistio"].Value;
                if (val != null && val != DBNull.Value) asistio = Convert.ToBoolean(val);

                if (asistio)
                {
                    dgvAsistencia.Rows[e.RowIndex].Cells["Justificacion"].Value = "";
                }
            }
        }
        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            CargarAsistenciaPorFecha(dtpFecha.Value);
        }

        private void CargarAsistenciaPorFecha(DateTime fecha)
        {
            string query = @"SELECT a.IdAsistencia, CONCAT(d.Nombres, ' ', d.Apellidos) AS DOCENTE, a.Asistio, 
               a.Justificacion FROM ASISTENCIA_DOCENTE a INNER JOIN DOCENTE d ON a.IdDocente = d.IdDocente
               WHERE CONVERT(date, a.Fecha) = @Fecha";

            using (SqlConnection con = new SqlConnection(Conexion.CN))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Fecha", fecha.Date);
                con.Open();

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                dgvAsistencia.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show($"No se encuentran registros en la fecha {fecha:dd/MM/yyyy}",
                                    "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Ajuste visual
                    dgvAsistencia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvAsistencia.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    if (dgvAsistencia.Columns.Contains("Asistio"))
                        dgvAsistencia.Columns["Asistio"].HeaderText = "Asistió";

                    if (dgvAsistencia.Columns.Contains("Justificacion"))
                        dgvAsistencia.Columns["Justificacion"].HeaderText = "Justificación";
                }
            }
            // Ocultar ID siempre
            if (dgvAsistencia.Columns.Contains("IdAsistencia"))
                dgvAsistencia.Columns["IdAsistencia"].Visible = false;
        }

        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            if (dgvAsistencia.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para generar el reporte.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF Files|*.pdf";
                saveFileDialog.Title = "Guardar Reporte de Asistencia";
                saveFileDialog.FileName = $"AsistenciaDocentes_{dtpFecha.Value:ddMMyyyy}.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Document doc = new Document(PageSize.A4, 20, 20, 20, 20);
                    PdfWriter.GetInstance(doc, new FileStream(saveFileDialog.FileName, FileMode.Create));
                    doc.Open();

                    // Título
                    Paragraph tituloC = new Paragraph($"Instituto Miguel Paz Barahona", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14));
                    Paragraph titulo = new Paragraph($"Reporte de Asistencia de Docentes\nFecha: {dtpFecha.Value:dd/MM/yyyy}", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14));
                    titulo.Alignment = Element.ALIGN_CENTER;
                    tituloC.Alignment = Element.ALIGN_CENTER;
                    titulo.SpacingAfter = 20;
                    doc.Add(tituloC);
                    doc.Add(titulo);

                    // Obtener solo las columnas visibles para el reporte
                    var columnasVisibles = new List<DataGridViewColumn>();
                    foreach (DataGridViewColumn col in dgvAsistencia.Columns)
                    {
                        if (col.Name != "IdAsistencia" && col.Name != "IdDocente")
                        {
                            columnasVisibles.Add(col);
                        }
                    }

                    // Crear tabla con el número de columnas visibles
                    PdfPTable table = new PdfPTable(columnasVisibles.Count);
                    table.WidthPercentage = 100;

                    // Encabezados
                    foreach (DataGridViewColumn columna in columnasVisibles)
                    {
                        PdfPCell celdaEncabezado = new PdfPCell(new Phrase(columna.HeaderText, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10)));
                        celdaEncabezado.BackgroundColor = BaseColor.LIGHT_GRAY;
                        celdaEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(celdaEncabezado);
                    }

                    // Filas de datos
                    foreach (DataGridViewRow row in dgvAsistencia.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            foreach (DataGridViewColumn col in columnasVisibles)
                            {
                                var cellValue = row.Cells[col.Index].Value;
                                PdfPCell pdfCell;

                                // Columna Asistió: cambiar True/False a Sí/No y centrar
                                if (col.Name == "Asistio")
                                {
                                    string valor = Convert.ToBoolean(cellValue) ? "Sí" : "No";
                                    pdfCell = new PdfPCell(new Phrase(valor));
                                    pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                }
                                else
                                {
                                    pdfCell = new PdfPCell(new Phrase(cellValue?.ToString()));
                                    pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
                                }
                                table.AddCell(pdfCell);
                            }
                        }
                    }

                    // Agregar tabla al documento
                    doc.Add(table);
                    doc.Close();

                    MessageBox.Show("Reporte PDF generado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el reporte: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReporteExcel_Click(object sender, EventArgs e)
        {
            if (dgvAsistencia.Rows.Count > 0)
            {
                // Crear DataTable temporal para exportar sin IdAsistencia
                DataTable dtExport = new DataTable();

                // Agregar columnas
                foreach (DataGridViewColumn col in dgvAsistencia.Columns)
                {
                    if (col.Name != "IdAsistencia" && col.Name != "IdDocente") // No incluir IdAsistencia ni IdDocente
                        dtExport.Columns.Add(col.HeaderText);
                }

                // Agregar filas
                foreach (DataGridViewRow row in dgvAsistencia.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        List<object> cellValues = new List<object>();

                        foreach (DataGridViewColumn col in dgvAsistencia.Columns)
                        {
                            if (col.Name != "IdAsistencia" && col.Name != "IdDocente") // Ignorar esta columna
                            {
                                var value = row.Cells[col.Index].Value;
                                if (value is bool)
                                    value = (bool)value ? "Sí" : "No";
                                cellValues.Add(value);
                            }
                        }

                        dtExport.Rows.Add(cellValues.ToArray());
                    }
                }

                // Nombre del archivo según fecha seleccionada
                string fecha = dtpFecha.Value.ToString("ddMMyyyy");
                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = $"AsistenciaDocentes_{fecha}.xlsx";
                savefile.Filter = "Excel Files|*.xlsx";

                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XLWorkbook wb = new XLWorkbook();
                        wb.Worksheets.Add(dtExport, "AsistenciaDocentes");
                        wb.SaveAs(savefile.FileName);
                        MessageBox.Show("Reporte Excel generado correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Error al generar reporte Excel", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            else
            {
                MessageBox.Show("No existen datos para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
