using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class AlumnoPostulaciones
    {
        public int IdOferta { get; set; }
        public string CargoOfrecido { get; set; }
        public DateTime FechaPostulacion { get; set; }
        public int IdOfertaPostulante { get; set; }
    }
}
