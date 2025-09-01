using CapaModelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Alumno
    {
        public static List<Alumno> Listar()
        {
            List<Alumno> rptListaAlumno = new List<Alumno>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ListarAlumno", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        rptListaAlumno.Add(new Alumno()
                        {
                            IdAlumno = Convert.ToInt32(dr["IdAlumno"].ToString()),
                            Codigo = dr["Codigo"].ToString(),
                            Nombres = dr["Nombres"].ToString(),
                            Apellidos = dr["Apellidos"].ToString(),
                            DocumentoPartida = dr["DocumentoPartida"].ToString(),
                            FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"].ToString()),
                            Sexo = dr["Sexo"].ToString(),
                            Ciudad = dr["Ciudad"].ToString(),
                            Direccion = dr["Direccion"].ToString(),
                            Activo = Convert.ToBoolean(dr["Activo"])

                        });
                    }
                    dr.Close();

                    return rptListaAlumno;

                }
                catch (Exception ex)
                {
                    rptListaAlumno = null;
                    return rptListaAlumno;
                }
            }
        }

        public static DataTable Reporte(string Nombres, string Apellidos, string Codigo, string DocumentoPartida)
        {
            List<Alumno> rptListaAlumno = new List<Alumno>();
            DataTable dt = new DataTable();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlDataAdapter da = new SqlDataAdapter("usp_ReporteAlumno", oConexion);
                da.SelectCommand.Parameters.AddWithValue("Nombres", Nombres);
                da.SelectCommand.Parameters.AddWithValue("Apellidos", Apellidos);
                da.SelectCommand.Parameters.AddWithValue("Codigo", Codigo);
                da.SelectCommand.Parameters.AddWithValue("DocumentoPartida", DocumentoPartida);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    da.Fill(dt);
                    return dt;
                }
                catch (Exception ex)
                {
                    return dt;
                }
            }
        }


        public static bool Registrar(Alumno oAlumno)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_RegistrarAlumno", oConexion);
                    cmd.Parameters.AddWithValue("Nombres", oAlumno.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", oAlumno.Apellidos);
                    cmd.Parameters.AddWithValue("DocumentoPartida", oAlumno.DocumentoPartida);
                    cmd.Parameters.AddWithValue("FechaNacimiento", oAlumno.FechaNacimiento);
                    cmd.Parameters.AddWithValue("Sexo", oAlumno.Sexo);
                    cmd.Parameters.AddWithValue("Ciudad", oAlumno.Ciudad);
                    cmd.Parameters.AddWithValue("Direccion", oAlumno.Direccion);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }


        public static bool Editar(Alumno oAlumno)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_EditarAlumno", oConexion);
                    cmd.Parameters.AddWithValue("IdAlumno", oAlumno.IdAlumno);
                    cmd.Parameters.AddWithValue("Codigo", oAlumno.Codigo);
                    cmd.Parameters.AddWithValue("Nombres", oAlumno.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", oAlumno.Apellidos);
                    cmd.Parameters.AddWithValue("DocumentoPartida", oAlumno.DocumentoPartida);
                    cmd.Parameters.AddWithValue("FechaNacimiento", oAlumno.FechaNacimiento);
                    cmd.Parameters.AddWithValue("Sexo", oAlumno.Sexo);
                    cmd.Parameters.AddWithValue("Ciudad", oAlumno.Ciudad);
                    cmd.Parameters.AddWithValue("Direccion", oAlumno.Direccion);
                    cmd.Parameters.AddWithValue("Activo", oAlumno.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }

        public static bool Eliminar(int idalumno)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_EliminarAlumno", oConexion);
                    cmd.Parameters.AddWithValue("IdAlumno", idalumno);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }
        //Para el formulario frmBuscarAlumno
        public static DataTable BuscarAlumnos(string filtro, string valor)
        {
            DataTable dt = new DataTable();

            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                // Armamos un query básico (puedes cambiarlo a SP si quieres)
                string query = "SELECT Codigo, Nombres, Apellidos FROM ALUMNO WHERE 1=1";

                if (!string.IsNullOrEmpty(valor))
                {
                    if (filtro == "Codigo")
                        query += " AND Codigo LIKE @valor";
                    else if (filtro == "Nombre")
                        query += " AND Nombres LIKE @valor";
                    else if (filtro == "Apellido")
                        query += " AND Apellidos LIKE @valor";
                }

                using (SqlCommand cmd = new SqlCommand(query, oConexion))
                {
                    cmd.Parameters.AddWithValue("@valor", "%" + valor + "%");

                    try
                    {
                        oConexion.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                    catch
                    {
                        dt = new DataTable(); // Si hay error, devolvemos vacío
                    }
                }
            }

            return dt;
        }


    }
}
