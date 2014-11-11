using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class Empresa
    {
        public int IdEmpresa { get; set; }
        public string Nombre { get; set; }
        public string RazonSocial { get; set; }
        public string Pais { get; set; }
        public string IdentificadorTributario { get; set; }
        public string DescripcionEmpresa { get; set; }
        public string PresentacionEmpresa { get; set; }
        public int AnoCreacion { get; set; }
        public string NumeroEmpleados { get; set; }
        public string EstadoEmpresa { get; set; }
        public string SectorEmpresarial { get; set; }
        public string SectorEmpresarial2 { get; set; }
        public string SectorEmpresarial3 { get; set; }
        public string CreadoPor { get; set; }

    }
}
