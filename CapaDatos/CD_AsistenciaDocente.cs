using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaModelo;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_AsistenciaDocente
    {
        public List<AsistenciaDocente> Listar()
        {
            List<AsistenciaDocente> lista = new List<AsistenciaDocente>();

            using (SqlConnection con = new SqlConnection(Conexion.CN))
            {
                string query = @"
                    SELECT a.IdAsistencia, a.IdDocente, 
                           (d.Nombres + ' ' + d.Apellidos) AS NombreDocente,
                           a.Fecha, a.Asistio, a.Justificacion
                    FROM ASISTENCIA_DOCENTE a
                    INNER JOIN DOCENTE d ON a.IdDocente = d.IdDocente
                    ORDER BY a.Fecha DESC";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new AsistenciaDocente()
                        {
                            IdAsistencia = Convert.ToInt32(dr["IdAsistencia"]),
                            IdDocente = Convert.ToInt32(dr["IdDocente"]),
                            NombreDocente = dr["NombreDocente"].ToString(),
                            Fecha = Convert.ToDateTime(dr["Fecha"]),
                            Asistio = Convert.ToBoolean(dr["Asistio"]),
                            Justificacion = dr["Justificacion"] != DBNull.Value ? dr["Justificacion"].ToString() : ""
                        });
                    }
                }
            }
            return lista;
        }

        public bool Registrar(AsistenciaDocente obj)
        {
            using (SqlConnection con = new SqlConnection(Conexion.CN))
            {
                string query = "INSERT INTO ASISTENCIA_DOCENTE (IdDocente, Fecha, Asistio, Justificacion) VALUES (@IdDocente, @Fecha, @Asistio, @Justificacion)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdDocente", obj.IdDocente);
                cmd.Parameters.AddWithValue("@Fecha", obj.Fecha);
                cmd.Parameters.AddWithValue("@Asistio", obj.Asistio);
                cmd.Parameters.AddWithValue("@Justificacion", (object)obj.Justificacion ?? DBNull.Value);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Editar(AsistenciaDocente obj)
        {
            using (SqlConnection con = new SqlConnection(Conexion.CN))
            {
                string query = "UPDATE ASISTENCIA_DOCENTE SET IdDocente=@IdDocente, Fecha=@Fecha, Asistio=@Asistio, Justificacion=@Justificacion WHERE IdAsistencia=@IdAsistencia";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdAsistencia", obj.IdAsistencia);
                cmd.Parameters.AddWithValue("@IdDocente", obj.IdDocente);
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
                string query = "DELETE FROM ASISTENCIA_DOCENTE WHERE IdAsistencia=@IdAsistencia";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdAsistencia", idAsistencia);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
