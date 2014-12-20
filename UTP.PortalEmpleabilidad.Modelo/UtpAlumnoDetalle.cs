using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
   public  class UtpAlumnoDetalle
    {
        public int id { get; set; }
        public int IdAlumno { get; set; }
        public string FechaRegistro { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Carrera{ get; set; }
        public string CicloEquivalente { get; set; }

        public string Direccion { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string CorreoElectronico { get; set; }
       
    }
}
