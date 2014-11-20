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
    public class ADOferta
    {
        private string cadenaConexion = ConfigurationManager.ConnectionStrings["UTPConexionBD"].ConnectionString;
        /// <summary>
        /// Obtiene las Ofertas Para la Pantalla Oferta
        /// </summary>
        /// <returns></returns>
        ///

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
                DataTable dt = new DataTable();

                da.Fill(dt);

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
                DataTable dt = new DataTable();

                da.Fill(dt);

                conexion.Close();
            }

            return dtResultado;
        }

        public DataTable ObtenerBusquedaAbanzada(int IdLista)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Lista_ListaValor";
                cmd.Parameters.Add(new SqlParameter("@IdLista", SqlDbType.Int)).Value = IdLista;
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                conexion.Close();
            }

            return dtResultado;
        }

        public DataTable Obtener_PanelEmpresa(int idEmpresa)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Oferta_ObtenerPanelEmpresa";
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", idEmpresa));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                conexion.Close();
            }

            return dtResultado;
        }

        public void Insertar(Oferta oferta)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Oferta_Insertar";

                //Parámetros:
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", oferta.IdEmpresa ));
                cmd.Parameters.Add(new SqlParameter("@UsuarioPropietarioEmpresa", oferta.UsuarioPropietarioEmpresa));
                cmd.Parameters.Add(new SqlParameter("@EstadoOferta", oferta.EstadoOferta));
                cmd.Parameters.Add(new SqlParameter("@FechaPublicacion", oferta.FechaPublicacion));
                cmd.Parameters.Add(new SqlParameter("@FechaFinRecepcionCV", oferta.FechaFinRecepcionCV));
                cmd.Parameters.Add(new SqlParameter("@FechaFinProceso", oferta.FechaFinProceso));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresaLocacion", oferta.IdEmpresaLocacion));
                cmd.Parameters.Add(new SqlParameter("@DescripcionOferta", oferta.DescripcionOferta));
                cmd.Parameters.Add(new SqlParameter("@TipoTrabajo", oferta.TipoTrabajo));
                cmd.Parameters.Add(new SqlParameter("@TipoContrato", oferta.TipoContrato));
                cmd.Parameters.Add(new SqlParameter("@DuracionContrato", oferta.DuracionContrato));
                cmd.Parameters.Add(new SqlParameter("@TipoCargo", oferta.TipoCargo));
                cmd.Parameters.Add(new SqlParameter("@CargoOfrecido", oferta.CargoOfrecido));
                cmd.Parameters.Add(new SqlParameter("@RemuneracionOfrecida", oferta.RemuneracionOfrecida));                
                cmd.Parameters.Add(new SqlParameter("@FechaInicioLabores", oferta.FechaInicioLabores));
                cmd.Parameters.Add(new SqlParameter("@Horario", oferta.Horario));                
                cmd.Parameters.Add(new SqlParameter("@AreaEmpresa", oferta.AreaEmpresa));
                cmd.Parameters.Add(new SqlParameter("@NumeroVacantes", oferta.NumeroVacantes));
                cmd.Parameters.Add(new SqlParameter("@RequiereExperienciaLaboral", oferta.RequiereExperienciaLaboral));                
                cmd.Parameters.Add(new SqlParameter("@CreadoPor", oferta.CreadoPor));                

                cmd.Connection = conexion;

                conexion.Open();

                cmd.ExecuteNonQuery();
                
                conexion.Close();
            }
        }
    }
}
