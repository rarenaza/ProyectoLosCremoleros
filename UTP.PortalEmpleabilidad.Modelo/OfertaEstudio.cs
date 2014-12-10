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
        public string TipoDeEstudioIdListaValor { get; set; }
        public ListaValor TipoDeEstudio { get; set; }
        public string EstadoDelEstudioIdListaValor { get; set; }
        public ListaValor EstadoDelEstudio { get; set; }
        public ListaValor EstadoOfertaEstudio { get; set; }
        public string CreadoPor { get; set; }
        public string ModificadoPor { get; set; }

        public OfertaEstudio()
        {
            TipoDeEstudio = new ListaValor();
            EstadoDelEstudio = new ListaValor();
            EstadoOfertaEstudio = new ListaValor();
        }
    }
}
