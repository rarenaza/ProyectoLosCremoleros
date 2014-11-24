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
    public class ADOfertaInformacionAdicional
    {
        private string cadenaConexion = ConfigurationManager.ConnectionStrings["UTPConexionBD"].ConnectionString;

        public DataTable ObtenerInformacionAdicional(int idOferta)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "OfertaInformacionAdicional_Obtener";
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

        public void InsertarInformacionAdicional(OfertaInformacionAdicional ofertaInformacionAdicional)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "OfertaInformacionAdicional_Insertar";

                //Parámetros:
                cmd.Parameters.Add(new SqlParameter("@IdOferta", ofertaInformacionAdicional.IdOferta));
                cmd.Parameters.Add(new SqlParameter("@TipoConocimiento", ofertaInformacionAdicional.TipoConocimiento));
                cmd.Parameters.Add(new SqlParameter("@Conocimiento", ofertaInformacionAdicional.Conocimiento));
                cmd.Parameters.Add(new SqlParameter("@NivelConocimiento", ofertaInformacionAdicional.NivelConocimiento));
                cmd.Parameters.Add(new SqlParameter("@AñosExperiencia", ofertaInformacionAdicional.AniosExperiencia));
                cmd.Parameters.Add(new SqlParameter("@EstadoOfertaOtroConocimiento", ofertaInformacionAdicional.EstadoOfertaInformacionAdicional));
                cmd.Parameters.Add(new SqlParameter("@CreadoPor", ofertaInformacionAdicional.CreadoPor));

                cmd.Connection = conexion;

                conexion.Open();

                cmd.ExecuteNonQuery();

                conexion.Close();
            }
        }        
    }
}
