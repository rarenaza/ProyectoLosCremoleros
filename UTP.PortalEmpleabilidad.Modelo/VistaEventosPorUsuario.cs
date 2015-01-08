using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class VistaEventosPorUsuario
    {
        
        public IList<Evento> usuarioeventoposible { get; set; }
        public IList<Evento> usuarioeventoactivo { get; set; }
        public IList<Evento> usuarioeventopasado { get; set; }
        
    }
}
