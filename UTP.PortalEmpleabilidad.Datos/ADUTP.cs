using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Ofertas;

namespace UTP.PortalEmpleabilidad.Datos
{
    public class ADUTP
    {
        private string cadenaConexion = ConfigurationManager.ConnectionStrings["UTPConexionBD"].ConnectionString;

        public DataTable OfertasObtenerPendientes(int nroPagina, int filasPorPagina)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ofertas_ObtenerPendientes";
                //Paginación:
                cmd.Parameters.Add(new SqlParameter("@NroPaginaActual", nroPagina));
                cmd.Parameters.Add(new SqlParameter("@FilasPorPagina", filasPorPagina));


                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();

            }

            return dtResultado;
        }



        public DataTable EmpresaObtenerPendientes(int nroPagina, int filasPorPagina)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Empresa_ObtenerPendientes";
                //Paginación:
                cmd.Parameters.Add(new SqlParameter("@NroPaginaActual", nroPagina));
                cmd.Parameters.Add(new SqlParameter("@FilasPorPagina", filasPorPagina));

                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();

            }

            return dtResultado;
        }




        public DataTable Utp_ListaEmpresas()
        {

            ADConexion cnn = new ADConexion();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Utp_ListaEmpresas";


            cmd.Connection = cnn.cn;
            cnn.Conectar();


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            cnn.Desconectar();

            return dt;
        }
        public DataTable Utp_ListarAlumnosNombreyCodigo()
        {

            ADConexion cnn = new ADConexion();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Utp_ListaAlumnosNombreyCodigo";


            cmd.Connection = cnn.cn;
            cnn.Conectar();


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            cnn.Desconectar();

            return dt;
        }

        public DataTable Empresa_ObtenerPorNombre(string PalabraClave, int nroPaginaActual, int filasPorPagina)
        {

            ADConexion cnn = new ADConexion();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Empresas_ObtenerNombre";
            cmd.Parameters.Add(new SqlParameter("@Nombre", PalabraClave));
            //Paginación.
            cmd.Parameters.Add(new SqlParameter("@NroPaginaActual", nroPaginaActual));
            cmd.Parameters.Add(new SqlParameter("@FilasPorPagina", filasPorPagina));

            cmd.Connection = cnn.cn;
            cnn.Conectar();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            cnn.Desconectar();

            return dt;
        }

        public DataTable Empresa_BusquedaAvanzada(string NombreComercial, string IdEstadoEmpresa, string IdSector, string RazonSocial, string IdentificadorTributario, int NroOferta, int NroPostulante, int nroPaginaActual, int filasPorPagina)
        {

            ADConexion cnn = new ADConexion();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Empresa_BusquedaAvanzada";
            cmd.Parameters.Add(new SqlParameter("@Nombre", NombreComercial));
            cmd.Parameters.Add(new SqlParameter("@Valor", IdEstadoEmpresa));
            cmd.Parameters.Add(new SqlParameter("@Sector", IdSector));
            cmd.Parameters.Add(new SqlParameter("@Razon", RazonSocial));
            cmd.Parameters.Add(new SqlParameter("@IdentificadorTributario", IdentificadorTributario));
            cmd.Parameters.Add(new SqlParameter("@NroOferta", NroOferta));
            cmd.Parameters.Add(new SqlParameter("@NroPostulante", NroPostulante));
            //Paginación.
            cmd.Parameters.Add(new SqlParameter("@NroPaginaActual", nroPaginaActual));
            cmd.Parameters.Add(new SqlParameter("@FilasPorPagina", filasPorPagina));


            cmd.Connection = cnn.cn;
            cnn.Conectar();


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            cnn.Desconectar();

            return dt;
        }

        public DataTable Excel_ObtenerEmpresas(
            string NombreComercial,
            string IdEstadoEmpresa,
            string IdSector,
            string RazonSocial,
            string IdentificadorTributario,
            int NroOferta,
            int NroPostulante)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Excel_ObtenerEmpresas";
                cmd.Parameters.Add(new SqlParameter("@Nombre", NombreComercial));
                cmd.Parameters.Add(new SqlParameter("@Valor", IdEstadoEmpresa));
                cmd.Parameters.Add(new SqlParameter("@Sector", IdSector));
                cmd.Parameters.Add(new SqlParameter("@Razon", RazonSocial));
                cmd.Parameters.Add(new SqlParameter("@IdentificadorTributario", IdentificadorTributario));
                cmd.Parameters.Add(new SqlParameter("@NroOferta", NroOferta));
                cmd.Parameters.Add(new SqlParameter("@NroPostulante", NroPostulante));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dt = new DataTable();

                da.Fill(dt);

                conexion.Close();
            }

            return dt;
        }

        public DataTable UTP_ObtenerUltimosConvenios(string Dato)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTP_ObtenerUltimosConvenios";
                cmd.Parameters.Add(new SqlParameter("@PalabraClave", Dato));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }


        public DataSet UTP_ObtenerConvenio(int idConvenio)
        {
            DataSet dsResultado = new DataSet();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Convenio_ObtenerDatos";
                cmd.Parameters.Add(new SqlParameter("@IdConvenio", idConvenio));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dsResultado = new DataSet();

                da.Fill(dsResultado);

                conexion.Close();
            }

            return dsResultado;
        }

        public void UTP_ConvenioInsertar(Convenio convenio)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Convenio_Insertar";

                //Parámetros:
                cmd.Parameters.Add(new SqlParameter("@IdAlumno", convenio.IdAlumno));
                cmd.Parameters.Add(new SqlParameter("@Carrera", convenio.Carrera));
                cmd.Parameters.Add(new SqlParameter("@NivelAcademico", "NULL"));
                cmd.Parameters.Add(new SqlParameter("@TelefonoFijoCasa", "NULL"));
                cmd.Parameters.Add(new SqlParameter("@TelefonoCelular", convenio.TelefonoCelular));
                cmd.Parameters.Add(new SqlParameter("@Ciclo", convenio.Ciclo));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", convenio.IdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@ContactoNombre", convenio.ContactoNombre));
                cmd.Parameters.Add(new SqlParameter("@ContactoCargo", convenio.ContactoCargo));
                cmd.Parameters.Add(new SqlParameter("@ContactoCorreoElectronico", convenio.ContactoCorreoElectronico));
                cmd.Parameters.Add(new SqlParameter("@ContactoTelefono", "NULL"));
                cmd.Parameters.Add(new SqlParameter("@ContactoCelular", convenio.ContactoCelular));
                cmd.Parameters.Add(new SqlParameter("@TipoTrabajo", "NULL"));
                cmd.Parameters.Add(new SqlParameter("@DuracionContrato", convenio.DuracionContrato));
                cmd.Parameters.Add(new SqlParameter("@SalarioOfrecido", convenio.SalarioOfrecido));
                cmd.Parameters.Add(new SqlParameter("@CargoOfrecido", convenio.CargoOfrecido));
                cmd.Parameters.Add(new SqlParameter("@AreaEmpresa", convenio.AreaEmpresa));
                cmd.Parameters.Add(new SqlParameter("@FechaIngreso", DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@FuenteConvenio", convenio.FuenteConvenio));
                cmd.Parameters.Add(new SqlParameter("@Observaciones", convenio.Observaciones));
                cmd.Parameters.Add(new SqlParameter("@CreadoPor", convenio.CreadoPor));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", SqlDbType.DateTime)).Value =  convenio.FechaInicio;
                cmd.Parameters.Add(new SqlParameter("@FechaFin", SqlDbType.DateTime)).Value =  convenio.FechaFin;
                cmd.Parameters.Add(new SqlParameter("@FechaRegistro", SqlDbType.DateTime)).Value =  convenio.FechaRegistro;
                cmd.Parameters.Add(new SqlParameter("@EstadoConvenio", convenio.EstadoConvenio));
                cmd.Parameters.Add(new SqlParameter("@Clasificacion", convenio.Clasificacion));
                cmd.Parameters.Add(new SqlParameter("@EnvioEvaluacionJefe", SqlDbType.DateTime)).Value =  convenio.EnvioEvaluacionJefe;
                cmd.Parameters.Add(new SqlParameter("@EnvioFormatoInformeAlumno", SqlDbType.DateTime)).Value =  convenio.EnvioFormatoInformeAlumno;
                cmd.Parameters.Add(new SqlParameter("@EnvioEvaluacionEvaluador", SqlDbType.DateTime)).Value =  convenio.EnvioEvaluacionEvaluador;
                cmd.Parameters.Add(new SqlParameter("@RecepcionEvaluacionJefe", SqlDbType.DateTime)).Value =  convenio.RecepcionEvaluacionJefe;
                cmd.Parameters.Add(new SqlParameter("@RecepcionInformeAlumno", SqlDbType.DateTime)).Value =  convenio.RecepcionInformeAlumno;
                cmd.Parameters.Add(new SqlParameter("@RecepcionEvaluacionEvaluador",  SqlDbType.DateTime)).Value = convenio.RecepcionEvaluacionEvaluador;
                cmd.Parameters.Add(new SqlParameter("@NotaEvaluacionJefe", convenio.NotaEvaluacionJefe));
                cmd.Parameters.Add(new SqlParameter("@EvaluadorAlumno", convenio.EvaluadorAlumno));
                cmd.Parameters.Add(new SqlParameter("@NotaEvaluacionEvaluador", convenio.NotaEvaluacionEvaluador));
                cmd.Parameters.Add(new SqlParameter("@CodigoAlumno", convenio.CodAlumnoUtp));
                cmd.Parameters.Add(new SqlParameter("@MesInicio", DBNull.Value));
                


                cmd.Connection = conexion;

                conexion.Open();

                cmd.ExecuteNonQuery();

                conexion.Close();
            }
        }
        public void UTP_ConvenioActualizar(Convenio convenio)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Convenio_Actualizar";

                //Parámetros:
                cmd.Parameters.Add(new SqlParameter("@IdConvenio", convenio.IdConvenio));
                cmd.Parameters.Add(new SqlParameter("@IdAlumno", convenio.IdAlumno));
                cmd.Parameters.Add(new SqlParameter("@Carrera", convenio.Carrera));
                cmd.Parameters.Add(new SqlParameter("@NivelAcademico", "NULL"));
                cmd.Parameters.Add(new SqlParameter("@TelefonoFijoCasa", "NULL"));
                cmd.Parameters.Add(new SqlParameter("@TelefonoCelular", convenio.TelefonoCelular));
                cmd.Parameters.Add(new SqlParameter("@Ciclo", convenio.Ciclo));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", convenio.IdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@ContactoNombre", convenio.ContactoNombre));
                cmd.Parameters.Add(new SqlParameter("@ContactoCargo", convenio.ContactoCargo));
                cmd.Parameters.Add(new SqlParameter("@ContactoCorreoElectronico", convenio.ContactoCorreoElectronico));
                cmd.Parameters.Add(new SqlParameter("@ContactoTelefono", "NULL"));
                cmd.Parameters.Add(new SqlParameter("@ContactoCelular", convenio.ContactoCelular));
                cmd.Parameters.Add(new SqlParameter("@IdExperienciaCargo", convenio.IdExperienciaCargo));
                cmd.Parameters.Add(new SqlParameter("@TipoTrabajo", "NULL"));
                cmd.Parameters.Add(new SqlParameter("@DuracionContrato", convenio.DuracionContrato));
                cmd.Parameters.Add(new SqlParameter("@SalarioOfrecido", convenio.SalarioOfrecido));
                cmd.Parameters.Add(new SqlParameter("@CargoOfrecido", convenio.CargoOfrecido));
                cmd.Parameters.Add(new SqlParameter("@AreaEmpresa", convenio.AreaEmpresa));
                cmd.Parameters.Add(new SqlParameter("@FechaIngreso", DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@FuenteConvenio", convenio.FuenteConvenio));
                cmd.Parameters.Add(new SqlParameter("@NuevaObservacion", convenio.NuevaObservacion));
                cmd.Parameters.Add(new SqlParameter("@ModificadoPor", convenio.ModificadoPor));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", SqlDbType.DateTime)).Value = convenio.FechaInicio;
                cmd.Parameters.Add(new SqlParameter("@FechaFin", SqlDbType.DateTime)).Value = convenio.FechaFin;
                cmd.Parameters.Add(new SqlParameter("@FechaRegistro", SqlDbType.DateTime)).Value = convenio.FechaRegistro;
                cmd.Parameters.Add(new SqlParameter("@EstadoConvenio", convenio.EstadoConvenio));
                cmd.Parameters.Add(new SqlParameter("@Clasificacion", convenio.Clasificacion));
                cmd.Parameters.Add(new SqlParameter("@EnvioEvaluacionJefe", SqlDbType.DateTime)).Value = convenio.EnvioEvaluacionJefe;
                cmd.Parameters.Add(new SqlParameter("@EnvioFormatoInformeAlumno", SqlDbType.DateTime)).Value = convenio.EnvioFormatoInformeAlumno;
                cmd.Parameters.Add(new SqlParameter("@EnvioEvaluacionEvaluador", SqlDbType.DateTime)).Value = convenio.EnvioEvaluacionEvaluador;
                cmd.Parameters.Add(new SqlParameter("@RecepcionEvaluacionJefe", SqlDbType.DateTime)).Value = convenio.RecepcionEvaluacionJefe;
                cmd.Parameters.Add(new SqlParameter("@RecepcionInformeAlumno", SqlDbType.DateTime)).Value = convenio.RecepcionInformeAlumno;
                cmd.Parameters.Add(new SqlParameter("@RecepcionEvaluacionEvaluador", SqlDbType.DateTime)).Value = convenio.RecepcionEvaluacionEvaluador;
                cmd.Parameters.Add(new SqlParameter("@NotaEvaluacionJefe", convenio.NotaEvaluacionJefe));
                cmd.Parameters.Add(new SqlParameter("@EvaluadorAlumno", convenio.EvaluadorAlumno));
                cmd.Parameters.Add(new SqlParameter("@NotaEvaluacionEvaluador", convenio.NotaEvaluacionEvaluador));
                cmd.Parameters.Add(new SqlParameter("@CodigoAlumno", convenio.CodAlumnoUtp));
                cmd.Parameters.Add(new SqlParameter("@MesInicio", DBNull.Value));  


                cmd.Connection = conexion;

                conexion.Open();

                cmd.ExecuteNonQuery();

                conexion.Close();
            }
        }

        public void ActualizarEstadoYUsuarioEC(Empresa empresa)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Empresa_ActualizarEstadoYUsuarioEC";

                    //Parámetros:
                    cmd.Parameters.Add(new SqlParameter("@IdEmpresa", empresa.IdEmpresa));
                    cmd.Parameters.Add(new SqlParameter("@Estado", empresa.EstadoIdListaValor));
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEC", empresa.UsuarioEC));

                    cmd.Parameters.Add(new SqlParameter("@Clasificacion", empresa.Clasificacion == null ? "" : empresa.Clasificacion));
                    cmd.Parameters.Add(new SqlParameter("@NivelDeRelacion", empresa.NivelDeRelacion == null ? "" : empresa.NivelDeRelacion));
                    cmd.Parameters.Add(new SqlParameter("@FacultadPrincipal", empresa.FacultadPrincipal == null ? "" : empresa.FacultadPrincipal));
                    cmd.Parameters.Add(new SqlParameter("@FacultadSecundaria", empresa.FacultadSecundaria == null ? "" : empresa.FacultadSecundaria));

                    cmd.Parameters.Add(new SqlParameter("@Usuario", empresa.Usuario));

                    cmd.Parameters.Add(new SqlParameter("@NivelDeFacturacion", empresa.NivelDeFacturacion));
                    cmd.Parameters.Add(new SqlParameter("@PosicionEnSector", empresa.PosicionEnSector == null ? "" : empresa.PosicionEnSector));

                    cmd.Parameters.Add(new SqlParameter("@FechaActivacion", empresa.EstadoIdListaValor == "EMPRAC" ? (object)DateTime.Now : DBNull.Value));

                    cmd.Parameters.Add(new SqlParameter("@NuevoComentario", empresa.NuevoComentario == null ? "" : empresa.NuevoComentario));

                    cmd.Connection = conexion;
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable Utp_BuscarDatosAlumno(int idAlumno)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Convenio_ObtenerDatosAlumno";
                cmd.Parameters.Add(new SqlParameter("@IdAlumno", idAlumno));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public void UTP_ActualizaSeguimientoOferta(Oferta oferta)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Oferta_ActualizaSeguimiento";

                //Parámetros:
                cmd.Parameters.Add(new SqlParameter("@IdOferta", oferta.IdOferta));
                //if (oferta.FechaSeguimiento.ToString("yyyyMMdd") != "19000101")
                //{
                //    cmd.Parameters.Add(new SqlParameter("@FechaSeguimiento", oferta.FechaSeguimiento));
                //}
                if (oferta.FechaSeguimiento != new DateTime(1, 1, 1))
                {
                    cmd.Parameters.Add(new SqlParameter("@FechaSeguimiento", oferta.FechaSeguimiento));
                }
                cmd.Parameters.Add(new SqlParameter("@NumeroPostulantes", oferta.NumeroPostulantes));
                cmd.Parameters.Add(new SqlParameter("@NumeroEntrevistados", oferta.NumeroEntrevistados));
                cmd.Parameters.Add(new SqlParameter("@NumeroContratados", oferta.NumeroContratados));
                cmd.Parameters.Add(new SqlParameter("@ConvenioRegistrado", oferta.ConvenioRegistrado));
                cmd.Parameters.Add(new SqlParameter("@Contacto", oferta.Contacto));
                cmd.Parameters.Add(new SqlParameter("@DatosContacto", oferta.DatosContacto));
                cmd.Parameters.Add(new SqlParameter("@MedioComunicacion", oferta.MedioComunicacion));
                //Datos del seguimiento:
                cmd.Parameters.Add(new SqlParameter("@SeguimientoCalificacion", oferta.SeguimientoCalificacion));
                cmd.Parameters.Add(new SqlParameter("@SeguimientoNroInvitados", oferta.SeguimientoNroInvitados));
                cmd.Parameters.Add(new SqlParameter("@SeguimientoContratados", oferta.SeguimientoContratados));
                cmd.Parameters.Add(new SqlParameter("@SeguimientoContratadosOtros", oferta.SeguimientoContratadosOtros));

                cmd.Parameters.Add(new SqlParameter("@TipoTrabajoUTP", oferta.TipoTrabajoUTP));

                cmd.Connection = conexion;

                conexion.Open();

                cmd.ExecuteNonQuery();

                conexion.Close();
            }
        }

        #region Mantenimiento de Usuarios UTP

        //public DataTable ObtenerUsuariosUTP()
        public DataTable ObtenerUsuariosUTP(int nroPagina, int filasPorPagina)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTPUsuario_ObtenerLista";
                ////Paginación:
                cmd.Parameters.Add(new SqlParameter("@NroPaginaActual", nroPagina));
                cmd.Parameters.Add(new SqlParameter("@FilasPorPagina", filasPorPagina));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public DataTable UTP_LISTAVALORPADRE(int nroPagina, int filasPorPagina)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTP_LISTAVALORPADRE";
                //Paginación:
                cmd.Parameters.Add(new SqlParameter("@NroPaginaActual", nroPagina));
                cmd.Parameters.Add(new SqlParameter("@FilasPorPagina", filasPorPagina));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public DataTable UTP_LISTAVALORHIJO(int id, int nroPagina, int filasPorPagina)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTP_LISTAVALORHIJO";
                cmd.Parameters.Add(new SqlParameter("@Idlista", id));
                //Paginación:
                cmd.Parameters.Add(new SqlParameter("@NroPaginaActual", nroPagina));
                cmd.Parameters.Add(new SqlParameter("@FilasPorPagina", filasPorPagina));

                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public DataTable UTPELIMINAR_LISTAVALORHIJO(string id)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTPELIMINAR_LISTAVALORHIJO";
                cmd.Parameters.Add(new SqlParameter("@IDListaValor", id));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public DataTable UTP_BUSCARLISTAVALORPADRE(int id)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTP_BUSCARLISTAVALORPADRE";
                cmd.Parameters.Add(new SqlParameter("@Idlista", id));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public DataTable ObtenerUsuarioUTPPorId(int idUTPUsuario)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTPUsuario_ObtenerPorId";
                cmd.Parameters.Add(new SqlParameter("@IdUTPUsuario", idUTPUsuario));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }





        public DataTable UTP_OBTENERVALORPADREEDITAR(string IDListaValor)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTP_OBTENERVALORPADREEDITAR";
                cmd.Parameters.Add(new SqlParameter("@IDListaValor", IDListaValor));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }


        public void Insertar(UTPUsuario utpUsuario)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTPUsuario_Insertar";

                //Parámetros:
                cmd.Parameters.Add(new SqlParameter("@Usuario", utpUsuario.NombreUsuario));
                cmd.Parameters.Add(new SqlParameter("@Nombres", utpUsuario.Nombres));
                cmd.Parameters.Add(new SqlParameter("@Apellidos", utpUsuario.Apellidos));
                cmd.Parameters.Add(new SqlParameter("@Sexo", utpUsuario.SexoIdListaValor == null ? "" : utpUsuario.SexoIdListaValor));


                cmd.Parameters.Add(new SqlParameter("@CorreoElectronico", utpUsuario.Correo));
                cmd.Parameters.Add(new SqlParameter("@TelefonoFijo", utpUsuario.TelefonoFijo == null ? "" : utpUsuario.TelefonoFijo));
                cmd.Parameters.Add(new SqlParameter("@TelefonoAnexo", utpUsuario.TelefonoAnexo == null ? "" : utpUsuario.TelefonoAnexo));
                cmd.Parameters.Add(new SqlParameter("@TelefonoCelular", utpUsuario.TelefonoCelular == null ? "" : utpUsuario.TelefonoCelular));
                cmd.Parameters.Add(new SqlParameter("@EstadoUsuario", utpUsuario.EstadoUsuarioIdListaValor));
                cmd.Parameters.Add(new SqlParameter("@Rol", utpUsuario.RolIdListaValor));
                cmd.Parameters.Add(new SqlParameter("@TipoUsuario", utpUsuario.TipoUsuarioIdListaValor));
                cmd.Parameters.Add(new SqlParameter("@CreadoPor", utpUsuario.CreadoPor));

                cmd.Connection = conexion;

                conexion.Open();

                cmd.ExecuteNonQuery();

                conexion.Close();
            }
        }

        public void UTPINSERTAR_LISTAVALORPADRE(Lista lista)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTPINSERTAR_LISTAVALORPADRE";

                //Parámetros:
                cmd.Parameters.Add(new SqlParameter("@NombreLista", lista.NombreLista));
                cmd.Parameters.Add(new SqlParameter("@DescripcionLista", lista.DescripcionLista));
                cmd.Parameters.Add(new SqlParameter("@Modificable", lista.Modificable));
                cmd.Parameters.Add(new SqlParameter("@CreadoPor", lista.Creadopor));


                cmd.Connection = conexion;

                conexion.Open();

                cmd.ExecuteNonQuery();

                conexion.Close();
            }
        }

        public void UTPINSERTAR_LISTAVALORHIJO(ListaValor lista)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTPINSERTAR_LISTAVALORHIJO";

                //Parámetros:
                cmd.Parameters.Add(new SqlParameter("@IDListaValor", lista.IdListaValor));
                cmd.Parameters.Add(new SqlParameter("@IDLista", lista.IdLista));
                cmd.Parameters.Add(new SqlParameter("@Valor", lista.Valor == null ? "" : lista.Valor));
                cmd.Parameters.Add(new SqlParameter("@DescripcionValor", lista.DescripcionValor == null ? System.Data.SqlTypes.SqlString.Null : lista.DescripcionValor));
                cmd.Parameters.Add(new SqlParameter("@Icono", lista.Icono == null ? System.Data.SqlTypes.SqlString.Null : lista.Icono));
                cmd.Parameters.Add(new SqlParameter("@Peso", lista.Peso == null ? 0 : lista.Peso));
                cmd.Parameters.Add(new SqlParameter("@ValorUTP", lista.ValorUTP == null ? System.Data.SqlTypes.SqlString.Null : lista.ValorUTP));
                cmd.Parameters.Add(new SqlParameter("@EstadoValor", lista.EstadoValor == null ? "" : lista.EstadoValor));
                cmd.Parameters.Add(new SqlParameter("@CreadoPor", lista.Creadopor));
                cmd.Connection = conexion;

                conexion.Open();

                cmd.ExecuteNonQuery();

                conexion.Close();
            }
        }

        public void UTPACTUALIZAR_LISTAVALORHIJO(ListaValor lista)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTPACTUALIZAR_LISTAVALORHIJO";

                //Parámetros:
                cmd.Parameters.Add(new SqlParameter("@IDListaValor", lista.IdListaValor));
                cmd.Parameters.Add(new SqlParameter("@Valor", lista.Valor));
                cmd.Parameters.Add(new SqlParameter("@DescripcionValor", lista.DescripcionValor == null ? System.Data.SqlTypes.SqlString.Null : lista.DescripcionValor));
                cmd.Parameters.Add(new SqlParameter("@Icono", lista.Icono == null ? System.Data.SqlTypes.SqlString.Null : lista.Icono));
                cmd.Parameters.Add(new SqlParameter("@Peso", lista.Peso == null ? 0 : lista.Peso));
                cmd.Parameters.Add(new SqlParameter("@ValorUTP", lista.ValorUTP == null ? System.Data.SqlTypes.SqlString.Null : lista.ValorUTP));
                cmd.Parameters.Add(new SqlParameter("@EstadoValor", lista.EstadoValor));
                cmd.Parameters.Add(new SqlParameter("@ModificadoPor", lista.Modificadopor == null ? System.Data.SqlTypes.SqlString.Null : lista.Modificadopor));
                cmd.Parameters.Add(new SqlParameter("@IdListaValorPadre", lista.IdListaValorPadre == null ? System.Data.SqlTypes.SqlString.Null : lista.IdListaValorPadre));

                cmd.Connection = conexion;

                conexion.Open();

                cmd.ExecuteNonQuery();

                conexion.Close();
            }
        }


        public void UTPACTUALIZAR_LISTAVALORPADRE(Lista lista)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTPACTUALIZAR_LISTAVALORPADRE";

                //Parámetros:
                cmd.Parameters.Add(new SqlParameter("@IDLista", lista.IDLista));
                cmd.Parameters.Add(new SqlParameter("@NombreLista", lista.NombreLista));
                cmd.Parameters.Add(new SqlParameter("@DescripcionLista", lista.DescripcionLista));
                cmd.Parameters.Add(new SqlParameter("@Modificable", lista.Modificable));
                cmd.Parameters.Add(new SqlParameter("@ModificadoPor", lista.Modificadopor));

                cmd.Connection = conexion;

                conexion.Open();

                cmd.ExecuteNonQuery();

                conexion.Close();

            }
        }



        public void Actualizar(UTPUsuario utpUsuario)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTPUsuario_Actualizar";

                //Parámetros:
                cmd.Parameters.Add(new SqlParameter("@Usuario", utpUsuario.NombreUsuario));
                cmd.Parameters.Add(new SqlParameter("@Nombres", utpUsuario.Nombres));
                cmd.Parameters.Add(new SqlParameter("@Apellidos", utpUsuario.Apellidos));
                cmd.Parameters.Add(new SqlParameter("@Sexo", utpUsuario.SexoIdListaValor == null ? "" : utpUsuario.SexoIdListaValor));
                cmd.Parameters.Add(new SqlParameter("@CorreoElectronico", utpUsuario.Correo));
                cmd.Parameters.Add(new SqlParameter("@TelefonoFijo", utpUsuario.TelefonoFijo == null ? "" : utpUsuario.TelefonoFijo));
                cmd.Parameters.Add(new SqlParameter("@TelefonoAnexo", utpUsuario.TelefonoAnexo == null ? "" : utpUsuario.TelefonoAnexo));
                cmd.Parameters.Add(new SqlParameter("@TelefonoCelular", utpUsuario.TelefonoCelular == null ? "" : utpUsuario.TelefonoCelular));
                cmd.Parameters.Add(new SqlParameter("@EstadoUsuario", utpUsuario.EstadoUsuarioIdListaValor));
                cmd.Parameters.Add(new SqlParameter("@Rol", utpUsuario.RolIdListaValor));
                cmd.Parameters.Add(new SqlParameter("@ModificadoPor", utpUsuario.ModificadoPor));

                cmd.Connection = conexion;

                conexion.Open();

                cmd.ExecuteNonQuery();

                conexion.Close();
            }
        }

        public int UsuarioSistemaUTP_Exitencia(string Usuario)
        {
            int cantidad = 0;
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "UsuarioSistemaUTP_Exitencia";

                    //Parámetros:
                    cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));


                    cmd.Connection = conexion;
                    conexion.Open();
                    cmd.ExecuteScalar();

                    object resultado = cmd.ExecuteScalar();
                    cantidad = Convert.ToInt32(resultado);

                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return cantidad;
        }

        public DataTable UTP_ObtenerOfertasporActivar(string oferta, string TipoTrabajoUTP, int nroPagina, int filasPorPagina)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTP_Obtenerofertasporactivar";
                cmd.Parameters.Add(new SqlParameter("@oferta", oferta));
                cmd.Parameters.Add(new SqlParameter("@TipoTrabajoUTP", TipoTrabajoUTP));

                //Paginación:
                cmd.Parameters.Add(new SqlParameter("@NroPaginaActual", nroPagina));
                cmd.Parameters.Add(new SqlParameter("@FilasPorPagina", filasPorPagina));

                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public DataTable UTP_ObtenerofertasAvanzada(
            string CargoOfrecido, 
            string NombreComercial, 
            string TipoCargo, 
            string sector, 
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
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTP_ObtenerofertasAvanzada";
                cmd.Parameters.Add(new SqlParameter("@Cargo", CargoOfrecido));
                cmd.Parameters.Add(new SqlParameter("@Empresa", NombreComercial));
                cmd.Parameters.Add(new SqlParameter("@TipoCargo", TipoCargo));
                cmd.Parameters.Add(new SqlParameter("@Sector", sector));
                cmd.Parameters.Add(new SqlParameter("@Contrato", Contrato));
                cmd.Parameters.Add(new SqlParameter("@AExperiencia", AExperiencia));
                cmd.Parameters.Add(new SqlParameter("@Remuneracion", Remuneracion));
                cmd.Parameters.Add(new SqlParameter("@TipoDeEstudio", TipoEstudio));
                cmd.Parameters.Add(new SqlParameter("@Conocimiento", Conocimiento));
                cmd.Parameters.Add(new SqlParameter("@NroPostulante", NroPostulante));
                cmd.Parameters.Add(new SqlParameter("@EstadoOferta", EstadoOferta));
                cmd.Parameters.Add(new SqlParameter("@TipoInformacionAdicional", TipoInformacionAdicional));
                cmd.Parameters.Add(new SqlParameter("@Carrera", Carrera));
                cmd.Parameters.Add(new SqlParameter("@TipoTrabajoUTP", TipoTrabajoUTP));
                cmd.Parameters.Add(new SqlParameter("@TipoDeEstudioPrincipal", Constantes.TIPO_ESTUDIO_PRINCIPAL));
                //Paginación:
                cmd.Parameters.Add(new SqlParameter("@NroPaginaActual", nroPagina));
                cmd.Parameters.Add(new SqlParameter("@FilasPorPagina", filasPorPagina));

                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public DataTable Excel_ObtenerOfertas(
            string CargoOfrecido, 
            string NombreComercial, 
            string TipoCargo, 
            string sector, 
            string Contrato, 
            int AExperiencia, 
            int Remuneracion, 
            string TipoEstudio, 
            string Conocimiento, 
            int NroPostulante, 
            string EstadoOferta, 
            string TipoInformacionAdicional,
            string Carrera,
            string TipoTrabajoUTP)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Excel_ObtenerOfertas";
                cmd.Parameters.Add(new SqlParameter("@Cargo", CargoOfrecido));
                cmd.Parameters.Add(new SqlParameter("@Empresa", NombreComercial));
                cmd.Parameters.Add(new SqlParameter("@TipoCargo", TipoCargo));
                cmd.Parameters.Add(new SqlParameter("@Sector", sector));
                cmd.Parameters.Add(new SqlParameter("@Contrato", Contrato));
                cmd.Parameters.Add(new SqlParameter("@AExperiencia", AExperiencia));
                cmd.Parameters.Add(new SqlParameter("@Remuneracion", Remuneracion));
                cmd.Parameters.Add(new SqlParameter("@TipoDeEstudio", TipoEstudio));
                cmd.Parameters.Add(new SqlParameter("@Conocimiento", Conocimiento));
                cmd.Parameters.Add(new SqlParameter("@NroPostulante", NroPostulante));
                cmd.Parameters.Add(new SqlParameter("@EstadoOferta", EstadoOferta));
                cmd.Parameters.Add(new SqlParameter("@TipoInformacionAdicional", TipoInformacionAdicional));
                cmd.Parameters.Add(new SqlParameter("@Carrera", Carrera));
                cmd.Parameters.Add(new SqlParameter("@TipoTrabajoUTP", TipoTrabajoUTP));
                cmd.Parameters.Add(new SqlParameter("@TipoDeEstudioPrincipal", Constantes.TIPO_ESTUDIO_PRINCIPAL));

                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dt = new DataTable();

                da.Fill(dt);

                conexion.Close();
            }

            return dt;
        }

        public DataTable UTP_ObtenerEventosObtenerBuscar(string evento, int nroPagina, int filasPorPagina)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTP_ObtenerEventosObtenerBuscar";
                cmd.Parameters.Add(new SqlParameter("@Evento", evento));
                //Paginación:
                cmd.Parameters.Add(new SqlParameter("@NroPaginaActual", nroPagina));
                cmd.Parameters.Add(new SqlParameter("@FilasPorPagina", filasPorPagina));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public DataTable UTP_ObtenerUltimosAlumnos(string Dato, int nroPagina, int filasPorPagina)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTP_ObtenerUltimosAlumnos";
                cmd.Parameters.Add(new SqlParameter("@Dato", Dato));
                //Paginación:
                cmd.Parameters.Add(new SqlParameter("@NroPaginaActual", nroPagina));
                cmd.Parameters.Add(new SqlParameter("@FilasPorPagina", filasPorPagina));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public DataTable UTP_ObtenerUltimosAlumnosAvanzada(string Estudio, string Ciclo, string Sector, string Dato, string Sexo,
                                                           string Distrito, string TipoEstudio, string Conocimiento, string EstadoEstudio, int Completitud, int nroPagina, int filasPorPagina)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTP_ObtenerUltimosAlumnosAvanzada";
                cmd.Parameters.Add(new SqlParameter("@Estudio", Estudio));
                cmd.Parameters.Add(new SqlParameter("@Ciclo", Ciclo));
                cmd.Parameters.Add(new SqlParameter("@Sector", Sector));
                cmd.Parameters.Add(new SqlParameter("@Dato", Dato));
                cmd.Parameters.Add(new SqlParameter("@Sexo", Sexo));
                cmd.Parameters.Add(new SqlParameter("@Distrito", Distrito));
                cmd.Parameters.Add(new SqlParameter("@TipoEstudio", TipoEstudio));
                cmd.Parameters.Add(new SqlParameter("@Conocimiento", Conocimiento));
                cmd.Parameters.Add(new SqlParameter("@EstadoEstudio", EstadoEstudio));
                cmd.Parameters.Add(new SqlParameter("@Completitud", Completitud));

                //Paginación:
                cmd.Parameters.Add(new SqlParameter("@NroPaginaActual", nroPagina));
                cmd.Parameters.Add(new SqlParameter("@FilasPorPagina", filasPorPagina));

                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public DataTable Excel_ObtenerAlumnos(
            string Dato,
            string Estudio, 
            string Ciclo, 
            string Sector, 
            string Sexo,
            string Distrito, 
            string TipoEstudio, 
            string Conocimiento, 
            string EstadoEstudio, 
            int Completitud)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Excel_ObtenerAlumnos";
                cmd.Parameters.Add(new SqlParameter("@Estudio", Estudio));
                cmd.Parameters.Add(new SqlParameter("@Ciclo", Ciclo));
                cmd.Parameters.Add(new SqlParameter("@Sector", Sector));
                cmd.Parameters.Add(new SqlParameter("@Dato", Dato));
                cmd.Parameters.Add(new SqlParameter("@Sexo", Sexo));
                cmd.Parameters.Add(new SqlParameter("@Distrito", Distrito));
                cmd.Parameters.Add(new SqlParameter("@TipoEstudio", TipoEstudio));
                cmd.Parameters.Add(new SqlParameter("@Conocimiento", Conocimiento));
                cmd.Parameters.Add(new SqlParameter("@EstadoEstudio", EstadoEstudio));
                cmd.Parameters.Add(new SqlParameter("@Completitud", Completitud));

                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dt = new DataTable();

                da.Fill(dt);

                conexion.Close();
            }

            return dt;
        }

        public DataTable Evento_ListaEstado()
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Evento_ListaEstado";

                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();

            }

            return dtResultado;
        }

        public DataTable Evento_ListaTipoEvento()
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Evento_ListaTipoEvento";

                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();

            }

            return dtResultado;
        }

        public DataTable EMPRESA_LISTAEMPRESA()
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EMPRESA_LISTAEMPRESA";

                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();

            }

            return dtResultado;
        }

        #endregion

        public DataSet Reporte_AlumnosActivos()
        {
            DataSet dsResultado = new DataSet();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Reporte_Alumnos_Activos";
                cmd.Connection = conexion;
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dsResultado = new DataSet();
                da.Fill(dsResultado);
                conexion.Close();
            }
            return dsResultado;
        }

        public DataSet Reporte_EmpresasActivas()
        {
            DataSet dsResultado = new DataSet();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Reporte_Empresas_Activas";
                cmd.Connection = conexion;
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dsResultado = new DataSet();
                da.Fill(dsResultado);
                conexion.Close();
            }
            return dsResultado;
        }

        public DataTable Reporte_EmpresasSegunClasificacion()
        {
            DataTable dtResultado = new DataTable();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Reporte_Empresas_Segun_Clasificacion";
                cmd.Connection = conexion;
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);
                conexion.Close();
            }
            return dtResultado;
        }

        public DataTable Reporte_OfertasSegunClasificacion()
        {
            DataTable dtResultado = new DataTable();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Reporte_Ofertas_Segun_Clasificacion";
                cmd.Connection = conexion;
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);
                conexion.Close();
            }
            return dtResultado;
        }

        public DataTable Reporte_EmpresasSegunClasificacionMensual(int? ano, int? mes)
        {
            DataTable dtResultado = new DataTable();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Reporte_Empresas_Segun_Clasificacion_Mensual";
                cmd.Parameters.Add(new SqlParameter("@Ano", ano));
                cmd.Parameters.Add(new SqlParameter("@Mes", mes));
                cmd.Connection = conexion;
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);
                conexion.Close();
            }
            return dtResultado;
        }

        public DataTable Reporte_OfertasSegunClasificacionMensual(int? ano, int? mes)
        {
            DataTable dtResultado = new DataTable();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Reporte_Ofertas_Segun_Clasificacion_Mensual";
                cmd.Parameters.Add(new SqlParameter("@Ano", ano));
                cmd.Parameters.Add(new SqlParameter("@Mes", mes));
                cmd.Connection = conexion;
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);
                conexion.Close();
            }
            return dtResultado;
        }

        public DataSet Reporte_OfertasSegunTipoTrabajoUTP(int? ano, int? mes)
        {
            DataSet dtResultado = new DataSet();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Reporte_Ofertas_Segun_Tipo_Trabajo_UTP";
                cmd.Parameters.Add(new SqlParameter("@Ano", ano));
                cmd.Parameters.Add(new SqlParameter("@Mes", mes));
                cmd.Connection = conexion;
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataSet();
                da.Fill(dtResultado);
                conexion.Close();
            }
            return dtResultado;
        }
        public DataTable Reporte_CarrerasSinOfertasDePracticas(int? ano, int? mes)
        {
            DataTable dtResultado = new DataTable();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Reporte_Carreras_Sin_Ofertas_de_Practicas";
                cmd.Parameters.Add(new SqlParameter("@Ano", ano));
                cmd.Parameters.Add(new SqlParameter("@Mes", mes));
                cmd.Parameters.Add(new SqlParameter("@TipoDeEstudioPrincipal", Constantes.TIPO_ESTUDIO_PRINCIPAL));
                cmd.Connection = conexion;
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);
                conexion.Close();
            }
            return dtResultado;
        }
        public DataTable Reporte_CarrerasSinOfertasDeEmpleo(int? ano, int? mes)
        {
            DataTable dtResultado = new DataTable();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Reporte_Carreras_Sin_Ofertas_de_Empleo";
                cmd.Parameters.Add(new SqlParameter("@Ano", ano));
                cmd.Parameters.Add(new SqlParameter("@Mes", mes));
                cmd.Parameters.Add(new SqlParameter("@TipoDeEstudioPrincipal", Constantes.TIPO_ESTUDIO_PRINCIPAL));
                cmd.Connection = conexion;
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);
                conexion.Close();
            }
            return dtResultado;
        }
        public DataSet Reporte_VacantesOfrecidas()
        {
            DataSet dtResultado = new DataSet();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Reporte_Vacantes_Ofrecidas";
                cmd.Connection = conexion;
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataSet();
                da.Fill(dtResultado);
                conexion.Close();
            }
            return dtResultado;
        }
        public DataSet Reporte_PostulacionesAOfertasDeEmpresasTarget()
        {
            DataSet dtResultado = new DataSet();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Reporte_Postulaciones_A_Ofertas_De_Empresas_Target";
                cmd.Connection = conexion;
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataSet();
                da.Fill(dtResultado);
                conexion.Close();
            }
            return dtResultado;
        }
        public DataSet Reporte_OfertasConProcesosCerrados()
        {
            DataSet dtResultado = new DataSet();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Reporte_Ofertas_Con_Procesos_Cerrados";
                cmd.Connection = conexion;
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataSet();
                da.Fill(dtResultado);
                conexion.Close();
            }
            return dtResultado;
        }
        public DataSet Reporte_ClasificacionDeCvRecibidos(int? ano, int? mes)
        {
            DataSet dtResultado = new DataSet();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Reporte_Clasificacion_CVs_Recibidos";
                cmd.Parameters.Add(new SqlParameter("@Ano", ano));
                cmd.Parameters.Add(new SqlParameter("@Mes", mes));
                cmd.Connection = conexion;
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataSet();
                da.Fill(dtResultado);
                conexion.Close();
            }
            return dtResultado;
        }
        public DataSet Reporte_Contrataciones()
        {
            DataSet dtResultado = new DataSet();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Reporte_Contrataciones";
                cmd.Connection = conexion;
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataSet();
                da.Fill(dtResultado);
                conexion.Close();
            }
            return dtResultado;
        }
        public DataTable Reporte_AlumnosActivosSegunCarrera()
        {
            DataTable dtResultado = new DataTable();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Reporte_Alumnos_Por_Carrera";
                cmd.Parameters.Add(new SqlParameter("@TipoDeEstudioPrincipal", Constantes.TIPO_ESTUDIO_PRINCIPAL));
                cmd.Connection = conexion;
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);
                conexion.Close();
            }
            return dtResultado;
        }
        public DataTable Reporte_CvCompletosSegunCarrera()
        {
            DataTable dtResultado = new DataTable();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Reporte_Cv_Completo_Por_Carrera";
                cmd.Connection = conexion;
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);
                conexion.Close();
            }
            return dtResultado;
        }        
        public DataTable Reporte_Top10CarrerasMasDemandadas()
        {
            DataTable dtResultado = new DataTable();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Reporte_Carreras_Mas_Demandadas_Top10";
                cmd.Parameters.Add(new SqlParameter("@TipoDeEstudioPrincipal", Constantes.TIPO_ESTUDIO_PRINCIPAL));
                cmd.Connection = conexion;
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);
                conexion.Close();
            }
            return dtResultado;
        }

    }
}
