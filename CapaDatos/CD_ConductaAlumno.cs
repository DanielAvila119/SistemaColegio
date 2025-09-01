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
    public class CD_ConductaAlumno
    {
        private string conexion = "Data Source=.;Initial Catalog=BDCOLEGIO;Integrated Security=True;TrustServerCertificate=True";

        public List<ConductaAlumno> Listar()
        {
            List<ConductaAlumno> lista = new List<ConductaAlumno>();

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                string query = @"SELECT IdConducta, NombreAlumno, CursoSeccion, FechaConducta, Tipo, Descripcion, IdUsuario, Activo, FechaRegistro
                                 FROM CONDUCTA_ALUMNO";

                SqlCommand cmd = new SqlCommand(query, cn);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new ConductaAlumno
                    {
                        IdConducta = Convert.ToInt32(dr["IdConducta"]),
                        NombreAlumno = dr["NombreAlumno"].ToString(),
                        CursoSeccion = dr["CursoSeccion"].ToString(),
                        FechaConducta = Convert.ToDateTime(dr["FechaConducta"]),
                        Tipo = dr["Tipo"].ToString(),
                        Descripcion = dr["Descripcion"].ToString(),
                        IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                        Activo = Convert.ToBoolean(dr["Activo"]),
                        FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"])
                    });
                }
            }
            return lista;
        }

        public bool Registrar(ConductaAlumno obj)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                string query = @"INSERT INTO CONDUCTA_ALUMNO 
                                (NombreAlumno, CursoSeccion, FechaConducta, Tipo, Descripcion, IdUsuario, Activo)
                                 VALUES (@NombreAlumno, @CursoSeccion, @FechaConducta, @Tipo, @Descripcion, @IdUsuario, 1)";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@NombreAlumno", obj.NombreAlumno);
                cmd.Parameters.AddWithValue("@CursoSeccion", obj.CursoSeccion);
                cmd.Parameters.AddWithValue("@FechaConducta", obj.FechaConducta);
                cmd.Parameters.AddWithValue("@Tipo", obj.Tipo);
                cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                cmd.Parameters.AddWithValue("@IdUsuario", obj.IdUsuario);

                cn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Editar(ConductaAlumno obj)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                string query = @"UPDATE CONDUCTA_ALUMNO
                                 SET NombreAlumno = @NombreAlumno,
                                     CursoSeccion = @CursoSeccion,
                                     FechaConducta = @FechaConducta,
                                     Tipo = @Tipo,
                                     Descripcion = @Descripcion
                                 WHERE IdConducta = @IdConducta";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@NombreAlumno", obj.NombreAlumno);
                cmd.Parameters.AddWithValue("@CursoSeccion", obj.CursoSeccion);
                cmd.Parameters.AddWithValue("@FechaConducta", obj.FechaConducta);
                cmd.Parameters.AddWithValue("@Tipo", obj.Tipo);
                cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                cmd.Parameters.AddWithValue("@IdConducta", obj.IdConducta);

                cn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Eliminar(int idConducta)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                string query = "DELETE FROM CONDUCTA_ALUMNO WHERE IdConducta = @IdConducta";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@IdConducta", idConducta);

                cn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public DataTable FiltrarConductas(string campo, string valor)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.CN))
            {
                string query = $"SELECT * FROM CONDUCTA_ALUMNO WHERE {campo} LIKE @valor";
                SqlDataAdapter da = new SqlDataAdapter(query, cn);
                da.SelectCommand.Parameters.AddWithValue("@valor", "%" + valor + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable FiltrarConductasPorTipo(string tipo)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.CN))
            {
                string query = "SELECT * FROM CONDUCTA_ALUMNO WHERE Tipo = @tipo";
                SqlDataAdapter da = new SqlDataAdapter(query, cn);
                da.SelectCommand.Parameters.AddWithValue("@tipo", tipo);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public DataTable FiltrarConductasPorFecha(DateTime fecha)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.CN))
            {
                string query = "SELECT * FROM CONDUCTA_ALUMNO WHERE FechaConducta = @fecha";
                SqlDataAdapter da = new SqlDataAdapter(query, cn);
                da.SelectCommand.Parameters.AddWithValue("@fecha", fecha.Date);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }


    }
}
