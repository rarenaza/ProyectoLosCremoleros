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
    public class ADOfertaEstudio
    {
        private string cadenaConexion = ConfigurationManager.ConnectionStrings["UTPConexionBD"].ConnectionString;

        public DataTable ObtenerEstudios(int idOferta, int idOfertaEstudio)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "OfertaEstudio_Obtener";
                cmd.Parameters.Add(new SqlParameter("@IdOferta", idOferta));
                cmd.Parameters.Add(new SqlParameter("@IdOfertaEstudio", idOfertaEstudio));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public void Insertar(OfertaEstudio ofertaEstudio)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "OfertaEstudio_Insertar";

                //Parámetros:
                cmd.Parameters.Add(new SqlParameter("@IdOferta", ofertaEstudio.IdOferta));
                cmd.Parameters.Add(new SqlParameter("@CicloEstudio", ofertaEstudio.CicloEstudio));
                cmd.Parameters.Add(new SqlParameter("@Estudio", ofertaEstudio.Estudio));
                cmd.Parameters.Add(new SqlParameter("@TipoDeEstudio", ofertaEstudio.TipoDeEstudio.IdListaValor));
                cmd.Parameters.Add(new SqlParameter("@EstadoDelEstudio", ofertaEstudio.EstadoDelEstudio.IdListaValor));
                cmd.Parameters.Add(new SqlParameter("@EstadoOfertaEstudio", ofertaEstudio.EstadoOfertaEstudio.IdListaValor));
                cmd.Parameters.Add(new SqlParameter("@CreadoPor", ofertaEstudio.CreadoPor));

                cmd.Connection = conexion;

                conexion.Open();

                cmd.ExecuteNonQuery();

                conexion.Close();
            }
        }

        public void Actualizar(OfertaEstudio ofertaEstudio)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "OfertaEstudio_Actualizar";

                //Parámetros:
                cmd.Parameters.Add(new SqlParameter("@IdOfertaEstudio", ofertaEstudio.IdOfertaEstudio));
                cmd.Parameters.Add(new SqlParameter("@IdOferta", ofertaEstudio.IdOferta));
                cmd.Parameters.Add(new SqlParameter("@CicloEstudio", ofertaEstudio.CicloEstudio));
                cmd.Parameters.Add(new SqlParameter("@Estudio", ofertaEstudio.Estudio));
                cmd.Parameters.Add(new SqlParameter("@TipoDeEstudio", ofertaEstudio.TipoDeEstudio.IdListaValor));
                cmd.Parameters.Add(new SqlParameter("@EstadoDelEstudio", ofertaEstudio.EstadoDelEstudio.IdListaValor));
                cmd.Parameters.Add(new SqlParameter("@EstadoOfertaEstudio", ofertaEstudio.EstadoOfertaEstudio.IdListaValor));
                cmd.Parameters.Add(new SqlParameter("@ModificadoPor", ofertaEstudio.ModificadoPor));

                cmd.Connection = conexion;

                conexion.Open();

                cmd.ExecuteNonQuery();

                conexion.Close();
            }
        }        
    }
}
