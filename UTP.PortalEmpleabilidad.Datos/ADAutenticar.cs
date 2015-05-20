using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Datos
{
  public   class ADAutenticar
    {
        ADConexion cnn = new ADConexion();
        SqlCommand cmd = new SqlCommand();

        public DataSet Autenticar_Usuario(string Usuario)
        {
            DataSet ds = new DataSet();

            using (SqlConnection conexion = new SqlConnection(cnn.Conexion()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Autenticar_Usuario2";
                    cmd.Connection = conexion;
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("@Usuario", SqlDbType.VarChar, 100)).Value = Usuario;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    da.Fill(ds);

                    conexion.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return ds;
        }


        public DataTable ObtenerCabeceraPorCodigoUTP(string usuarioUtp)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UTP_ObtenerCabeceraPorCodigoUsuario";
            cmd.Connection = cnn.cn;
            cnn.Conectar();
            cmd.Parameters.Add(new SqlParameter("@UsuarioUTP", SqlDbType.VarChar, 50)).Value = usuarioUtp;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            cnn.Desconectar();

            return dt;
        }

        /// <summary>
        /// Método para validar si el alumno esta matriculado o es egresado
        /// </summary>
        /// <param name="nombreUsuario">Nombre del usuario</param>
        /// <returns></returns>
        public DataTable ValidarAlumno(string nombreUsuario)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GL_ALUMNO_MATRICULADO";
            cmd.Connection = cnn.cn;
            cnn.Conectar();
            cmd.Parameters.Add(new SqlParameter("@Usuario", SqlDbType.VarChar)).Value = nombreUsuario;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            cnn.Desconectar();

            return dt;
        }
      

    }

}

