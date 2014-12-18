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

        public DataSet Autenticar_Usuario(string Usuario, string Contraseña)
        {
            DataSet ds = new DataSet();

            using (SqlConnection conexion = new SqlConnection(cnn.Conexion()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Autenticar_Usuario";
                    cmd.Connection = conexion;
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("@Usuario", SqlDbType.VarChar, 100)).Value = Usuario;
                    cmd.Parameters.Add(new SqlParameter("@Pass", SqlDbType.VarChar, 100)).Value = Contraseña;
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


      

        }

    }

