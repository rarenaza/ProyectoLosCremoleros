using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo.Vistas.Empresa
{
    public class VistaPanelCabecera
    {
        public string EmpresaRazonSocial { get; set; }
        public string EmpresaIdentificadorTributario { get; set; }
        public string UsuarioNombre { get; set; }
        public string UsuarioApellido { get; set; }
        public string UsuarioTipoDocumento { get; set; }
        public string UsuarioNumeroDocumento { get; set; }
        public string UsuarioCorreoElectronico { get; set; }
        public string UsuarioTelefonoCelular { get; set; }
        public string LocacionNombre { get; set; }
        public string LocacionDireccion { get; set; }
        public string LocacionTelefonoFijo { get; set; }
        public int IdEmpresaUsuario { get; set; }

        //Otras propiedades.
    }
}
