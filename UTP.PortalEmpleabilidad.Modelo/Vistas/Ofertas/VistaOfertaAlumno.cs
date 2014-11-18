using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo.Vistas.Ofertas
{
    public class VistaOfertaAlumno
    {

        public DateTime FechaPublicacion { get; set; }

        public string  Empresa { get; set; }

        public string CargoOfrecido { get; set; }
        
        public string TipoTrabajo { get; set; }

        public DateTime  Horario { get; set; }

        public decimal RemuneracionOfrecida { get; set; }

        public string  EstadoOferta { get; set; }

    }
}
