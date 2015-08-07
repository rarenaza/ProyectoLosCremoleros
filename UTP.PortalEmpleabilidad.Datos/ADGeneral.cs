using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTP.PortalEmpleabilidad.Datos
{
    public class ADGeneral
    {
        private string cadenaConexion = ConfigurationManager.ConnectionStrings["UTPConexionBD"].ConnectionString;
        
        public DataTable ObtenerReporteEquivalente()
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Obtener_ReporteEquivalencia";
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }
            return dtResultado;
        }
        public DataTable ObtenerListaValor(int idLista)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Lista_ListaValor";
                cmd.Parameters.Add(new SqlParameter("@IdLista", idLista));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();

            }

            return dtResultado;
        }

        public DataTable ObtenerListaValor2(int idLista)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Lista_ListaValor";
                cmd.Parameters.Add(new SqlParameter("@IdLista", idLista));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();

            }

            return dtResultado;
        }

        public DataTable EMPRESA_BUSCAR_INFORMACIONADICIONAL(string IDListaValorPadre)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EMPRESA_BUSCAR_INFORMACIONADICIONAL";
                cmd.Parameters.Add(new SqlParameter("@IDListaValorPadre", IDListaValorPadre));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();

            }

            return dtResultado;
        }



        public DataTable ObtenerListaValorPorIdPadre( string idListaPadre)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Lista_ListaValorPorIdPadre";
                cmd.Parameters.Add(new SqlParameter("@idListaPadre", idListaPadre));
                cmd.Connection = conexion;
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);
                conexion.Close();
            }
            return dtResultado;
        }

        public DataTable EmpresaHuntingBuscarSimple(string Nombre, int nroPagina, int filasPorPagina)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EmpresaHuntingBuscar";
                cmd.Parameters.Add(new SqlParameter("@PalabraClave", Nombre));
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

        public DataTable EmpresaHuntingBuscarAvanzada(string TipoDeEstudio, string Estudio, string EstadoEstudio,
            string SectorEmpresarial, int AnosExperiencia, string NombreCargo, string TipoInformacionAdicional, string Conocimiento, string Distrito,
            int nroPagina, int filasPorPagina)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EmpresaHuntingBuscarAvanzado";
                cmd.Parameters.Add(new SqlParameter("@TipoDeEstudio", TipoDeEstudio));
                cmd.Parameters.Add(new SqlParameter("@Estudio", Estudio));
                cmd.Parameters.Add(new SqlParameter("@EstadoEstudio", EstadoEstudio));
                cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial", SectorEmpresarial));
                cmd.Parameters.Add(new SqlParameter("@AnosExperiencia", AnosExperiencia));
                cmd.Parameters.Add(new SqlParameter("@NombreCargo", NombreCargo));
                cmd.Parameters.Add(new SqlParameter("@TipoInformacionAdicional", TipoInformacionAdicional));
                cmd.Parameters.Add(new SqlParameter("@Conocimiento", Conocimiento));
                cmd.Parameters.Add(new SqlParameter("@Distrito", Distrito));
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

        public DataTable Home_Departamento(int idLista)
        {
            DataTable dtResultado = new DataTable();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Lista_ListaValor";
                cmd.Parameters.Add(new SqlParameter("@IdLista", idLista));
                cmd.Connection = conexion;
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);
                conexion.Close();
            }
            return dtResultado;
        }



        public DataTable Home_ListarDistritos(string IDListaValorPadre)
        {
            DataTable dtResultado = new DataTable();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Home_ListarDistritos";
                cmd.Parameters.Add(new SqlParameter("@IDListaValorPadre", IDListaValorPadre));
                cmd.Connection = conexion;
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);
                conexion.Close();
            }
            return dtResultado;
        }

       

        public void InsertarLog(Error error)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "LogPortal_Insertar";

                //Parámetros:
                cmd.Parameters.Add(new SqlParameter("@Usuario", error.Usuario));
                cmd.Parameters.Add(new SqlParameter("@Accion", error.Accion));
                cmd.Parameters.Add(new SqlParameter("@Controlador", error.Controlador));
                cmd.Parameters.Add(new SqlParameter("@ErrorInnerException", error.ErrorInnerException));
                cmd.Parameters.Add(new SqlParameter("@ErrorMessage", error.ErrorMessage));
                cmd.Parameters.Add(new SqlParameter("@ErrorSource", error.ErrorSource));
                cmd.Parameters.Add(new SqlParameter("@ErrorStackTrace", error.ErrorStackTrace));
                cmd.Parameters.Add(new SqlParameter("@IP", error.IP));
               
                cmd.Connection = conexion;
                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();
            }
        }

        /// <summary>
        /// Obtiene los correos pendientes de envío.
        /// </summary>
        /// <returns></returns>
        public DataTable ObtenerOfertaCorreoPendientes()
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "OfertaCorreo_ObtenerPendientes";

                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();

            }

            return dtResultado;
        }

        public void ActualizarOfertaCorreo(int idOfertaCorreo, int enviado)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "OfertaCorreo_Actualizar";

                //Parámetros:
                cmd.Parameters.Add(new SqlParameter("@IdOfertaCorreo", idOfertaCorreo));
                cmd.Parameters.Add(new SqlParameter("@Enviado", enviado));                

                cmd.Connection = conexion;
                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();
            }
        }

        /// <summary>
        /// Actualiza el estado de las ofertas pasadas a finalizada y obtiene la lista de los IdOferta para el envío de mensajes.
        /// </summary>
        /// <returns></returns>
        public DataTable FinalizarOfertaPorFechaCV()
        {
            DataTable dtResultado = new DataTable();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Oferta_FinalizarPorFechaCV";              

                cmd.Connection = conexion;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);
                conexion.Close();
            }

            return dtResultado;
        }

        public DataTable OfertasVencidas()
        {
            DataTable dtResultado = new DataTable();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Oferta_ObtenerVencidas";

                cmd.Connection = conexion;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);
                conexion.Close();
            }

            return dtResultado;
        }
    }
}
