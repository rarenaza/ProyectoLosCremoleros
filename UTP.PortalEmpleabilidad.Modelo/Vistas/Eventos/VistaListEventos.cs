using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo.Vistas.Eventos
{
    public class VistaListEventos
    {
        public int id { get; set; }
        public string Evento { get; set; }
        public string Lugar { get; set; }
        public string Publico { get; set; }
        public string Estado { get; set; }
        public string Asistencia { get; set; }

        public string PalabraClave { get; set; }

        public List<Evento> ListaBusqueda { get; set; }
       
    }
}
