using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class EmpresaLocacion
    {
        public int IdEmpresaLocacion { get; set; }
        public int IdEmpresa { get; set; }
        public string TipoLocacion { get; set; }
        public string NombreLocacion { get; set; }
        public string CorreoElectronico { get; set; }
        public string TelefonoFijo { get; set; }
        public string DireccionLinea1 { get; set; }
        public string DireccionLinea2 { get; set; }
        public string DireccionLinea3 { get; set; }
        public string DireccionDistrito { get; set; }
        public string DireccionCiudad { get; set; }
        public string DireccionRegion { get; set; }
        public string EstadoLocacion { get; set; }

    }
}
