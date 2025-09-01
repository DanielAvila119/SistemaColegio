using CapaModelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaDatos
{
    public class CD_TrasladoAlumnos
    {
        public static bool RegistrarTraslado(TrasladoAlumno objTraslado)
        {
            bool respuesta = false;

            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    oConexion.Open();

                    // Consulta SQL INSERT en línea
                    string query = "INSERT INTO TRASLADO_ALUMNO (" +
                                    "NombreAlumno, GradoSeccion, NumeroPartida, TipoTraslado, " +
                                    "InstitutoOrigenDestino, FechaTraslado, Motivo, Observaciones) " +
                                    "VALUES (@NombreAlumno, @GradoSeccion, @NumeroPartida, @TipoTraslado, " +
                                    "@InstitutoOrigenDestino, @FechaTraslado, @Motivo, @Observaciones)";

                    SqlCommand cmd = new SqlCommand(query, oConexion);

                    // Añadimos los parámetros de forma segura para evitar inyecciones SQL
                    cmd.Parameters.AddWithValue("@NombreAlumno", objTraslado.NombreAlumno);
                    cmd.Parameters.AddWithValue("@GradoSeccion", objTraslado.GradoSeccion);
                    cmd.Parameters.AddWithValue("@NumeroPartida", objTraslado.NumeroPartida);
                    cmd.Parameters.AddWithValue("@TipoTraslado", objTraslado.TipoTraslado);
                    cmd.Parameters.AddWithValue("@InstitutoOrigenDestino", objTraslado.InstitutoOrigenDestino);
                    cmd.Parameters.AddWithValue("@FechaTraslado", objTraslado.FechaTraslado);
                    cmd.Parameters.AddWithValue("@Motivo", objTraslado.Motivo);
                    cmd.Parameters.AddWithValue("@Observaciones", objTraslado.Observaciones);

                    cmd.CommandType = CommandType.Text; // Indicamos que es un comando de texto, no un Stored Procedure
                    cmd.ExecuteNonQuery();

                    respuesta = true; // Si llega hasta aquí, la inserción fue exitosa
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    respuesta = false;
                    MessageBox.Show($"Error en la inserción: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return respuesta;
        }

        public static List<TrasladoAlumno> ListarTraslados()
        {
            List<TrasladoAlumno> lista = new List<TrasladoAlumno>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    oConexion.Open();
                    // Usamos el mismo Stored Procedure para la lista, ya que el error era en el registro
                    SqlCommand cmd = new SqlCommand("sp_ListarTrasladosAlumnos_Manual", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new TrasladoAlumno()
                            {
                                IdTraslado = Convert.ToInt32(dr["IdTraslado"]),
                                NombreAlumno = dr["NombreAlumno"].ToString(),
                                GradoSeccion = dr["GradoSeccion"].ToString(),
                                NumeroPartida = dr["NumeroPartida"].ToString(),
                                TipoTraslado = dr["TipoTraslado"].ToString(),
                                InstitutoOrigenDestino = dr["InstitutoOrigenDestino"].ToString(),
                                FechaTraslado = Convert.ToDateTime(dr["FechaTraslado"]),
                                Motivo = dr["Motivo"].ToString(),
                                Observaciones = dr["Observaciones"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"]),
                                FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    lista = new List<TrasladoAlumno>();
                    MessageBox.Show($"Error al listar traslados: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return lista;
        }
        public static bool EliminarTraslado(int idTraslado)
        {
            bool respuesta = false;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    oConexion.Open();
                    string query = "DELETE FROM TRASLADO_ALUMNO WHERE IdTraslado = @IdTraslado";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@IdTraslado", idTraslado);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    respuesta = true;
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    MessageBox.Show($"Error al eliminar el traslado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return respuesta;
        }
    }
}
