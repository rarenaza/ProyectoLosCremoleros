using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Empresa;

namespace UTP.PortalEmpleabilidad.Logica
{
    public class LNEmpresaUsuario
    {
        ADEmpresaUsuario adEmpresaUsuario = new ADEmpresaUsuario();

        public void Insertar(EmpresaUsuario empresaUsuario)
        {
            if (empresaUsuario.TipoDocumentoIdListaValor == null) empresaUsuario.TipoDocumentoIdListaValor = "";
            if (empresaUsuario.NumeroDocumento == null) empresaUsuario.NumeroDocumento = "";
            if (empresaUsuario.SexoIdListaValor == null) empresaUsuario.SexoIdListaValor = "";
            if (empresaUsuario.TelefonoFijo == null) empresaUsuario.TelefonoFijo = "";
            if (empresaUsuario.TelefonoAnexo == null) empresaUsuario.TelefonoAnexo = "";
            if (empresaUsuario.TelefonoCelular == null) empresaUsuario.TelefonoCelular = "";

            adEmpresaUsuario.Insertar(empresaUsuario);        
        }

        public void ActualizarContrasena(string contrasena, string user)
        {
            adEmpresaUsuario.ActualizarContrasena(contrasena,user);
        }
        public void Actualizar(EmpresaUsuario empresaUsuario)
        {
            String spassword = null;
            if (empresaUsuario.TipoDocumentoIdListaValor == null) empresaUsuario.TipoDocumentoIdListaValor = "";
            if (empresaUsuario.NumeroDocumento == null) empresaUsuario.NumeroDocumento = "";
            if (empresaUsuario.SexoIdListaValor == null) empresaUsuario.SexoIdListaValor = "";
            if (empresaUsuario.TelefonoFijo == null) empresaUsuario.TelefonoFijo = "";
            if (empresaUsuario.TelefonoAnexo == null) empresaUsuario.TelefonoAnexo = "";
            if (empresaUsuario.TelefonoCelular == null) empresaUsuario.TelefonoCelular = "";

            LNAutenticarUsuario ln = new LNAutenticarUsuario();
            DataSet dsResultado = ln.Autenticar_Usuario(empresaUsuario.NombreUsuario);
            string contrasenaDecodificada = Convert.ToString(dsResultado.Tables[0].Rows[0]["Contrasena"]);
            if (empresaUsuario.Contrasena != contrasenaDecodificada)
            {
                byte[] bytes = Encoding.Default.GetBytes(empresaUsuario.Contrasena);
                SHA1 sha = new SHA1CryptoServiceProvider();
                byte[] password = sha.ComputeHash(bytes);
                spassword = Encoding.Default.GetString(password);
            }
            adEmpresaUsuario.Actualizar(empresaUsuario, spassword);
        }

        public EmpresaUsuario ObtenerPorIdEmpresaUsuario(int idEmpresaUsuario)
        {
            EmpresaUsuario empresaUsuario = new EmpresaUsuario();

            DataTable dtResultado = this.adEmpresaUsuario.ObtenerPorIdEmpresaUsuario(idEmpresaUsuario);
            
            //Usuarios>
            foreach (DataRow usuarioBD in dtResultado.Rows)
            {
                empresaUsuario = new EmpresaUsuario();
                empresaUsuario.IdEmpresaUsuario = Convert.ToInt32(usuarioBD["IdEmpresaUsuario"]);
                empresaUsuario.Empresa.IdEmpresa = Convert.ToInt32(usuarioBD["IdEmpresa"]);
                empresaUsuario.NombreUsuario = Convert.ToString(usuarioBD["Usuario"]);
                empresaUsuario.Usuario.NombreUsuario = Convert.ToString(usuarioBD["Usuario"]);
                empresaUsuario.RolIdListaValor = Convert.ToString(usuarioBD["Rol"]);
                empresaUsuario.Usuario.Rol.Valor = Convert.ToString(usuarioBD["UsuarioRolDescripcion"]);
                empresaUsuario.Usuario.EstadoUsuario.Valor = Convert.ToString(usuarioBD["UsuarioEstadoDescripcion"]);
                empresaUsuario.Nombres = Convert.ToString(usuarioBD["Nombres"]);
                empresaUsuario.Apellidos = Convert.ToString(usuarioBD["Apellidos"]);
                empresaUsuario.IdEmpresaLocacion = Convert.ToInt32(usuarioBD["IdEmpresaLocacion"]);
                empresaUsuario.TipoDocumentoIdListaValor = Convert.ToString(usuarioBD["TipoDocumento"]);
                empresaUsuario.TipoDocumento.Valor = Convert.ToString(usuarioBD["TipoDocumentoDescripcion"]);
                empresaUsuario.NumeroDocumento = Convert.ToString(usuarioBD["NumeroDocumento"]);
                empresaUsuario.SexoIdListaValor = Convert.ToString(usuarioBD["Sexo"]);
                empresaUsuario.Sexo.Valor = Convert.ToString(usuarioBD["SexoDescripcion"]);
                empresaUsuario.CorreoElectronico = Convert.ToString(usuarioBD["CorreoElectronico"]);
                empresaUsuario.TelefonoFijo = Convert.ToString(usuarioBD["TelefonoFijo"]);
                empresaUsuario.TelefonoCelular = Convert.ToString(usuarioBD["TelefonoCelular"]);
                empresaUsuario.TelefonoAnexo = Convert.ToString(usuarioBD["TelefonoAnexo"]);
                empresaUsuario.EstadoUsuarioIdListaValor = Convert.ToString(usuarioBD["Estado"]);
                empresaUsuario.Contrasena = Convert.ToString(usuarioBD["Contrasena"]);
                empresaUsuario.RepetirContrasena = Convert.ToString(usuarioBD["Contrasena"]);

                break; //sólo hay uno.
            }

            return empresaUsuario;
        }

        public List<VistaEmpresaUsuario> ObtenerUsuariosPorIdEmpresa(int idEmpresa)
        {
            List<VistaEmpresaUsuario> lista = new List<VistaEmpresaUsuario>();

            DataTable dtResultado = new DataTable();

            dtResultado = adEmpresaUsuario.ObtenerUsuariosPorIdEmpresa(idEmpresa);

            foreach (DataRow fila in dtResultado.Rows)
            {
                VistaEmpresaUsuario vista = new VistaEmpresaUsuario();

                vista.IdEmpresaUsuario = Convert.ToInt32(fila["IdEmpresaUsuario"]);
                vista.NombreUsuario = Convert.ToString(fila["Usuario"]);
                vista.NombresUsuario = Convert.ToString(fila["Nombres"]);
                vista.ApellidosUsuario = Convert.ToString(fila["Apellidos"]);
                vista.NombreRol = Convert.ToString(fila["UsuarioRolDescripcion"]);
                vista.NombreEstado = Convert.ToString(fila["UsuarioEstadoDescripcion"]);

                lista.Add(vista);
            }

            return lista;
        }

        /// <summary>
        /// Obtiene los usuarios activos(USEMAC) y los que tienen los roles ROLEAD, ROLEUS
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        public List<VistaEmpresaUsuario> ObtenerUsuariosActivosYPorRolesPorIdEmpresa(int idEmpresa)
        {
            List<VistaEmpresaUsuario> lista = new List<VistaEmpresaUsuario>();

            DataTable dtResultado = new DataTable();

            dtResultado = adEmpresaUsuario.ObtenerUsuariosPorIdEmpresa(idEmpresa);
            
            foreach (DataRow fila in dtResultado.Rows)
            {
                string estado = Convert.ToString(fila["EstadoUsuarioIdListaValor"]);
                string rol = Convert.ToString(fila["RolIdListaValor"]);

                //Se agregan a la lista los usuarios ACTIVOS y los que tienen uno de estos roles ROLEAD, ROLEUS.
                if (estado == Constantes.LISTAVALOR_ESTADO_DEL_USUARIO_ACTIVO && 
                    (rol == Constantes.LISTAVALOR_ROL_DEL_USUARIO_ADMINISTRADOREMPRESA ||
                    rol == Constantes.LISTAVALOR_ROL_DEL_USUARIO_USUARIOEMPRESA))
                { 

                    VistaEmpresaUsuario vista = new VistaEmpresaUsuario();

                    vista.IdEmpresaUsuario = Convert.ToInt32(fila["IdEmpresaUsuario"]);
                    vista.NombreUsuario = Convert.ToString(fila["Usuario"]);
                    vista.NombresUsuario = Convert.ToString(fila["Nombres"]);
                    vista.ApellidosUsuario = Convert.ToString(fila["Apellidos"]);
                    vista.NombreRol = Convert.ToString(fila["UsuarioRolDescripcion"]);
                    vista.NombreEstado = Convert.ToString(fila["UsuarioEstadoDescripcion"]);

                    vista.NombreCompletoUsuario = Convert.ToString(fila["UsuarioNombreCompleto"]);

                    lista.Add(vista);

                }
            }

            return lista;
        }

        public int ValidarNumeroDocumento(int idEmpresa, string numeroDocumento, int idEmpresaUsuario)
        {
            return adEmpresaUsuario.ValidarNumeroDocumento(idEmpresa, numeroDocumento, idEmpresaUsuario);
        }

        public int ValidarUsuario(int idEmpresa, string usuario, int idEmpresaUsuario)
        {
            return adEmpresaUsuario.ValidarUsuario(idEmpresa, usuario, idEmpresaUsuario);
        }

        public List<EmpresaUsuario> ObtenerUsuariosParaUTP(int nroPaginActual, int filasPorPagina, string nombre)
        {
            List<EmpresaUsuario> lista = new List<EmpresaUsuario>();

            DataTable dtResultado = adEmpresaUsuario.ObtenerUsuariosParaUTP(nroPaginActual, filasPorPagina, nombre);

            //Usuarios>
            foreach (DataRow usuarioBD in dtResultado.Rows)
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

                empresaUsuario.CantidadTotal = Convert.ToInt32(usuarioBD["CantidadTotal"]); ;

                lista.Add(empresaUsuario);
            }

            return lista;
        }
    }
}
