using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
  public class EmpresaListaEmpresa
    {
        public int IdEmpresa { get; set; }
        public string id { get; set; }
        public string NombreComercial { get; set; }
    
        public string RazonSocial { get; set; }
        public string RUC { get; set; }
        public string Estado { get; set; }
        public string SectorEmpresarial { get; set; }
        public string Ofertas { get; set; }
        public string EjecutivoUTP { get; set; }
        public string Clasificacion { get; set; }
        public string IdEstadoEmpresa { get; set; }
       public string IdSector { get; set; }

      public string  NivelDeRelacion { get; set; }
      public string  FacultadPrincipal { get; set; }
       public string  Comentarios { get; set; }


    }
}
