using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class OfertaFase
    {
        public int IdOfertaFase { get; set; }
        public int IdOferta { get; set; }
        public string IdListaValor { get; set; }
        public Boolean Incluir { get; set; }
        public string FaseOferta { get; set; }
        public string CreadoPor { get; set; }
        public string ModificadoPor { get; set; }
    }
}
