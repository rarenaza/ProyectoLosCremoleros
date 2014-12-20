using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo.Vistas.Alumno
{
   public  class VistaUTPListaAlumno
    {
        public int id { get; set; }
        public string FechaRegistro { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Carrera { get; set; }
        public string Ciclo { get; set; }
        public int idAlumno { get; set; }
    }
}
