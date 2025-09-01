using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class AsistenciaAlumnos
    {
        public int IdAsistencia { get; set; }
        public int IdDocente { get; set; }
        public string NombreDocente { get; set; } // Nombres + Apellidos
        public DateTime Fecha { get; set; }
        public bool Asistio { get; set; }
        public string Justificacion { get; set; }
    }
}
