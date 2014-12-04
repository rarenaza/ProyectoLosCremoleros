using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTP.PortalEmpleabilidad.Logica
{
    public class LNEmpresaUsuario
    {
        ADEmpresaUsuario adEmpresaUsuario = new ADEmpresaUsuario();

        public void Insertar(EmpresaUsuario empresaUsuario, string usuarioCreacion)
        {
            adEmpresaUsuario.Insertar(empresaUsuario, usuarioCreacion);        
        }
    }
}
