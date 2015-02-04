using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo.Vistas.Alumno
{
  public   class AlertasCvAlumno
    {
        public int IdCV { get; set; }
        public string NombreCV { get; set; }
        public string NombrePlantilla { get; set; }
        public int PorcentajeCV { get; set; }
        public int Dia { get; set; }
        public int MesesSinTrabajo { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
