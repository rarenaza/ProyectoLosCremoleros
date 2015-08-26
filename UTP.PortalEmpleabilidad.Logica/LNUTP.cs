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
        public void UTP_ConvenioActualizar(Convenio convenio)
        {
            adUtp.UTP_ConvenioActualizar(convenio);
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
               
                vista.Ofertas = dtResultado.Rows[i]["Ofertas"].ToString();

                vista.NivelDeFacturacion = dtResultado.Rows[i]["NivelDeFacturacion"].ToString();
                vista.PosicionEnSector = dtResultado.Rows[i]["PosicionEnSector"].ToString();
                vista.Clasificacion = dtResultado.Rows[i]["Clasificación"].ToString();
                vista.NivelDeRelacion = dtResultado.Rows[i]["NivelDeRelacion"].ToString();
                vista.FacultadPrincipal = dtResultado.Rows[i]["FacultadPrincipal"].ToString();
                vista.FacultadSecundaria = dtResultado.Rows[i]["FacultadSecundaria"].ToString();

                //vista.Comentarios = dtResultado.Rows[i]["Comentarios"].ToString();
                vista.TieneComentarios = dtResultado.Rows[i]["TieneComentarios"].ToString();
                vista.CantidadTotal = Convert.ToInt32(dtResultado.Rows[i]["CantidadTotal"]);

                listaEjemplo.Add(vista);
            }

            return listaEjemplo;
        }

        public List<EmpresaListaEmpresa> EmpresaBusquedaAvanzada(string NombreComercial, string IdEstadoEmpresa, string IdSector, string RazonSocial, string IdentificadorTributario, int NroOferta, int NroPostulante, int nroPaginaActual, int filasPorPagina)
        {
            List<EmpresaListaEmpresa> listaEjemplo = new List<EmpresaListaEmpresa>();

            DataTable dtResultado = adUtp.Empresa_BusquedaAvanzada(NombreComercial, IdEstadoEmpresa, IdSector, RazonSocial, IdentificadorTributario, NroOferta, NroPostulante, nroPaginaActual, filasPorPagina);

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

                vista.Ofertas = dtResultado.Rows[i]["Ofertas"].ToString();

                vista.NivelDeFacturacion = dtResultado.Rows[i]["NivelDeFacturacion"].ToString();
                vista.PosicionEnSector = dtResultado.Rows[i]["PosicionEnSector"].ToString();
                vista.Clasificacion = dtResultado.Rows[i]["Clasificación"].ToString();
                vista.NivelDeRelacion = dtResultado.Rows[i]["NivelDeRelacion"].ToString();
                vista.FacultadPrincipal = dtResultado.Rows[i]["FacultadPrincipal"].ToString();
                vista.FacultadSecundaria = dtResultado.Rows[i]["FacultadSecundaria"].ToString();

                //vista.Comentarios = dtResultado.Rows[i]["Comentarios"].ToString();
                vista.TieneComentarios = dtResultado.Rows[i]["TieneComentarios"].ToString();
                vista.CantidadTotal = Convert.ToInt32(dtResultado.Rows[i]["CantidadTotal"]);

                listaEjemplo.Add(vista);
            }

            return listaEjemplo;
        }

        public DataTable EmpresaBusquedaExcel(
            string NombreComercial = "", 
            string IdEstadoEmpresa = "", 
            string IdSector = "", 
            string RazonSocial = "", 
            string IdentificadorTributario = "", 
            int NroOferta = 0, 
            int NroPostulante = 0) 
        {
            return adUtp.Excel_ObtenerEmpresas(NombreComercial, IdEstadoEmpresa, IdSector, RazonSocial, IdentificadorTributario, NroOferta, NroPostulante);
        }
  
        public DataTable UTP_ObtenerUltimosConvenios(string Dato)
        {
            return adUtp.UTP_ObtenerUltimosConvenios(Dato);
        }

        public Convenio UTP_ObtenerConvenio(int idConvenio)
        {
            DataSet dsResultado = adUtp.UTP_ObtenerConvenio(idConvenio);
            Convenio convenio = new Convenio();
            convenio.IdConvenio = idConvenio;
            
            List<AlumnoExperiencia> alumnoexperiencialst = new List<AlumnoExperiencia>();
            if (dsResultado.Tables.Count > 0)
            {
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    for(int n=0;n<=dsResultado.Tables[0].Rows.Count -1;n++){
                        convenio.IdConvenio = Funciones.ToInt(dsResultado.Tables[0].Rows[n]["IdConvenio"]);
                        convenio.IdAlumno = Funciones.ToInt(dsResultado.Tables[0].Rows[n]["IdAlumno"]);
                        convenio.Nombres = Funciones.ToString(dsResultado.Tables[0].Rows[n]["Nombres"]);
                        convenio.Apellidos = Funciones.ToString(dsResultado.Tables[0].Rows[n]["Apellidos"]);
                        convenio.CodAlumnoUtp = Funciones.ToString(dsResultado.Tables[0].Rows[n]["CodAlumnoUtp"]);
                        convenio.Carrera = Funciones.ToString(dsResultado.Tables[0].Rows[n]["Carrera"]);
                        convenio.NivelAcademico = Funciones.ToString(dsResultado.Tables[0].Rows[n]["NivelAcademico"]);
                        convenio.TelefonoFijoCasa = Funciones.ToString(dsResultado.Tables[0].Rows[n]["TelefonoFijoCasa"]);
                        convenio.TelefonoCelular = Funciones.ToString(dsResultado.Tables[0].Rows[n]["TelefonoCelular"]);
                        convenio.Ciclo = Funciones.ToInt(dsResultado.Tables[0].Rows[n]["Ciclo"]);
                        convenio.IdEmpresa = Funciones.ToInt(dsResultado.Tables[0].Rows[n]["IdEmpresa"]);
                        convenio.NombreComercial = Funciones.ToString(dsResultado.Tables[0].Rows[n]["NombreComercial"]);
                        convenio.RazonSocial = Funciones.ToString(dsResultado.Tables[0].Rows[n]["RazonSocial"]);
                        convenio.IdentificadorTributario = Funciones.ToString(dsResultado.Tables[0].Rows[n]["IdentificadorTributario"]);
                        convenio.SectorEmpresarial = Funciones.ToString(dsResultado.Tables[0].Rows[n]["SectorEmpresarial"]);
                        convenio.IdExperienciaCargo = Funciones.ToInt(dsResultado.Tables[0].Rows[n]["IdExperienciaCargo"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[n]["IdExperienciaCargo"]);
                        convenio.FechaInicioCargoAno = Funciones.ToInt(dsResultado.Tables[0].Rows[n]["FechaInicioCargoAno"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[n]["FechaInicioCargoAno"]);
                        convenio.FechaInicioCargoMes = Funciones.ToInt(dsResultado.Tables[0].Rows[n]["FechaInicioCargoMes"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[n]["FechaInicioCargoMes"]);
                        convenio.FechaFinCargoAno = Funciones.ToInt(dsResultado.Tables[0].Rows[n]["FechaFinCargoAno"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[n]["FechaFinCargoAno"]);
                        convenio.FechaFinCargoMes = Funciones.ToInt(dsResultado.Tables[0].Rows[n]["FechaFinCargoMes"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[n]["FechaFinCargoMes"]);
                        convenio.NombreCargo = Funciones.ToString(dsResultado.Tables[0].Rows[n]["NombreCargo"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[n]["NombreCargo"]);
                        convenio.DescripcionCargo = Funciones.ToString(dsResultado.Tables[0].Rows[n]["DescripcionCargo"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[n]["DescripcionCargo"]);
                        
                        convenio.ContactoNombre = Funciones.ToString(dsResultado.Tables[0].Rows[n]["ContactoNombre"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[n]["ContactoNombre"]);
                        convenio.ContactoCargo = Funciones.ToString(dsResultado.Tables[0].Rows[n]["ContactoCargo"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[n]["ContactoCargo"]);
                        convenio.ContactoCorreoElectronico = Funciones.ToString(dsResultado.Tables[0].Rows[n]["ContactoCorreoElectronico"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[n]["ContactoCorreoElectronico"]);
                        convenio.ContactoTelefono = Funciones.ToString(dsResultado.Tables[0].Rows[n]["ContactoTelefono"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[n]["ContactoTelefono"]);
                        convenio.ContactoCelular = Funciones.ToString(dsResultado.Tables[0].Rows[n]["ContactoCelular"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[n]["ContactoCelular"]);
                        convenio.TipoTrabajo = Funciones.ToString(dsResultado.Tables[0].Rows[n]["TipoTrabajo"]);
                        convenio.DuracionContrato = Funciones.ToInt(dsResultado.Tables[0].Rows[n]["DuracionContrato"]);
                        convenio.SalarioOfrecido = Funciones.ToDecimal(dsResultado.Tables[0].Rows[n]["SalarioOfrecido"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[n]["SalarioOfrecido"]);
                        convenio.CargoOfrecido = Funciones.ToString(dsResultado.Tables[0].Rows[n]["CargoOfrecido"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[n]["CargoOfrecido"]);
                        convenio.AreaEmpresa = Funciones.ToString(dsResultado.Tables[0].Rows[n]["AreaEmpresa"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[n]["AreaEmpresa"]);
                        convenio.FechaIngreso = Funciones.ToDateTime(dsResultado.Tables[0].Rows[n]["FechaIngreso"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[n]["FechaIngreso"]);
                        convenio.FuenteConvenio = Funciones.ToString(dsResultado.Tables[0].Rows[n]["FuenteConvenio"]);
                        convenio.Observaciones = Funciones.ToString(dsResultado.Tables[0].Rows[n]["Observaciones"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[n]["Observaciones"]);
                        convenio.CreadoPor = Funciones.ToString(dsResultado.Tables[0].Rows[n]["CreadoPor"]);
                        convenio.FechaCreacion = Funciones.ToDateTime(dsResultado.Tables[0].Rows[n]["FechaCreacion"]);
                        convenio.ModificadoPor = Funciones.ToString(dsResultado.Tables[0].Rows[n]["ModificadoPor"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[n]["ModificadoPor"]);
                        convenio.FechaModificacion = Funciones.ToDateTime(dsResultado.Tables[0].Rows[n]["FechaModificacion"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[n]["FechaModificacion"]);
                        
                        convenio.FechaInicio = Funciones.ToString(dsResultado.Tables[0].Rows[n]["FechaInicio"]);
                        convenio.FechaFin = Funciones.ToString(dsResultado.Tables[0].Rows[n]["FechaFin"]);
                        convenio.FechaRegistro = Funciones.ToString(dsResultado.Tables[0].Rows[n]["FechaRegistro"]);
                        convenio.EstadoConvenio = Funciones.ToString(dsResultado.Tables[0].Rows[n]["EstadoConvenio"]);
                        convenio.Clasificacion = Funciones.ToString(dsResultado.Tables[0].Rows[n]["Clasificacion"]);
                        convenio.EnvioEvaluacionJefe = Funciones.ToString(dsResultado.Tables[0].Rows[n]["EnvioEvaluacionJefe"]);
                        convenio.EnvioFormatoInformeAlumno = Funciones.ToString(dsResultado.Tables[0].Rows[n]["EnvioFormatoInformeAlumno"]);
                        convenio.EnvioEvaluacionEvaluador = Funciones.ToString(dsResultado.Tables[0].Rows[n]["EnvioEvaluacionEvaluador"]);
                        convenio.RecepcionEvaluacionJefe = Funciones.ToString(dsResultado.Tables[0].Rows[n]["RecepcionEvaluacionJefe"]);
                        convenio.RecepcionInformeAlumno = Funciones.ToString(dsResultado.Tables[0].Rows[n]["RecepcionInformeAlumno"]);
                        convenio.RecepcionEvaluacionEvaluador = Funciones.ToString(dsResultado.Tables[0].Rows[n]["RecepcionEvaluacionEvaluador"]);
                        convenio.NotaEvaluacionJefe = Funciones.ToString(dsResultado.Tables[0].Rows[n]["NotaEvaluacionJefe"]);
                        convenio.EvaluadorAlumno = Funciones.ToString(dsResultado.Tables[0].Rows[n]["EvaluadorAlumno"]);
                        convenio.NotaEvaluacionEvaluador = Funciones.ToString(dsResultado.Tables[0].Rows[n]["NotaEvaluacionEvaluador"]);
                        convenio.CodAlumnoUtp = Funciones.ToString(dsResultado.Tables[0].Rows[n]["CodigoAlumno"]);
                        convenio.MesInicio = Funciones.ToString(dsResultado.Tables[0].Rows[n]["MesInicio"]);
                        
                        break;
                    }
                }
                if (dsResultado.Tables[1].Rows.Count > 0)
                {
                    for (int n = 0; n <= dsResultado.Tables[0].Rows.Count - 1; n++) 
                    {
                        AlumnoExperiencia alumnoexperiencia = new AlumnoExperiencia();
                        alumnoexperiencia.IdExperienciaCargo = Funciones.ToInt(dsResultado.Tables[1].Rows[n]["IdExperienciaCargo"]);
                        alumnoexperiencia.FechaInicioCargoAno = Funciones.ToInt(dsResultado.Tables[1].Rows[n]["FechaInicioCargoAno"]);
                        alumnoexperiencia.FechaInicioCargoMes = Funciones.ToInt(dsResultado.Tables[1].Rows[n]["FechaInicioCargoMes"]);
                        alumnoexperiencia.FechaFinCargoAno = Funciones.ToInt(dsResultado.Tables[1].Rows[n]["FechaFinCargoAno"] == System.DBNull.Value ? null : dsResultado.Tables[1].Rows[n]["FechaFinCargoAno"]);
                        alumnoexperiencia.FechaFinCargoMes = Funciones.ToInt(dsResultado.Tables[1].Rows[n]["FechaFinCargoMes"] == System.DBNull.Value ? null : dsResultado.Tables[1].Rows[n]["FechaFinCargoMes"]);
                        alumnoexperiencia.NombreCargo = Funciones.ToString(dsResultado.Tables[1].Rows[n]["NombreCargo"]);
                        alumnoexperiencia.DescripcionCargo = Funciones.ToString(dsResultado.Tables[1].Rows[n]["DescripcionCargo"]);
                        alumnoexperiencia.Experiencia = 
                            Funciones.ToInt(dsResultado.Tables[1].Rows[n]["FechaInicioCargoMes"]) + "/" +
                            Funciones.ToInt(dsResultado.Tables[1].Rows[n]["FechaInicioCargoAno"]) + " - " +
                            Funciones.ToInt(dsResultado.Tables[1].Rows[n]["FechaFinCargoMes"] == System.DBNull.Value ? null : dsResultado.Tables[1].Rows[n]["FechaFinCargoMes"]) + "/" +
                            Funciones.ToInt(dsResultado.Tables[1].Rows[n]["FechaFinCargoAno"] == System.DBNull.Value ? null : dsResultado.Tables[1].Rows[n]["FechaFinCargoAno"]) + ": " + 
                            Funciones.ToString(dsResultado.Tables[1].Rows[n]["NombreCargo"]);

                        alumnoexperiencialst.Add(alumnoexperiencia);
                    }
                }
            }
            convenio.Experiencias = alumnoexperiencialst;

            return convenio;
        }
       
        public void ActualizarEstadoYUsuarioEC(Empresa empresa, string correoUTPDe)
        {
            if (empresa.UsuarioEC == null) empresa.UsuarioEC = "";
            
            adUtp.ActualizarEstadoYUsuarioEC(empresa);

            #region Envio de correo:
            //Se completan los campos:
            //Se indica el nombre del estado:
            string estado = "";
            if (empresa.EstadoIdListaValor == "EMPRAC") estado = "Activa";
            if (empresa.EstadoIdListaValor == "EMPRNO") estado = "No Activa";
            if (empresa.EstadoIdListaValor == "EMPRRV") estado = "Pendiente de aprobación de registro";
            if (empresa.EstadoIdListaValor == "EMPRSU") estado = "Suspendida";
                        
            //Obtener usuario empresa y su correo electronico.
            LNMensaje lnMensaje = new LNMensaje();
            DataTable dt = lnMensaje.ObtenerUsuarioEmpresaPorIdEmpresa(empresa.IdEmpresa);

            if (dt.Rows.Count > 0)
            { 
                Mensaje mensaje = new Mensaje();
                mensaje.DeUsuarioCorreoElectronico = correoUTPDe; // empresa.Usuario; //Contiene el ticket del usuario UTP.
                mensaje.ParaUsuarioCorreoElectronico = Convert.ToString(dt.Rows[0]["CorreoElectronico"]);
                mensaje.Asunto = "Empresa actualizada";            
                mensaje.MensajeTexto = "El estado de la empresa '" + empresa.NombreComercial + "' ha sido actualizado a: " + estado + "'";

                LNCorreo.EnviarCorreo(mensaje);
            }
            #endregion
           
        }

        public List<AlumnoUTP> UTP_ObtenerUltimosAlumnos(string Dato, int nroPagina, int filasPorPagina)
        {
            List<AlumnoUTP> listaEjemplo = new List<AlumnoUTP>();


            DataTable dtResultado = adUtp.UTP_ObtenerUltimosAlumnos(Dato, nroPagina, filasPorPagina);

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                AlumnoUTP vista = new AlumnoUTP();

                vista.FechaperiodoRegistro = Convert.ToDateTime(dtResultado.Rows[i]["FechaCreacion"]);
                vista.FechaRegistro = dtResultado.Rows[i]["FechaRegistro"].ToString();
                vista.Nombre = dtResultado.Rows[i]["Nombres"].ToString();
                vista.EstadoAlumno = dtResultado.Rows[i]["Valor"].ToString();
                vista.Apellidos = dtResultado.Rows[i]["Apellidos"].ToString();
                vista.Carrera = dtResultado.Rows[i]["Carrera"].ToString();
                vista.Ciclo = dtResultado.Rows[i]["CicloEquivalente"].ToString();
                vista.idAlumno = Convert.ToInt32(dtResultado.Rows[i]["IdAlumno"]);
                vista.completitud = Convert.ToInt32(dtResultado.Rows[i]["Completitud"]);
                vista.CantidadTotal = Convert.ToInt32(dtResultado.Rows[i]["CantidadTotal"]);
                listaEjemplo.Add(vista);
            }

            return listaEjemplo;
        }

        public List<AlumnoUTP> UTP_ObtenerUltimosAlumnosAvanzada(string Estudio, string Ciclo, string Sector, string Dato, string Sexo,
                                                           string Distrito, string TipoEstudio,string Conocimiento,string EstadoEstudio, int Completitud, int nroPagina, int filasPorPagina)
        {
            List<AlumnoUTP> listaEjemplo = new List<AlumnoUTP>();


            DataTable dtResultado = adUtp.UTP_ObtenerUltimosAlumnosAvanzada(Estudio, Ciclo, Sector, Dato, Sexo, Distrito, TipoEstudio, Conocimiento, EstadoEstudio, Completitud, nroPagina, filasPorPagina);

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                AlumnoUTP vista = new AlumnoUTP();

                vista.FechaperiodoRegistro = Convert.ToDateTime(dtResultado.Rows[i]["FechaCreacion"]);
                vista.FechaRegistro = dtResultado.Rows[i]["FechaRegistro"].ToString();
                vista.Nombre = dtResultado.Rows[i]["Nombres"].ToString();
                vista.EstadoAlumno = dtResultado.Rows[i]["EstadoAlumnoValor"].ToString();
                vista.Apellidos = dtResultado.Rows[i]["Apellidos"].ToString();
                vista.Carrera = dtResultado.Rows[i]["Carrera"].ToString();
                vista.Ciclo = dtResultado.Rows[i]["CicloEquivalente"].ToString();
                vista.idAlumno = Convert.ToInt32(dtResultado.Rows[i]["IdAlumno"]);
                vista.completitud = Convert.ToInt32(dtResultado.Rows[i]["Completitud"]);
                vista.CantidadTotal = Convert.ToInt32(dtResultado.Rows[i]["CantidadTotal"]);
                listaEjemplo.Add(vista);
            }

            return listaEjemplo;
        }

        public DataTable AlumnosBusquedaExcel(
            string Estudio = "", 
            string Ciclo = "", 
            string Sector = "", 
            string Dato = "", 
            string Sexo = "",
            string Distrito = "", 
            string TipoEstudio = "", 
            string Conocimiento = "", 
            string EstadoEstudio = "", 
            int Completitud = 0)
        {
            return  adUtp.Excel_ObtenerAlumnos(Estudio, Ciclo, Sector, Dato, Sexo, Distrito, TipoEstudio, Conocimiento, EstadoEstudio, Completitud);
        }

        public List<OfertaUTP> UTP_ObtenerOfertasporActivar(string Oferta, string TipoTrabajoUTP, int nroPagina, int filasPorPagina)
        {
            List<OfertaUTP> listaEjemplo = new List<OfertaUTP>();


            DataTable dtResultado = adUtp.UTP_ObtenerOfertasporActivar(Oferta, TipoTrabajoUTP, nroPagina, filasPorPagina);

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                OfertaUTP vista = new OfertaUTP();

                vista.FechaPublicacion = Convert.ToDateTime(dtResultado.Rows[i]["FechaPublicacion"]);
                vista.NombreComercial = Convert.ToString(dtResultado.Rows[i]["NombreComercial"]);
                vista.Clasificación = Convert.ToString(dtResultado.Rows[i]["Clasificación"]);
                vista.CargoOfrecido = Convert.ToString(dtResultado.Rows[i]["CargoOfrecido"]);
                vista.NumeroVacantes = Convert.ToString(dtResultado.Rows[i]["NumeroVacantes"]);
                vista.Cargo = Convert.ToString(dtResultado.Rows[i]["Cargo"]);
                vista.FacultadPrincipal = Convert.ToString(dtResultado.Rows[i]["FacultadPrincipal"]);
                vista.EjecutivoUTP = Convert.ToString(dtResultado.Rows[i]["EjecutivoUTP"]);
                vista.Nrocv = Convert.ToString(dtResultado.Rows[i]["Nrocv"]);
                vista.FechaSeguimiento = Convert.ToString(dtResultado.Rows[i]["FechaSeguimiento"]);
                vista.FechaFinRecepcionCV = Convert.ToString(dtResultado.Rows[i]["FechaFinRecepcionCV"]);
                //vista.FechaSeguimiento = Convert.ToDateTime(fila["FechaSeguimiento"] == DBNull.Value? (DateTime?)null :(DateTime)fila["FechaSeguimiento"]);

                vista.Comentarios = Convert.ToString(dtResultado.Rows[i]["TieneComentarios"]);

                vista.IdOferta = Convert.ToInt32(dtResultado.Rows[i]["IdOferta"]);
                vista.Estado = Convert.ToString(dtResultado.Rows[i]["Estado"]);

                vista.CantidadTotal = Convert.ToInt32(dtResultado.Rows[i]["CantidadTotal"]);

                listaEjemplo.Add(vista);
            }

            return listaEjemplo;
        }

        public List<OfertaUTP> UTP_ObtenerofertasAvanzada(
            string CargoOfrecido, 
            string NombreComercial, 
            string TipoCargo, 
            string Sector, 
            string Contrato, 
            int AExperiencia, 
            int Remuneracion, 
            string TipoEstudio, 
            string Conocimiento, 
            int NroPostulante, 
            string EstadoOferta, 
            string TipoInformacionAdicional, 
            string Carrera, 
            string TipoTrabajoUTP,
            int nroPagina, 
            int filasPorPagina)
        {
            List<OfertaUTP> listaEjemplo = new List<OfertaUTP>();

            DataTable dtResultado = adUtp.UTP_ObtenerofertasAvanzada(CargoOfrecido, NombreComercial, TipoCargo, Sector, Contrato, AExperiencia, Remuneracion, TipoEstudio, Conocimiento, NroPostulante, EstadoOferta, TipoInformacionAdicional, Carrera, TipoTrabajoUTP, nroPagina, filasPorPagina);

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                OfertaUTP vista = new OfertaUTP();

                vista.FechaPublicacion = Convert.ToDateTime(dtResultado.Rows[i]["FechaPublicacion"]);
                vista.NombreComercial = Convert.ToString(dtResultado.Rows[i]["NombreComercial"]);
                vista.Clasificación = Convert.ToString(dtResultado.Rows[i]["Clasificación"]);
                vista.CargoOfrecido = Convert.ToString(dtResultado.Rows[i]["CargoOfrecido"]);
                vista.NumeroVacantes = Convert.ToString(dtResultado.Rows[i]["NumeroVacantes"]);
                vista.Cargo = Convert.ToString(dtResultado.Rows[i]["Cargo"]);
                vista.FacultadPrincipal = Convert.ToString(dtResultado.Rows[i]["FacultadPrincipal"]);
                vista.EjecutivoUTP = Convert.ToString(dtResultado.Rows[i]["EjecutivoUTP"]);
                vista.Nrocv = Convert.ToString(dtResultado.Rows[i]["Nrocv"]);
                vista.FechaSeguimiento = Convert.ToString(dtResultado.Rows[i]["FechaSeguimiento"]);
                vista.FechaFinRecepcionCV = Convert.ToString(dtResultado.Rows[i]["FechaFinRecepcionCV"]);
                vista.Comentarios = Convert.ToString(dtResultado.Rows[i]["TieneComentarios"]);
                vista.IdOferta = Convert.ToInt32(dtResultado.Rows[i]["IdOferta"]);
                vista.Estado = Convert.ToString(dtResultado.Rows[i]["Estado"]);
                vista.CantidadTotal = Convert.ToInt32(dtResultado.Rows[i]["CantidadTotal"]);

                listaEjemplo.Add(vista);
            }

            return listaEjemplo;
        }

        public DataTable OfertaBusquedaExcel(
            string CargoOfrecido = "", 
            string NombreComercial = "", 
            string TipoCargo = "", 
            string Sector = "", 
            string Contrato = "", 
            int AExperiencia = 0, 
            int Remuneracion = 0, 
            string TipoEstudio = "", 
            string Conocimiento = "", 
            int NroPostulante = 0, 
            string EstadoOferta = "", 
            string TipoInformacionAdicional = "", 
            string Carrera = "",
            string TipoTrabajoUTP = "")
        {
            return adUtp.Excel_ObtenerOfertas(CargoOfrecido, NombreComercial, TipoCargo, Sector, Contrato, AExperiencia, Remuneracion, TipoEstudio, Conocimiento, NroPostulante, EstadoOferta, TipoInformacionAdicional, Carrera, TipoTrabajoUTP);
        } 

        public DataTable UTP_ObtenerEventosObtenerBuscar(string evento, int nroPagina, int filasPorPagina)
        {
            return adUtp.UTP_ObtenerEventosObtenerBuscar(evento, nroPagina, filasPorPagina);
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

        public void UTP_ActualizaSeguimientoOferta(Oferta oferta)
        {
            adUtp.UTP_ActualizaSeguimientoOferta(oferta);
        }

        #region Mantenimiento de Usuarios UTP

        //public List<UTPUsuario> ObtenerUsuariosUTP()
        //{
        //    List<UTPUsuario> lista = new List<UTPUsuario>();

        //    DataTable dtResultado = adUtp.ObtenerUsuariosUTP();

        //    foreach (DataRow fila in dtResultado.Rows)
        //    {
        //        UTPUsuario nuevo = new UTPUsuario();
        //        nuevo.IdUTPUsuario = Convert.ToInt32(fila["IdUTPUsuario"]);
        //        nuevo.NombreUsuario = Convert.ToString(fila["Usuario"]);
        //        nuevo.Nombres = Convert.ToString(fila["Nombres"]);
        //        nuevo.Apellidos = Convert.ToString(fila["Apellidos"]);
        //        nuevo.SexoDescripcion = Convert.ToString(fila["SexoDescripcion"]);
        //        nuevo.Correo = Convert.ToString(fila["CorreoElectronico"]);
        //        nuevo.TelefonoFijo = Convert.ToString(fila["TelefonoFijo"]);
        //        nuevo.TelefonoCelular = Convert.ToString(fila["TelefonoCelular"]);
        //        nuevo.EstadoUsuarioDescripcion = Convert.ToString(fila["EstadoUsuarioDescripcion"]);
        //        //nuevo.CantidadTotal = Convert.ToInt32(fila["CantidadTotal"]);

        //        lista.Add(nuevo);
        //    }



        //    return lista;
        //}

        public DataTable ObtenerUsuariosUTP(int nroPagina, int filasPorPagina)
        {

            return adUtp.ObtenerUsuariosUTP(nroPagina, filasPorPagina);
        }

        public DataTable UTP_LISTAVALORPADRE(int nroPagina, int filasPorPagina)
        {

            return adUtp.UTP_LISTAVALORPADRE(nroPagina, filasPorPagina); 
        }

        public DataTable UTP_LISTAVALORHIJO(int id, int nroPagina, int filasPorPagina)
        {

            return adUtp.UTP_LISTAVALORHIJO(id, nroPagina, filasPorPagina);
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
        public DataSet Reporte_AlumnosActivos()
        {

            return adUtp.Reporte_AlumnosActivos();
        }
        public DataSet Reporte_EmpresasActivas()
        {

            return adUtp.Reporte_EmpresasActivas();
        }

        public DataTable Reporte_EmpresasSegunClasificacion()
        {

            return adUtp.Reporte_EmpresasSegunClasificacion();
        }

        public DataTable Reporte_OfertasSegunClasificacion()
        {

            return adUtp.Reporte_OfertasSegunClasificacion();
        }

        public DataTable Reporte_EmpresasSegunClasificacionMensual(int? ano, int? mes)
        {

            return adUtp.Reporte_EmpresasSegunClasificacionMensual(ano, mes);
        }

        public DataTable Reporte_OfertasSegunClasificacionMensual(int? ano, int? mes)
        {

            return adUtp.Reporte_OfertasSegunClasificacionMensual(ano, mes);
        }
        public DataSet Reporte_OfertasSegunTipoTrabajoUTP(int? ano, int? mes)
        {

            return adUtp.Reporte_OfertasSegunTipoTrabajoUTP(ano, mes);
        }
        public DataTable Reporte_CarrerasSinOfertasDePracticas(int? ano, int? mes) 
        {

            return adUtp.Reporte_CarrerasSinOfertasDePracticas(ano, mes);
        }
        public DataTable Reporte_CarrerasSinOfertasDeEmpleo(int? ano, int? mes)
        {
            return adUtp.Reporte_CarrerasSinOfertasDeEmpleo(ano, mes);
        }
        public DataSet Reporte_VacantesOfrecidas()
        {
            return adUtp.Reporte_VacantesOfrecidas();
        }
        public DataSet Reporte_PostulacionesAOfertasDeEmpresasTarget()
        {
            return adUtp.Reporte_PostulacionesAOfertasDeEmpresasTarget();
        }
        public DataSet Reporte_OfertasConProcesosCerrados()
        {
            return adUtp.Reporte_OfertasConProcesosCerrados();
        }
        public DataSet Reporte_ClasificacionDeCvRecibidos(int? ano, int? mes)
        {
            return adUtp.Reporte_ClasificacionDeCvRecibidos(ano, mes);
        }
        public DataSet Reporte_Contrataciones()
        {
            return adUtp.Reporte_Contrataciones();
        }
        public DataTable Reporte_AlumnosActivosSegunCarrera()
        {
            return adUtp.Reporte_AlumnosActivosSegunCarrera();
        }
        public DataTable Reporte_CvCompletosSegunCarrera()
        {
            return adUtp.Reporte_CvCompletosSegunCarrera();
        }
        public DataTable Reporte_Top10CarrerasMasDemandadas()
        {
            return adUtp.Reporte_Top10CarrerasMasDemandadas();
        }
    }
}
