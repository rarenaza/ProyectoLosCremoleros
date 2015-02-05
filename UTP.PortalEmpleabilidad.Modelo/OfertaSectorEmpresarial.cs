using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class OfertaSectorEmpresarial
    {
        public int IdOfertaSectorEmpresarial { get; set; }
        public int IdOferta { get; set; }

        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string SectorEmpresarialIdListaValor { get; set; }
        public ListaValor SectorEmpresarial { get; set; }
        public bool ExperienciaExcluyente { get; set; }
        public int? AniosTrabajados { get; set; }
        
        public ListaValor EstadoOfertaSectorEmpresarial { get; set; }
        public string CreadoPor { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }

        public OfertaSectorEmpresarial()
        {
            SectorEmpresarial = new ListaValor();
            EstadoOfertaSectorEmpresarial = new ListaValor();
        }

    }
}

