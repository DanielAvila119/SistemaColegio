using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaModelo;

namespace CapaDatos
{
    public class CD_BuscarAlumno
    {
        private string connectionString = "Data Source=.;Initial Catalog=BDCOLEGIO;Integrated Security=True;TrustServerCertificate=True";
        // Cambia por tu cadena de conexión

        // Método para obtener todos los alumnos
        public List<Alumno> ObtenerTodos()
        {
            List<Alumno> lista = new List<Alumno>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT IdAlumno, Codigo, Nombres, Apellidos FROM ALUMNO WHERE Activo = 1", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Alumno
                    {
                        IdAlumno = dr.GetInt32(0),
                        Codigo = dr.GetString(1),
                        Nombres = dr.GetString(2),
                        Apellidos = dr.GetString(3)
                    });
                }
            }

            return lista;
        }

        // Método para buscar por filtro
        public List<Alumno> Buscar(string campo, string valor)
        {
            List<Alumno> lista = new List<Alumno>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = $"SELECT IdAlumno, Codigo, Nombres, Apellidos FROM ALUMNO " +
                               $"WHERE {campo} LIKE @valor AND Activo = 1";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@valor", "%" + valor + "%");

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Alumno
                    {
                        IdAlumno = dr.GetInt32(0),
                        Codigo = dr.GetString(1),
                        Nombres = dr.GetString(2),
                        Apellidos = dr.GetString(3)
                    });
                }
            }

            return lista;
        }
    }
}
