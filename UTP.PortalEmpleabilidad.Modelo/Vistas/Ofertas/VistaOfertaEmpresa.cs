using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo.Vistas.Ofertas
{
    public class VistaOfertaEmpresa
    {
        public int IdOferta { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public string Cargo { get; set; }
        public int CVPendientesRevision { get; set; }
        public int CVTotales { get; set; }
        public string FaseActual { get; set; }
        public string EstadoOferta { get; set; }
        public int MensajesNoLeidos { get; set; }
        public int MensajesTotales { get; set; }
    }
}
