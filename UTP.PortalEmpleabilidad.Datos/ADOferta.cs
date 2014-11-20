using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

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
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                conexion.Close();
            }

            return dtResultado;
        }
    }
}
