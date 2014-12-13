using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTP.PortalEmpleabilidad.Datos
{
    public class ADMensaje : ADBase
    {
        public DataTable ObtenerPorIdEmpresa(int idEmpresa, int idOferta)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mensaje_ObtenerPorIdEmpresaIdOferta";
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", idEmpresa));
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

        public DataTable ObtenePostulantesPorIdOferta(int idOferta)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "OfertaPostulante_ObtenerPorIdOferta";
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

        public void Insertar(Mensaje mensaje)
        {           
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mensaje_Insertar";

                    //Parámetros:
                    cmd.Parameters.Add(new SqlParameter("@DeUsuario", mensaje.DeUsuario));
                    cmd.Parameters.Add(new SqlParameter("@DeUsuarioCorreoElectronico", mensaje.DeUsuarioCorreoElectronico));
                    cmd.Parameters.Add(new SqlParameter("@ParaUsuario", mensaje.ParaUsuario));
                    cmd.Parameters.Add(new SqlParameter("@ParaUsuarioCorreoElectronico", mensaje.ParaUsuarioCorreoElectronico));
                    cmd.Parameters.Add(new SqlParameter("@IdOferta", mensaje.IdOferta));
                    cmd.Parameters.Add(new SqlParameter("@IdEvento", mensaje.IdEvento));
                    cmd.Parameters.Add(new SqlParameter("@FechaEnvio", mensaje.FechaEnvio));
                    cmd.Parameters.Add(new SqlParameter("@Asunto", mensaje.Asunto));
                    cmd.Parameters.Add(new SqlParameter("@Mensaje", mensaje.MensajeTexto));
                    cmd.Parameters.Add(new SqlParameter("@EstadoMensaje", mensaje.EstadoMensaje));
                    cmd.Parameters.Add(new SqlParameter("@CreadoPor", mensaje.CreadoPor));                    

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
    }
}
