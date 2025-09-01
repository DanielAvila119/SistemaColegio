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
    public class CD_AsistenciaAlumnos
    {
        public List<AsistenciaAlumnos> Listar()
        {
            List<AsistenciaAlumnos> lista = new List<AsistenciaAlumnos>();

            using (SqlConnection con = new SqlConnection(Conexion.CN))
            {
                string query = @"
                    SELECT a.IdAsistencia, a.IdAlumno, 
                           (d.Nombres + ' ' + d.Apellidos) AS NombreAlumno,
                           a.Fecha, a.Asistio, a.Justificacion
                    FROM ASISTENCIA_ALUMNO a
                    INNER JOIN ALUMNO d ON a.IdAlumno = d.IdAlumno
                    ORDER BY a.Fecha DESC";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new AsistenciaAlumnos()
                        {
                            IdAsistencia = Convert.ToInt32(dr["IdAsistencia"]),
                            IdDocente = Convert.ToInt32(dr["IdAlumno"]),
                            NombreDocente = dr["NombreAlumno"].ToString(),
                            Fecha = Convert.ToDateTime(dr["Fecha"]),
                            Asistio = Convert.ToBoolean(dr["Asistio"]),
                            Justificacion = dr["Justificacion"] != DBNull.Value ? dr["Justificacion"].ToString() : ""
                        });
                    }
                }
            }
            return lista;
        }

        public bool Registrar(AsistenciaAlumnos obj)
        {
            using (SqlConnection con = new SqlConnection(Conexion.CN))
            {
                string query = "INSERT INTO ASISTENCIA_ALUMNO (IdAlumno, Fecha, Asistio, Justificacion) VALUES (@IdAlumno, @Fecha, @Asistio, @Justificacion)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdAlumno", obj.IdDocente);
                cmd.Parameters.AddWithValue("@Fecha", obj.Fecha);
                cmd.Parameters.AddWithValue("@Asistio", obj.Asistio);
                cmd.Parameters.AddWithValue("@Justificacion", (object)obj.Justificacion ?? DBNull.Value);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Editar(AsistenciaAlumnos obj)
        {
            using (SqlConnection con = new SqlConnection(Conexion.CN))
            {
                string query = "UPDATE ASISTENCIA_ALUMNO SET IdAlumno=@IdAlumno, Fecha=@Fecha, Asistio=@Asistio, Justificacion=@Justificacion WHERE IdAsistencia=@IdAsistencia";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdAsistencia", obj.IdAsistencia);
                cmd.Parameters.AddWithValue("@IdAlumno", obj.IdDocente);
                cmd.Parameters.AddWithValue("@Fecha", obj.Fecha);
                cmd.Parameters.AddWithValue("@Asistio", obj.Asistio);
                cmd.Parameters.AddWithValue("@Justificacion", (object)obj.Justificacion ?? DBNull.Value);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Eliminar(int idAsistencia)
        {
            using (SqlConnection con = new SqlConnection(Conexion.CN))
            {
                string query = "DELETE FROM ASISTENCIA_ALUMNO WHERE IdAsistencia=@IdAsistencia";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdAsistencia", idAsistencia);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
