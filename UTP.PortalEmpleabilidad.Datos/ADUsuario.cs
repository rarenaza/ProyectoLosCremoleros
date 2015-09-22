using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Empresa;

namespace UTP.PortalEmpleabilidad.Datos
{
    public class ADUsuario : ADBase
    {
        public bool ValidarNombreDeUsuario(string nombreUsuario)
        {
            bool existe = false;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Usuario_ValidarNombreDeUsuario";
                cmd.Connection = conexion;

                conexion.Open();

                cmd.Parameters.Add(new SqlParameter("@NombreUsuario", nombreUsuario == null ? "" : nombreUsuario));

                object resultado = cmd.ExecuteScalar();

                if (resultado != null) existe = Convert.ToBoolean(resultado);

                conexion.Close();
            }

            return existe;
        }
        public bool ValidarExistenciaEmpresa(string empresaPais, string empresaRUC)
        {
            bool existe = false;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Registro_ValidarExistenciaEmpresa";
                cmd.Connection = conexion;

                conexion.Open();

                cmd.Parameters.Add(new SqlParameter("@EmpresaPais", empresaPais == null ? "" : empresaPais));
                cmd.Parameters.Add(new SqlParameter("@EmpresaRUC", empresaRUC == null ? "" : empresaRUC));
                object resultado = cmd.ExecuteScalar();

                if (resultado != null) existe = Convert.ToBoolean(resultado);

                conexion.Close();
            }

            return existe;
        }

        public DataTable ObtenerUsuariosPorTipo(string tipoUsuario)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Usuario_ObtenerPorTipo";
                cmd.Parameters.Add(new SqlParameter("@TipoUsuario", tipoUsuario == null ? "" : tipoUsuario));          

                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public void InsertarToken(string token, string usuario, DateTime fechaExpira, DateTime fechaSolicito, string ip)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand();

                    conexion.Open();
                    cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "Token_Empresa_Insertar";

                    cmd.Parameters.Add(new SqlParameter("@IdToken", token == null ? "" : token));
                    cmd.Parameters.Add(new SqlParameter("@Usuario", usuario == null ? "" : usuario));
                    cmd.Parameters.Add(new SqlParameter("@FechaExpira", fechaExpira));
                    cmd.Parameters.Add(new SqlParameter("@FechaSolicito", fechaSolicito));
                    cmd.Parameters.Add(new SqlParameter("@Ip", ip == null ? "" : ip));
                    cmd.Connection = conexion;
                    cmd.ExecuteNonQuery();
                    conexion.Close();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerToken(string usuario)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Token_Empresa_Obtener";
                cmd.Parameters.Add(new SqlParameter("@Usuario", usuario == null ? "" : usuario));

                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public bool ActualizarTerminosCondiciones(string usuario)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand();

                    conexion.Open();
                    cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "Usuario_ActualizarTerminosCondiciones";

                    cmd.Parameters.Add(new SqlParameter("@Usuario", usuario == null ? "" : usuario));
                    cmd.Connection = conexion;
                    cmd.ExecuteNonQuery();
                    conexion.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
