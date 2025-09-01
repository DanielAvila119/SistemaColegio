using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class TrasladoAlumno
    {
        public int IdTraslado { get; set; }
        public string NombreAlumno { get; set; }
        public string GradoSeccion { get; set; }
        public string NumeroPartida { get; set; }
        public string TipoTraslado { get; set; }
        public string InstitutoOrigenDestino { get; set; }
        public DateTime FechaTraslado { get; set; }
        public string Motivo { get; set; }
        public string Observaciones { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
