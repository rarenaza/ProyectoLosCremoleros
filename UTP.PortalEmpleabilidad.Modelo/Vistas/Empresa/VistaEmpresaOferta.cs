using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo.Vistas.Empresa
{
    public class VistaEmpresaOferta
    {
        public int IdOferta { get; set; }
        public int IdEmpresa { get; set; }
        public string FechaPublicacion { get; set; }
        public string CargoOfrecido { get; set; }
        public int CantidadPostulantes { get; set; }
        public string NombreEstado { get; set; }
        public string NombreEstadoOfertaDescripcion { get; set; }
    }
}
