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

        public void Actualizar(Empresa empresa)
        {
            //Se formatea los valores nulos.
            if (empresa.LinkVideo == null) empresa.LinkVideo = string.Empty;

            adEmpresa.Actualizar(empresa);
        }

        public Empresa ObtenerDatosEmpresaPorId(int idEmpresa)
        {
            Empresa empresa = new Empresa();

            DataSet dsResultado = adEmpresa.ObtenerDatosEmpresaPorId(idEmpresa);

            //Datos generales de la empresa.
            if (dsResultado.Tables.Count > 0)
            {
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    empresa.IdEmpresa = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["IdEmpresa"]);
                    empresa.NombreComercial = Convert.ToString(dsResultado.Tables[0].Rows[0]["NombreComercial"]);
                    empresa.RazonSocial = Convert.ToString(dsResultado.Tables[0].Rows[0]["RazonSocial"]);
                    empresa.Pais.Valor = Convert.ToString(dsResultado.Tables[0].Rows[0]["PaisDescripcion"]);
                    empresa.IdentificadorTributario = Convert.ToString(dsResultado.Tables[0].Rows[0]["IdentificadorTributario"]);
                    empresa.DescripcionEmpresa = Convert.ToString(dsResultado.Tables[0].Rows[0]["DescripcionEmpresa"]);
                    empresa.LinkVideo = Convert.ToString(dsResultado.Tables[0].Rows[0]["LinkVideo"]);
                    empresa.AnoCreacion = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["AnoCreacion"]);
                    empresa.NumeroEmpleados.Valor = Convert.ToString(dsResultado.Tables[0].Rows[0]["NumeroEmpleadosDescripcion"]);
                    empresa.EstadoEmpresa.Valor = Convert.ToString(dsResultado.Tables[0].Rows[0]["EstadoEmpresaDescripcion"]);
                    empresa.SectorEmpresarial.Valor = Convert.ToString(dsResultado.Tables[0].Rows[0]["SectorEmpresarialDescripcion"]);
                    empresa.SectorEmpresarial2.Valor = Convert.ToString(dsResultado.Tables[0].Rows[0]["SectorEmpresarial2Descripcion"]);
                    empresa.SectorEmpresarial3.Valor = Convert.ToString(dsResultado.Tables[0].Rows[0]["SectorEmpresarial3Descripcion"]);

                    empresa.PaisIdListaValor = Convert.ToString(dsResultado.Tables[0].Rows[0]["Pais"]);
                    empresa.NumeroEmpleadosIdListaValor = Convert.ToString(dsResultado.Tables[0].Rows[0]["NumeroEmpleados"]);
                    empresa.SectorEmpresarial1IdListaValor = Convert.ToString(dsResultado.Tables[0].Rows[0]["SectorEmpresarial"]);
                    empresa.SectorEmpresarial2IdListaValor = Convert.ToString(dsResultado.Tables[0].Rows[0]["SectorEmpresarial2"]);
                    empresa.SectorEmpresarial3IdListaValor = Convert.ToString(dsResultado.Tables[0].Rows[0]["SectorEmpresarial3"]);
                }
            }

            //Locaciones
            foreach (DataRow locacionBD in dsResultado.Tables[1].Rows)
            {
                EmpresaLocacion empresaLocacion = new EmpresaLocacion();
                empresaLocacion.IdEmpresaLocacion = Convert.ToInt32(locacionBD["IdEmpresaLocacion"]);
                empresaLocacion.IdEmpresa = Convert.ToInt32(locacionBD["IdEmpresa"]);
                empresaLocacion.TipoLocacion.Valor = Convert.ToString(locacionBD["TipoLocacionDescripcion"]);
                empresaLocacion.NombreLocacion = Convert.ToString(locacionBD["NombreLocacion"]);
                empresaLocacion.CorreoElectronico = Convert.ToString(locacionBD["CorreoElectronico"]);
                empresaLocacion.TelefonoFijo = Convert.ToString(locacionBD["TelefonoFijo"]);
                empresaLocacion.Direccion = Convert.ToString(locacionBD["Direccion"]);                
                empresaLocacion.DireccionDistrito = Convert.ToString(locacionBD["DireccionDistrito"]);
                empresaLocacion.DireccionCiudad = Convert.ToString(locacionBD["DireccionCiudad"]);
                empresaLocacion.DireccionRegion = Convert.ToString(locacionBD["DireccionRegion"]);
                empresaLocacion.EstadoLocacion.Valor = Convert.ToString(locacionBD["EstadoLocacionDescripcion"]);

                empresa.Locaciones.Add(empresaLocacion);
            }

            //Usuarios>
            foreach (DataRow usuarioBD in dsResultado.Tables[2].Rows)
            {
                EmpresaUsuario empresaUsuario = new EmpresaUsuario();
                empresaUsuario.IdEmpresaUsuario = Convert.ToInt32(usuarioBD["IdEmpresaUsuario"]);
                empresaUsuario.Empresa.IdEmpresa = Convert.ToInt32(usuarioBD["IdEmpresa"]); ;
                empresaUsuario.Usuario.NombreUsuario = Convert.ToString(usuarioBD["Usuario"]);
                empresaUsuario.Usuario.Rol.Valor = Convert.ToString(usuarioBD["UsuarioRolDescripcion"]);
                empresaUsuario.Usuario.EstadoUsuario.Valor = Convert.ToString(usuarioBD["UsuarioEstadoDescripcion"]);
                empresaUsuario.Nombres = Convert.ToString(usuarioBD["Nombres"]);
                empresaUsuario.Apellidos = Convert.ToString(usuarioBD["Apellidos"]);
                empresaUsuario.TipoDocumento.Valor = Convert.ToString(usuarioBD["TipoDocumentoDescripcion"]);
                empresaUsuario.NumeroDocumento = Convert.ToString(usuarioBD["NumeroDocumento"]);
                empresaUsuario.Sexo.Valor = Convert.ToString(usuarioBD["SexoDescripcion"]);
                empresaUsuario.CorreoElectronico = Convert.ToString(usuarioBD["CorreoElectronico"]);
                empresaUsuario.TelefonoFijo = Convert.ToString(usuarioBD["TelefonoFijo"]);
                empresaUsuario.TelefonoCelular = Convert.ToString(usuarioBD["TelefonoCelular"]);
                empresaUsuario.TelefonoAnexo = Convert.ToString(usuarioBD["TelefonoAnexo"]);

                empresa.Usuarios.Add(empresaUsuario);
            }
            return empresa;
        }        
    }
}