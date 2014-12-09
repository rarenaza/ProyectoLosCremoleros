using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class OfertaSectorEmpresarial
    {
        public int IdOfertaSectorEmpresarial { get; set; }
        public int IdOferta { get; set; }
        public ListaValor SectorEmpresarial { get; set; }
        public bool ExperienciaExcluyente { get; set; }
        public int AniosTrabajados { get; set; }
        public ListaValor EstadoOfertaSectorEmpresarial { get; set; }
        public string CreadoPor { get; set; }
        public string ModificadoPor { get; set; }

        public OfertaSectorEmpresarial()
        {
            SectorEmpresarial = new ListaValor();
            EstadoOfertaSectorEmpresarial = new ListaValor();
        }

    }
}

