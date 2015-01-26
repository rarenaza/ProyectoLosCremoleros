using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Ofertas;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Alumno;
namespace UTP.PortalEmpleabilidad.Logica
{
    public class LNUTP
    {
        ADUTP adUtp = new ADUTP();

        public DataTable OfertasObtenerPendientes(int nroPaginaActual, int filasPorPagina)
        {            
            return adUtp.OfertasObtenerPendientes(nroPaginaActual, filasPorPagina);
        }

        

        public void UTP_ConvenioInsertar(Convenio convenio)
        {
            adUtp.UTP_ConvenioInsertar(convenio);
        }


        public DataTable EmpresaObtenerPendientes(int nroPagina, int filasPorPagina)
        {

            return adUtp.EmpresaObtenerPendientes(nroPagina, filasPorPagina);
        }

        public int UsuarioSistemaUTP_Exitencia(string Usuario)
        {
            
          return adUtp.UsuarioSistemaUTP_Exitencia(Usuario);

        }
       

        public List<EmpresaListaEmpresa> Utp_ListaEmpresas()
        {
            List<EmpresaListaEmpresa> listaEjemplo = new List<EmpresaListaEmpresa>();


            DataTable dtResultado = adUtp.Utp_ListaEmpresas();

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                EmpresaListaEmpresa vista = new EmpresaListaEmpresa();

                vista.IdEmpresa = Convert.ToInt32(dtResultado.Rows[i]["IdEmpresa"]);
                vista.NombreComercial = dtResultado.Rows[i]["NombreComercial"].ToString();
                vista.RazonSocial = dtResultado.Rows[i]["RazonSocial"].ToString();
               

                listaEjemplo.Add(vista);
            }

            return listaEjemplo;
        }

        public List<ConvenioListaAlumnosNombreyCodigo> Utp_ListarAlumnosNombreyCodigo()
        {
            List<ConvenioListaAlumnosNombreyCodigo> listaEjemplo = new List<ConvenioListaAlumnosNombreyCodigo>();


            DataTable dtResultado = adUtp.Utp_ListarAlumnosNombreyCodigo();

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                ConvenioListaAlumnosNombreyCodigo vista = new ConvenioListaAlumnosNombreyCodigo();

                vista.IdAlumno = Convert.ToInt32(dtResultado.Rows[i]["IdAlumno"]);
                vista.Alumno = dtResultado.Rows[i]["Alumno"].ToString();
               

                listaEjemplo.Add(vista);
            }

            return listaEjemplo;
        }

        public DataTable Utp_BuscarDatosAlumno(int idAlumno)
        {

            return adUtp.Utp_BuscarDatosAlumno(idAlumno);
        }

        public List<EmpresaListaEmpresa> Empresa_ObtenerPorNombre(string PalabraClave, int nroPaginaActual, int filasPorPagina)
        {
            List<EmpresaListaEmpresa> listaEjemplo = new List<EmpresaListaEmpresa>();


            DataTable dtResultado = adUtp.Empresa_ObtenerPorNombre(PalabraClave, nroPaginaActual, filasPorPagina);

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                EmpresaListaEmpresa vista = new EmpresaListaEmpresa();

                vista.IdEmpresa = Convert.ToInt32(dtResultado.Rows[i]["IdEmpresa"]);
                vista.NombreComercial = dtResultado.Rows[i]["Nombre"].ToString();
                vista.RazonSocial = dtResultado.Rows[i]["Razon"].ToString();
                vista.RUC = dtResultado.Rows[i]["RUC"].ToString();
                vista.Estado = dtResultado.Rows[i]["Estado"].ToString();
                vista.EjecutivoUTP = dtResultado.Rows[i]["EjecutivoUTP"].ToString();
                vista.SectorEmpresarial = dtResultado.Rows[i]["SectorEmpresarial"].ToString();
                vista.Clasificacion = dtResultado.Rows[i]["Clasificación"].ToString();
                vista.Ofertas = dtResultado.Rows[i]["Ofertas"].ToString();
                vista.NivelDeRelacion = dtResultado.Rows[i]["NivelDeRelacion"].ToString();
                vista.FacultadPrincipal = dtResultado.Rows[i]["FacultadPrincipal"].ToString();

                //vista.Comentarios = dtResultado.Rows[i]["Comentarios"].ToString();
                vista.TieneComentarios = dtResultado.Rows[i]["TieneComentarios"].ToString();
                vista.CantidadTotal = Convert.ToInt32(dtResultado.Rows[i]["CantidadTotal"]);

                listaEjemplo.Add(vista);
            }

            return listaEjemplo;
        }
        public List<EmpresaListaEmpresa> EmpresaBusquedaAvanzada(string NombreComercial, string IdEstadoEmpresa, string IdSector, string RazonSocial, int nroPaginaActual, int filasPorPagina)
        {
            List<EmpresaListaEmpresa> listaEjemplo = new List<EmpresaListaEmpresa>();


            DataTable dtResultado = adUtp.Empresa_BusquedaAvanzada(NombreComercial, IdEstadoEmpresa, IdSector, RazonSocial, nroPaginaActual, filasPorPagina);

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                EmpresaListaEmpresa vista = new EmpresaListaEmpresa();

                vista.IdEmpresa = Convert.ToInt32(dtResultado.Rows[i]["IdEmpresa"]);
                vista.NombreComercial = dtResultado.Rows[i]["Nombre"].ToString();
                vista.RazonSocial = dtResultado.Rows[i]["Razon"].ToString();
                vista.RUC = dtResultado.Rows[i]["RUC"].ToString();
                vista.Estado = dtResultado.Rows[i]["Estado"].ToString();
                vista.EjecutivoUTP = dtResultado.Rows[i]["EjecutivoUTP"].ToString();
                vista.SectorEmpresarial = dtResultado.Rows[i]["SectorEmpresarial"].ToString();
                vista.Clasificacion = dtResultado.Rows[i]["Clasificación"].ToString();
                vista.Ofertas = dtResultado.Rows[i]["Ofertas"].ToString();
                vista.IdEstadoEmpresa = dtResultado.Rows[i]["Idestado"].ToString();
                vista.IdSector = dtResultado.Rows[i]["Idsector"].ToString();
                vista.NivelDeRelacion = dtResultado.Rows[i]["NivelDeRelacion"].ToString();
                vista.FacultadPrincipal = dtResultado.Rows[i]["FacultadPrincipal"].ToString();
                //vista.Comentarios = dtResultado.Rows[i]["Comentarios"].ToString();
               
                vista.TieneComentarios = dtResultado.Rows[i]["TieneComentarios"].ToString();
                vista.CantidadTotal = Convert.ToInt32(dtResultado.Rows[i]["CantidadTotal"]);
                listaEjemplo.Add(vista);
            }

            return listaEjemplo;
        }

       


        public DataTable UTP_ObtenerUltimosAlumnos(string Dato)
        {

            return adUtp.UTP_ObtenerUltimosAlumnos(Dato);
        }
        public DataTable UTP_ObtenerUltimosConvenios(string Dato)
        {

            return adUtp.UTP_ObtenerUltimosConvenios(Dato);
        }
        public DataTable UTP_ObtenerConvenio(int idConvenio)
        {

            return adUtp.UTP_ObtenerConvenio(idConvenio);
        }
       
        public void ActualizarEstadoYUsuarioEC(Empresa empresa)
        {
            if (empresa.UsuarioEC == null) empresa.UsuarioEC = "";
            
            adUtp.ActualizarEstadoYUsuarioEC(empresa);

            //Se envía la notificación vía correo.
            
            Mensaje mensaje = new Mensaje();
            mensaje.DeUsuarioCorreoElectronico = empresa.Usuario;
            //Obtener usuario empresa y su correo electronico.
            LNMensaje lnMensaje = new LNMensaje ();
            DataTable dt = lnMensaje.ObtenerUsuarioEmpresaPorIdEmpresa(empresa.IdEmpresa);

            mensaje.ParaUsuarioCorreoElectronico = Convert.ToString(dt.Rows[0]["CorreoElectronico"]);
            mensaje.Asunto = "Empresa actualizada";
            mensaje.MensajeTexto = "El estado de la empresa '" + empresa.NombreComercial + "' han sido actualizados.";

            LNCorreo.EnviarCorreo(mensaje);
        }

        public DataTable UTP_ObtenerOfertasporActivar(string oferta, int nroPagina, int filasPorPagina)
        {

            return adUtp.UTP_ObtenerOfertasporActivar(oferta, nroPagina, filasPorPagina);
        }

        public DataTable UTP_ObtenerEventosObtenerBuscar(string evento, int nroPagina, int filasPorPagina)
        //public DataTable UTP_ObtenerEventosObtenerBuscar(string evento)
        {

            return adUtp.UTP_ObtenerEventosObtenerBuscar(evento, nroPagina, filasPorPagina);
            //return adUtp.UTP_ObtenerEventosObtenerBuscar(evento);
        }


        public DataTable Evento_ListaEstadoEvento()
        {

            return adUtp.Evento_ListaEstado();
        }

        public DataTable Evento_ListaTipoEvento()
        {

            return adUtp.Evento_ListaTipoEvento();
        }
        public DataTable EMPRESA_LISTAEMPRESA()
        {

            return adUtp.EMPRESA_LISTAEMPRESA();
        }

        #region Mantenimiento de Usuarios UTP

        public List<UTPUsuario> ObtenerUsuariosUTP()
        {
            List<UTPUsuario> lista = new List<UTPUsuario>();

            DataTable dtResultado = adUtp.ObtenerUsuariosUTP();

            foreach (DataRow fila in dtResultado.Rows)
            {
                UTPUsuario nuevo = new UTPUsuario();
                nuevo.IdUTPUsuario = Convert.ToInt32(fila["IdUTPUsuario"]);
                nuevo.NombreUsuario = Convert.ToString(fila["Usuario"]);
                nuevo.Nombres = Convert.ToString(fila["Nombres"]);
                nuevo.Apellidos = Convert.ToString(fila["Apellidos"]);
                nuevo.SexoDescripcion = Convert.ToString(fila["SexoDescripcion"]);
                nuevo.Correo = Convert.ToString(fila["CorreoElectronico"]);
                nuevo.TelefonoFijo = Convert.ToString(fila["TelefonoFijo"]);
                nuevo.TelefonoCelular = Convert.ToString(fila["TelefonoCelular"]);
                nuevo.EstadoUsuarioDescripcion = Convert.ToString(fila["EstadoUsuarioDescripcion"]);
                
                lista.Add(nuevo);
            }

            return lista;
        }

        public DataTable UTP_LISTAVALORPADRE()
        {

            return adUtp.UTP_LISTAVALORPADRE(); 
        }

        public DataTable UTP_LISTAVALORHIJO(int id)
        {

            return adUtp.UTP_LISTAVALORHIJO(id);
        }

        public DataTable UTP_BUSCARLISTAVALORPADRE(int id)
        {

            return adUtp.UTP_BUSCARLISTAVALORPADRE(id);
        }

        public UTPUsuario ObtenerUsuarioUTPPorId(int idUTPUsuario)
        {
            UTPUsuario utpUsuario = new UTPUsuario();
            DataTable dtResultado = adUtp.ObtenerUsuarioUTPPorId(idUTPUsuario);

            if (dtResultado.Rows.Count > 0)
            {
                utpUsuario.IdUTPUsuario = Convert.ToInt32(dtResultado.Rows[0]["IdUTPUsuario"]);
                utpUsuario.NombreUsuario = Convert.ToString(dtResultado.Rows[0]["Usuario"]);
                utpUsuario.Nombres = Convert.ToString(dtResultado.Rows[0]["Nombres"]);
                utpUsuario.Apellidos = Convert.ToString(dtResultado.Rows[0]["Apellidos"]);
                utpUsuario.SexoIdListaValor = Convert.ToString(dtResultado.Rows[0]["SexoIdListaValor"]);                
                utpUsuario.Correo = Convert.ToString(dtResultado.Rows[0]["CorreoElectronico"]);
                utpUsuario.TelefonoFijo = Convert.ToString(dtResultado.Rows[0]["TelefonoFijo"]);
                utpUsuario.TelefonoCelular = Convert.ToString(dtResultado.Rows[0]["TelefonoCelular"]);
                utpUsuario.EstadoUsuarioIdListaValor = Convert.ToString(dtResultado.Rows[0]["EstadoUsuarioIdListaValor"]);                
                utpUsuario.RolIdListaValor = Convert.ToString(dtResultado.Rows[0]["RolIdListaValor"]);
            }

            return utpUsuario;
        }

        public DataTable UTP_OBTENERVALORPADREEDITAR(string Cod)
        {
            return adUtp.UTP_OBTENERVALORPADREEDITAR(Cod);
        }



        public void Insertar(UTPUsuario utpUsuario)
        {
            adUtp.Insertar(utpUsuario);
        }

        public void UTPINSERTAR_LISTAVALORPADRE(Lista lista)
        {
            adUtp.UTPINSERTAR_LISTAVALORPADRE(lista);
        }

        public void UTPINSERTAR_LISTAVALORHIJO(ListaValor lista)
        {
            adUtp.UTPINSERTAR_LISTAVALORHIJO(lista);
        }

        public void UTPACTUALIZAR_LISTAVALORHIJO(ListaValor lista)
        {
            adUtp.UTPACTUALIZAR_LISTAVALORHIJO(lista);
        }

        public void UTPELIMINAR_LISTAVALORHIJO(string id)
        {
            adUtp.UTPELIMINAR_LISTAVALORHIJO(id);
        }


        public void UTPACTUALIZAR_LISTAVALORPADRE(Lista lista)
        {
            adUtp.UTPACTUALIZAR_LISTAVALORPADRE(lista);
        }

        public void Actualizar(UTPUsuario utpUsuario)
        {
            adUtp.Actualizar(utpUsuario);
        }

        #endregion
    }
}
