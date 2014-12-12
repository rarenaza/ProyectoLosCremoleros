using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class OfertaEstudio
    {
        public int IdOfertaEstudio{ get; set; }
        public int IdOferta { get; set; }
        public int CicloEstudio { get; set; }
        
        [Required (ErrorMessage=Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string Estudio { get; set; }

        [Obsolete]
        public string NivelConocimiento { get; set; }
        
        [Required (ErrorMessage=Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string TipoDeEstudioIdListaValor { get; set; }
        public ListaValor TipoDeEstudio { get; set; }

        [Required (ErrorMessage=Constantes.MSJ_CAMPO_OBLIGATORIO)]
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
