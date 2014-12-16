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
    public class ADUTP
    {
        private string cadenaConexion = ConfigurationManager.ConnectionStrings["UTPConexionBD"].ConnectionString;

        public DataTable OfertasObtenerPendientes()
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ofertas_ObtenerPendientes";
            
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();

            }

            return dtResultado;
        }

        public DataTable EmpresaObtenerPendientes()
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Empresa_ObtenerPendientes";

                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();

            }

            return dtResultado;
        }


        public DataTable Empresa_ObtenerPorNombre(string nombre)
        {
  
            ADConexion cnn = new ADConexion();
            SqlCommand cmd = new SqlCommand();
            
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Empresas_ObtenerNombre";
            cmd.Parameters.Add(new SqlParameter("@Nombre", nombre));

            cmd.Connection = cnn.cn;
            cnn.Conectar();


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            cnn.Desconectar();

            return dt;
        }





    }
}
