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
    public class ADOferta
    {
        private string cadenaConexion = ConfigurationManager.ConnectionStrings["UTPConexionBD"].ConnectionString;
        /// <summary>
        /// Obtiene las Ofertas Para la Pantalla Oferta
        /// </summary>
        /// <returns></returns>
        ///
        public DataTable ObtenerOfertasAlumnoPorID(int idOferta)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "OfertaAlumno_ObtenerPorId";
                cmd.Parameters.Add(new SqlParameter("@idOferta", SqlDbType.VarChar, 100)).Value = idOferta;
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }
        public DataTable BuscarOfertasAlumno(int DescripcionOferta)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Oferta_ListarUltimosAlumno";
                cmd.Parameters.Add(new SqlParameter("@DescripcionOferta", SqlDbType.VarChar, 100)).Value = DescripcionOferta;
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public DataTable BuscarFiltroOfertasAlumno(int IdAlumno,string PalabraClave)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Oferta_BusquedaFiltroAlumno";
                cmd.Parameters.Add(new SqlParameter("@IdAlumno", SqlDbType.Int)).Value = IdAlumno;
                cmd.Parameters.Add(new SqlParameter("@PalabraClave", SqlDbType.VarChar, 200)).Value = PalabraClave;

                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public DataTable BuscarAvanzadoOfertasAlumno(VistaOfertaAlumno entidad)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Oferta_ListarUltimosAlumno";
                //cmd.Parameters.Add(new SqlParameter("@DescripcionOferta", SqlDbType.VarChar, 100)).Value = entidad.;
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public DataTable MostrarUltimasOfertas(int IdAlumno)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Oferta_ListarUltimosAlumno";
                cmd.Parameters.Add(new SqlParameter("@IdAlumno", SqlDbType.Int)).Value = IdAlumno;
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }
        public DataTable Obtener()
        {
            DataTable dtResultado = new DataTable();
 
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Oferta_Obtener";
                cmd.Connection = conexion;
                
                conexion.Open();
               
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }


        public DataTable ObtenerPostulantes()
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "OfertaPostulante_Obtener";
          
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
              
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        //Obtiene las Listas de opciones (Todo los Combos)
        public DataTable ObtenerLista_ListaValor(int Cod)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Lista_ListaValor";
                cmd.Parameters.Add(new SqlParameter("@IdLista", SqlDbType.NVarChar)).Value = Cod;
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();

            }

            return dtResultado;
        }
        

                       
        public DataTable Obtener_PanelEmpresa(int idEmpresa, string filtroBusqueda)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Oferta_ObtenerPanelEmpresa";
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", idEmpresa));
                cmd.Parameters.Add(new SqlParameter("@FiltroBusqueda", filtroBusqueda));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);               
                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public void Insertar(Oferta oferta)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Oferta_Insertar";

                    //Parámetros:
                    cmd.Parameters.Add(new SqlParameter("@IdEmpresa", oferta.IdEmpresa));
                    cmd.Parameters.Add(new SqlParameter("@Funciones", oferta.Funciones));
                    cmd.Parameters.Add(new SqlParameter("@Competencias", oferta.Competencias));
                    cmd.Parameters.Add(new SqlParameter("@UsuarioPropietarioEmpresa", oferta.UsuarioPropietarioEmpresa));
                    cmd.Parameters.Add(new SqlParameter("@EstadoOferta", oferta.EstadoOferta));
                    //cmd.Parameters.Add(new SqlParameter("@FechaPublicacion", oferta.FechaPublicacion));
                    cmd.Parameters.Add(new SqlParameter("@FechaFinRecepcionCV", oferta.FechaFinRecepcionCV));
                    cmd.Parameters.Add(new SqlParameter("@IdEmpresaLocacion", oferta.IdEmpresaLocacion));
                    cmd.Parameters.Add(new SqlParameter("@TipoTrabajo", oferta.TipoTrabajoIdListaValor));
                    cmd.Parameters.Add(new SqlParameter("@TipoContrato", oferta.TipoContratoIdListaValor));
                    cmd.Parameters.Add(new SqlParameter("@DuracionContrato", oferta.DuracionContrato));
                    cmd.Parameters.Add(new SqlParameter("@TipoCargo", oferta.TipoCargoIdListaValor));
                    cmd.Parameters.Add(new SqlParameter("@CargoOfrecido", oferta.CargoOfrecido));
                    cmd.Parameters.Add(new SqlParameter("@RemuneracionOfrecida", oferta.RemuneracionOfrecida));
                    cmd.Parameters.Add(new SqlParameter("@Horario", oferta.Horario));
                    cmd.Parameters.Add(new SqlParameter("@AreaEmpresa", oferta.AreaEmpresa));
                    cmd.Parameters.Add(new SqlParameter("@NumeroVacantes", oferta.NumeroVacantes));
                    cmd.Parameters.Add(new SqlParameter("@RequiereExperienciaLaboral", oferta.RequiereExperienciaLaboral));
                    cmd.Parameters.Add(new SqlParameter("@RecibeCorreos", oferta.RecibeCorreosIdListaValor));
                    cmd.Parameters.Add(new SqlParameter("@CreadoPor", oferta.CreadoPor));

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

        /// <summary>
        /// Se actualiza la oferta.
        /// </summary>
        /// <param name="oferta"></param>
        /// <returns></returns>
        public bool Actualizar(Oferta oferta)
        {
            bool procesoExitoso = false;

            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Oferta_Actualizar";

                    //Parámetros:
                    cmd.Parameters.Add(new SqlParameter("@IdOferta", oferta.IdOferta));
                    cmd.Parameters.Add(new SqlParameter("@IdEmpresa", oferta.IdEmpresa));
                    cmd.Parameters.Add(new SqlParameter("@Funciones", oferta.Funciones));
                    cmd.Parameters.Add(new SqlParameter("@Competencias", oferta.Competencias));
                    cmd.Parameters.Add(new SqlParameter("@UsuarioPropietarioEmpresa", oferta.UsuarioPropietarioEmpresa));
                    cmd.Parameters.Add(new SqlParameter("@FechaFinRecepcionCV", oferta.FechaFinRecepcionCV));
                    cmd.Parameters.Add(new SqlParameter("@IdEmpresaLocacion", oferta.IdEmpresaLocacion));                    
                    cmd.Parameters.Add(new SqlParameter("@TipoTrabajo", oferta.TipoTrabajoIdListaValor));
                    cmd.Parameters.Add(new SqlParameter("@TipoContrato", oferta.TipoContratoIdListaValor));
                    cmd.Parameters.Add(new SqlParameter("@DuracionContrato", oferta.DuracionContrato));
                    cmd.Parameters.Add(new SqlParameter("@TipoCargo", oferta.TipoCargoIdListaValor));
                    cmd.Parameters.Add(new SqlParameter("@CargoOfrecido", oferta.CargoOfrecido));
                    cmd.Parameters.Add(new SqlParameter("@RemuneracionOfrecida", oferta.RemuneracionOfrecida));
                    cmd.Parameters.Add(new SqlParameter("@Horario", oferta.Horario));
                    cmd.Parameters.Add(new SqlParameter("@AreaEmpresa", oferta.AreaEmpresa));
                    cmd.Parameters.Add(new SqlParameter("@NumeroVacantes", oferta.NumeroVacantes));
                    cmd.Parameters.Add(new SqlParameter("@RequiereExperienciaLaboral", oferta.RequiereExperienciaLaboral));
                    cmd.Parameters.Add(new SqlParameter("@AreaEmpresa", oferta.RecibeCorreosIdListaValor));
                    cmd.Parameters.Add(new SqlParameter("@ModificadoPor", oferta.ModificadoPor));

                    cmd.Connection = conexion;
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    conexion.Close();

                    procesoExitoso = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;                
            }

            return procesoExitoso;
        }

        public DataSet ObtenerPorId(int idOferta)
        {
            DataSet dsResultado = new DataSet();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Oferta_ObtenerPorId";
                cmd.Parameters.Add(new SqlParameter("@IdOferta", idOferta));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);                
                da.Fill(dsResultado);

                conexion.Close();
            }

            return dsResultado;
        }

        /// <summary>
        /// Obtiene la lista de postulantes para todas las ofertas de la empresa.
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        public DataTable ObtenerPostulacionesPorEmpresa(int idEmpresa)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "OfertaPostulaciones_ObtenerNuevasPostulaciones";
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", idEmpresa));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public DataTable ObtenerPostulantesPorIdOferta(int idOferta)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Oferta_ObtenerPostulantesPorIdOferta";
                cmd.Parameters.Add(new SqlParameter("@IdOferta", idOferta));

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
