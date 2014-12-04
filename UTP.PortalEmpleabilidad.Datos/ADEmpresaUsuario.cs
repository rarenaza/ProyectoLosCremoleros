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
        public void Insertar(EmpresaUsuario empresaUsuario, string usuarioCreacion)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EmpresaUsuario_Insertar";

                //Parámetros:
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", empresaUsuario.Empresa.IdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@Usuario", empresaUsuario.Usuario.NombreUsuario));
                cmd.Parameters.Add(new SqlParameter("@Nombres", empresaUsuario.Nombres));
                cmd.Parameters.Add(new SqlParameter("@Apellidos", empresaUsuario.Apellidos));
                cmd.Parameters.Add(new SqlParameter("@Sexo", empresaUsuario.Sexo.IdListaValor));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresLocacion", empresaUsuario.EmpresaLocacion.IdEmpresaLocacion));
                cmd.Parameters.Add(new SqlParameter("@CorreoElectronico", empresaUsuario.CorreoElectronico));
                cmd.Parameters.Add(new SqlParameter("@TelefonoFijo", empresaUsuario.TelefonoFijo));
                cmd.Parameters.Add(new SqlParameter("@TelefonoAnexo", empresaUsuario.TelefonoAnexo));
                cmd.Parameters.Add(new SqlParameter("@TelefonoCelular", empresaUsuario.TelefonoCelular));
                cmd.Parameters.Add(new SqlParameter("@CreadoPor", usuarioCreacion));

                cmd.Connection = conexion;

                conexion.Open();

                cmd.ExecuteNonQuery();

                conexion.Close();
            }
        }
    }
}
