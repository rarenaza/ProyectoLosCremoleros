using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class OfertaInformacionAdicional
    {
        public int IdOfertaInformacionAdicional { get; set; }
        public int IdOferta { get; set; }

        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string TipoConocimientoIdListaValor { get; set; }
        public virtual ListaValor TipoConocimiento { get; set; }

        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string Conocimiento { get; set; }
        public string NivelConocimientoIdListaValor { get; set; }
        public virtual ListaValor NivelConocimiento { get; set; }
        public int AniosExperiencia { get; set; }
        public ListaValor EstadoOfertaInformacionAdicional { get; set; }
        public string CreadoPor { get; set; }
        public string ModificadoPor { get; set; }

        public OfertaInformacionAdicional()
        {
            TipoConocimiento = new ListaValor();
            NivelConocimiento = new ListaValor();
            EstadoOfertaInformacionAdicional = new ListaValor();
        }
    }
}
