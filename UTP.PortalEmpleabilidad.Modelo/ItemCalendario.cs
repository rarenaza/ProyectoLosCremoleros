using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{

    public class ItemCalendario
    {
        public DateTime FechaHoraDesde { get; set; }
        public DateTime FechaHoraHasta { get; set; }
        public string Asunto { get; set; }
        public string Ubicacion { get; set; }
        public string Contenido { get; set; }

    }

}
