using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class OfertaEstudio
    {
        public int IdOfertaEstudio{ get; set; }
        public int IdOferta { get; set; }
        public string CicloEstudio { get; set; }
        public string Estudio { get; set; }
        public string NivelConocimiento { get; set; }
        public string TipoDeEstudio { get; set; }
        public string EstadoDelEstudio { get; set; }
        public string EstadoOfertaEstudio { get; set; }
        public string CreadoPor { get; set; }
        public string ModificadoPor { get; set; }
    }
}
