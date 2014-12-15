using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo.Vistas.Ofertas
{
  public   class VistaEmpresListarOfertas
    {
        public string id { get; set; }
        public string NombreComercial { get; set; }
        public string RazonSocial { get; set; }
        public string RUC { get; set; }
        public string Estado{ get; set; }
        public string SectorEmpresarial { get; set; }
        public string Ofertas { get; set; }

       
        public List<ListaValor> ListaEstado { get; set; }
        public List<ListaValor> Listasector { get; set; }

        public List<EmpresaListaEmpresa> ListaBusqueda { get; set; }
       public string PalabraClave { get; set; }
        public string IdEstadoEmpresa { get; set; }

        public string IdSector { get; set; }



    }
}
