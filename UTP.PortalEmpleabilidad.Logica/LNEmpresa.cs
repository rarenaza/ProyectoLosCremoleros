using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Empresa;

namespace UTP.PortalEmpleabilidad.Logica
{
    public class LNEmpresa
    {
        ADEmpresa adEmpresa = new ADEmpresa();

        public VistaPanelCabecera ObtenerPanelCabecera(string usuarioEmpresa)
        {
            VistaPanelCabecera panel = new VistaPanelCabecera();

            //Se llenan los datos del alumno.
            DataTable dtResultado = adEmpresa.ObtenerCabeceraPorCodigoUsuario(usuarioEmpresa);

            if (dtResultado.Rows.Count > 0)
            {
                //Datos de la empresa
                panel.EmpresaRazonSocial = dtResultado.Rows[0]["EmpresaRazonSocial"].ToString();
                panel.EmpresaIdentificadorTributario = dtResultado.Rows[0]["EmpresaIdentificadorTributario"].ToString();

                //Datos del usuario
                panel.UsuarioNombre = dtResultado.Rows[0]["UsuarioNombre"].ToString();
                panel.UsuarioApellido = dtResultado.Rows[0]["UsuarioApellido"].ToString();
                panel.UsuarioTipoDocumento = dtResultado.Rows[0]["UsuarioTipoDocumento"].ToString();
                panel.UsuarioNumeroDocumento = dtResultado.Rows[0]["UsuarioNumeroDocumento"].ToString();
                panel.UsuarioCorreoElectronico = dtResultado.Rows[0]["UsuarioCorreoElectronico"].ToString();
                panel.UsuarioTelefonoCelular = dtResultado.Rows[0]["UsuarioTelefonoCelular"].ToString();

                //Datos de la locación
                panel.LocacionNombre = dtResultado.Rows[0]["LocacionNombre"].ToString();
                panel.LocacionDireccion = dtResultado.Rows[0]["LocacionDireccion"].ToString();
                panel.LocacionTelefonoFijo = dtResultado.Rows[0]["LocacionTelefonoFijo"].ToString();

            }

            else
            {
                panel.EmpresaRazonSocial = "Sin datos DEMO";
                panel.EmpresaIdentificadorTributario = "Sin Datos DEMO";

            }

            return panel;
        }

        public void Insertar(Empresa empresa)
        {
            adEmpresa.Insertar(empresa);
        }
    }
}
