using System;
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

        public int Insertar(Mensaje mensaje)
        {
            int idMensajeInsertado = 0;
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
                    cmd.Parameters.Add(new SqlParameter("@IdOferta", mensaje.IdOfertaMensaje));
                    cmd.Parameters.Add(new SqlParameter("@IdEvento", mensaje.IdEvento));
                    cmd.Parameters.Add(new SqlParameter("@FechaEnvio", mensaje.FechaEnvio));
                    cmd.Parameters.Add(new SqlParameter("@Asunto", mensaje.Asunto));
                    cmd.Parameters.Add(new SqlParameter("@Mensaje", mensaje.MensajeTexto));
                    cmd.Parameters.Add(new SqlParameter("@IdMensajePadre", mensaje.IdMensajePadre));
                    cmd.Parameters.Add(new SqlParameter("@EstadoMensaje", mensaje.EstadoMensaje));
                    cmd.Parameters.Add(new SqlParameter("@CreadoPor", mensaje.CreadoPor));                    

                    cmd.Connection = conexion;
                    conexion.Open();
                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null)
                    {
                        idMensajeInsertado = Convert.ToInt32(resultado);
                    }
                    conexion.Close();                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return idMensajeInsertado;
        }

        public DataTable ObtenerPorIdMensaje(int idMensaje)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mensaje_ObtenerPorId";
                cmd.Parameters.Add(new SqlParameter("@IdMensaje", idMensaje));                
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();

            }

            return dtResultado;
        }

        public DataTable ObtenerPorUsuario(string usuario)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mensaje_ObtenerPorUsuario";
                cmd.Parameters.Add(new SqlParameter("@Usuario", usuario));
                
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();

            }

            return dtResultado;
        }

        public DataTable ObtenerOfertasPorIdAlumno(int idAlumno)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mensaje_ObtenerOfertasPorIdAlumno";
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

        /// <summary>
        /// Obtiene una oferta por Id, los campos a leer son UsuarioPropietarioEmpresa y su correo electrónico. 
        /// Este método se utiliza en los mensajes.
        /// </summary>
        /// <param name="idOferta"></param>
        /// <returns></returns>
        public DataTable ObtenerUsuarioEmpresaOfertaPorId(int idOferta)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mensaje_ObtenerOfertaPorId";
                cmd.Parameters.Add(new SqlParameter("@IdOferta", idOferta));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public int ObtenerIdEmpresaPorIdOferta(int idOferta)
        {
            int idEmpresa = 0;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mensaje_ObtenerIdEmpresaPorIdOferta";
                cmd.Parameters.Add(new SqlParameter("@IdOferta", idOferta));
                cmd.Connection = conexion;

                conexion.Open();

                object resultado = cmd.ExecuteScalar();

                if (resultado != null) idEmpresa = Convert.ToInt32(resultado);

                conexion.Close();
            }

            return idEmpresa;
        }
        public void ActualizarEstadoMensaje(int idMensaje, string estadoMensaje)
        {           
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mensaje_ActualizarEstadoMensaje";
                cmd.Parameters.Add(new SqlParameter("@IdMensaje", idMensaje));
                cmd.Parameters.Add(new SqlParameter("@EstadoMensaje", estadoMensaje));

                cmd.Connection = conexion;
                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();
            }
            
        }

        public DataTable ObtenerUsuarioAdministradorUTP()
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mensaje_ObtenerUsuarioAdministradorUTP";                
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public DataTable ObtenerUsuarioUTPAsignadoAEmpresa(int idEmpresa)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mensaje_ObtenerUsuarioUTPAsignadoAEmpresa";
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", idEmpresa));

                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public DataTable ObtenerUsuarioEmpresaPorIdEmpresa(int idEmpresa)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mensaje_ObtenerUsuarioEmpresaPorIdEmpresa";
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
    }
}
