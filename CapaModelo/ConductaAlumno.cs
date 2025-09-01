using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class ConductaAlumno
    {
            public int IdConducta { get; set; }
            public string NombreAlumno { get; set; }
            public string CursoSeccion { get; set; }
            public DateTime FechaConducta { get; set; }
            public string Tipo { get; set; } // "Mérito" o "Falta"
            public string Descripcion { get; set; }
            public int IdUsuario { get; set; }
            public bool Activo { get; set; }
            public DateTime FechaRegistro { get; set; }
    }

}

