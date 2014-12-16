 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo.Vistas.Ofertas
{
  public   class VistaPostulacionAlumno
    {
        public DateTime  FechaPublicacion { get; set; }
        public DateTime FechaPostulacion { get; set; }
        public string Empresa { get; set; }
        public string CargoOfrecido { get; set; }
        public string TipoTrabajo { get; set; }
        public string Horario { get; set; }
        public decimal  RemuneracionOfrecida { get; set; }
        public string  EstadoOferta { get; set; }
        public int IdAlumno { get; set; }
        public string PalabraClave { get; set; }
        public int IdOferta { get; set; }
        public int Mensajes { get; set; }
        public int IdEmpresa { get; set; }
    }
}
