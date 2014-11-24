using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class OfertaInformacionAdicional
    {
        public int IdOfertaInformacionAdicional { get; set; }
        public int IdOferta { get; set; }
        public string TipoConocimiento { get; set; }
        public string Conocimiento { get; set; }
        public string NivelConocimiento { get; set; }
        public int AniosExperiencia { get; set; }
        public string EstadoOfertaInformacionAdicional { get; set; }
        public string CreadoPor { get; set; }
        public string ModificadoPor { get; set; }
    }
}
