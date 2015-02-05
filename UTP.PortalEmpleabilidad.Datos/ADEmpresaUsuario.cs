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
    public class ADEmpresaUsuario : ADBase
    {
        public DataTable ObtenerPorIdEmpresaUsuario(int idEmpresaUsuario)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EmpresaUsuario_ObtenerPorId";
                cmd.Parameters.Add(new SqlParameter("@IdEmpresaUsuario", idEmpresaUsuario));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public void Insertar(EmpresaUsuario empresaUsuario)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EmpresaUsuario_Insertar";

                //Parámetros:
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", empresaUsuario.Empresa.IdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@Usuario", empresaUsuario.NombreUsuario));
                cmd.Parameters.Add(new SqlParameter("@Nombres", empresaUsuario.Nombres));
                cmd.Parameters.Add(new SqlParameter("@Apellidos", empresaUsuario.Apellidos));
                cmd.Parameters.Add(new SqlParameter("@Sexo", empresaUsuario.SexoIdListaValor));
                cmd.Parameters.Add(new SqlParameter("@TipoDocumento", empresaUsuario.TipoDocumentoIdListaValor));
                cmd.Parameters.Add(new SqlParameter("@NumeroDocumento", empresaUsuario.NumeroDocumento));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresaLocacion", empresaUsuario.IdEmpresaLocacion));
                cmd.Parameters.Add(new SqlParameter("@CorreoElectronico", empresaUsuario.CorreoElectronico));
                cmd.Parameters.Add(new SqlParameter("@TelefonoFijo", empresaUsuario.TelefonoFijo));
                cmd.Parameters.Add(new SqlParameter("@TelefonoAnexo", empresaUsuario.TelefonoAnexo));
                cmd.Parameters.Add(new SqlParameter("@TelefonoCelular", empresaUsuario.TelefonoCelular));
                cmd.Parameters.Add(new SqlParameter("@Rol", empresaUsuario.RolIdListaValor));
                cmd.Parameters.Add(new SqlParameter("@EstadoUsuario", empresaUsuario.EstadoUsuarioIdListaValor));
                cmd.Parameters.Add(new SqlParameter("@Contrasena", empresaUsuario.Contrasena));
                cmd.Parameters.Add(new SqlParameter("@CreadoPor", empresaUsuario.CreadoPor));

                cmd.Connection = conexion;

                conexion.Open();

                cmd.ExecuteNonQuery();

                conexion.Close();
            }
        }

        public void Actualizar(EmpresaUsuario empresaUsuario)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EmpresaUsuario_Actualizar";

                //Parámetros:
                cmd.Parameters.Add(new SqlParameter("@IdEmpresaUsuario", empresaUsuario.IdEmpresaUsuario));                
                cmd.Parameters.Add(new SqlParameter("@Usuario", empresaUsuario.NombreUsuario));
                cmd.Parameters.Add(new SqlParameter("@Nombres", empresaUsuario.Nombres));
                cmd.Parameters.Add(new SqlParameter("@Apellidos", empresaUsuario.Apellidos));
                cmd.Parameters.Add(new SqlParameter("@Sexo", empresaUsuario.SexoIdListaValor));
                cmd.Parameters.Add(new SqlParameter("@TipoDocumento", empresaUsuario.TipoDocumentoIdListaValor));
                cmd.Parameters.Add(new SqlParameter("@NumeroDocumento", empresaUsuario.NumeroDocumento));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresaLocacion", empresaUsuario.IdEmpresaLocacion));
                cmd.Parameters.Add(new SqlParameter("@CorreoElectronico", empresaUsuario.CorreoElectronico));
                cmd.Parameters.Add(new SqlParameter("@TelefonoFijo", empresaUsuario.TelefonoFijo));
                cmd.Parameters.Add(new SqlParameter("@TelefonoAnexo", empresaUsuario.TelefonoAnexo));
                cmd.Parameters.Add(new SqlParameter("@TelefonoCelular", empresaUsuario.TelefonoCelular));
                cmd.Parameters.Add(new SqlParameter("@Rol", empresaUsuario.RolIdListaValor));
                cmd.Parameters.Add(new SqlParameter("@EstadoUsuario", empresaUsuario.EstadoUsuarioIdListaValor));
                cmd.Parameters.Add(new SqlParameter("@Contrasena", empresaUsuario.Contrasena));
                cmd.Parameters.Add(new SqlParameter("@ModificadoPor", empresaUsuario.ModificadoPor));

                cmd.Connection = conexion;

                conexion.Open();

                cmd.ExecuteNonQuery();

                conexion.Close();
            }
        }

        /// <summary>
        /// Obtiene los usuarios de una empresa en particular.
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        public DataTable ObtenerUsuariosPorIdEmpresa(int idEmpresa)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EmpresaUsuario_ObtenerPorIdEmpresa";
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

        public int ValidarNumeroDocumento(int idEmpresa, string numeroDocumento, int idEmpresaUsuario)
        {
            int cantidad = 0;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EmpresaUsuario_ValidarNroDocumento";
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", idEmpresa));
                cmd.Parameters.Add(new SqlParameter("@NumeroDocumento", numeroDocumento));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresaUsuario", idEmpresaUsuario));

                cmd.Connection = conexion;

                conexion.Open();

                object resultado = cmd.ExecuteScalar();
                if (resultado != null)
                    cantidad = Convert.ToInt32(resultado);

                conexion.Close();

            }

            return cantidad;
        }

        public int ValidarUsuario(int idEmpresa, string usuario, int idEmpresaUsuario)
        {
            int cantidad = 0;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EmpresaUsuario_ValidarUsuario";
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", idEmpresa));
                cmd.Parameters.Add(new SqlParameter("@Usuario", usuario));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresaUsuario", idEmpresaUsuario));

                cmd.Connection = conexion;

                conexion.Open();

                object resultado = cmd.ExecuteScalar();
                if (resultado != null)
                    cantidad = Convert.ToInt32(resultado);

                conexion.Close();
            }

            return cantidad;
        }

        public DataTable ObtenerUsuariosParaUTP(int nroPaginActual, int filasPorPagina)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EmpresaUsuario_ObtenerListaParaUTP";
                cmd.Parameters.Add(new SqlParameter("@NroPaginaActual", nroPaginActual));
                cmd.Parameters.Add(new SqlParameter("@FilasPorPagina", filasPorPagina));

                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }
    }
}
